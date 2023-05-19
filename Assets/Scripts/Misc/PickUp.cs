using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickUp : MonoBehaviour
{
    [SerializeField] private float pickUpDistance = 5.0f;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float accelerationRate = 0.3f;

    private Vector3 moveDir = Vector3.zero;

    private Rigidbody2D rb = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;

        if (Vector3.Distance(playerPos, transform.position) < pickUpDistance)
        {
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += accelerationRate;
        }
        else
        {
            moveDir = Vector3.zero;
            moveSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            Destroy(gameObject);
        }
    }
}