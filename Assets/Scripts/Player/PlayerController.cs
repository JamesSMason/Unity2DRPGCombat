using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    public bool FacingLeft { get { return facingLeft; } }

    [SerializeField] private TrailRenderer myTrailRenderer = null;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float dashSpeed = 4.0f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCD = 0.25f;
    [SerializeField] private Transform weaponCollider = null;
    [SerializeField] private Transform slashAnimSpawnPoint = null;

    private PlayerControls playerControls = null;
    private Vector2 movement = Vector2.zero;
    private Rigidbody2D rb = null;
    private Animator myAnimator = null;
    private SpriteRenderer mySpriteRenderer = null;
    private Knockback knockback = null;

    private const string MOVE_X_STRING = "moveX";
    private const string MOVE_Y_STRING = "moveY";

    private bool facingLeft = false;
    private bool isDashing = false;
    private float startingMoveSpeed;

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        playerControls.Combat.Dash.started += _ => Dash();

        startingMoveSpeed = moveSpeed;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }

    public Transform GetSlashAnimSpawnPoint()
    {
        return slashAnimSpawnPoint;
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat(MOVE_X_STRING, movement.x);
        myAnimator.SetFloat(MOVE_Y_STRING, movement.y);
    }

    private void Move()
    {
        if (knockback.GettingKnockedBack) { return; }

        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (playerScreenPoint.x > mousePos.x)
        {
            mySpriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}