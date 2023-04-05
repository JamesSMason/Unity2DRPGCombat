using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public bool FacingLeft { get { return facingLeft; } }

    [SerializeField] private TrailRenderer myTrailRenderer = null;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float dashSpeed = 4.0f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCD = 0.25f;

    private PlayerControls playerControls = null;
    private Vector2 movement = Vector2.zero;
    private Rigidbody2D rb = null;
    private Animator myAnimator = null;
    private SpriteRenderer mySpriteRenderer = null;

    private const string MOVE_X_STRING = "moveX";
    private const string MOVE_Y_STRING = "moveY";

    private bool facingLeft = false;
    private bool isDashing = false;
    private float startingMoveSpeed;

    private void Awake()
    {
        Instance = this;

        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
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

    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat(MOVE_X_STRING, movement.x);
        myAnimator.SetFloat(MOVE_Y_STRING, movement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
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