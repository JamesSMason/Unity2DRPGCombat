using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private enum PickUpType
    {
        GoldCoin,
        StaminaGlobe,
        HealthGlobe
    }

    [SerializeField] private PickUpType pickUpType;
    [SerializeField] private float pickUpDistance = 5.0f;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float accelerationRate = 0.3f;
    [SerializeField] private AnimationCurve animCurve = null;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 1.0f;
    [SerializeField] private float spawnPointRandomOffset = 2.5f;

    private Vector3 moveDir = Vector3.zero;

    private Rigidbody2D rb = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(AnimCurveSpawnRoutine());
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
            DetectPickupType();
            Destroy(gameObject);
        }
    }

    public void DetectPickupType()
    {
        switch (pickUpType)
        {
            case PickUpType.GoldCoin:
                EconomyManager.Instance.UpdateCurrentGold();
                break;
            case PickUpType.HealthGlobe:
                PlayerHealth.Instance.HealPlayer();
                break;
            case PickUpType.StaminaGlobe:
                Debug.Log("Stamina Globe");
                break;
        }
    }

    private IEnumerator AnimCurveSpawnRoutine()
    {
        Vector2 startPos = transform.position;
        float randomX = Random.Range(-spawnPointRandomOffset, spawnPointRandomOffset);
        float randomY = Random.Range(-spawnPointRandomOffset, spawnPointRandomOffset) / 2;
        Vector2 endPos = transform.position + new Vector3(randomX, randomY, 0.0f);

        float timePassed = 0.0f;

        while (timePassed < popDuration)
        {
            timePassed += Time.deltaTime;

            float linearT = timePassed / popDuration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0.0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPos, endPos, linearT) + new Vector2(0.0f, height);

            yield return null;
        }
    }
}