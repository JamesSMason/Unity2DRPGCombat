using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    private Knockback knockback = null;
    private Rigidbody2D rb = null;
    private Vector2 moveDir = Vector2.zero;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (knockback.gettingKnockedBack) { return; }
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }
}