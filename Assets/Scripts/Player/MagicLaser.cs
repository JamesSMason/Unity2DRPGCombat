using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float laserGrowTime = 2.0f;

    private SpriteRenderer spriteRenderer = null;
    private CapsuleCollider2D capsuleCollider2D = null;
    private float laserRange = 0.0f;

    private bool isGrowing = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        LaserFaceMouse();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Indestructible>() && !collision.isTrigger)
        {
            isGrowing = false;
        }
    }

    public void UpdateLaserRange(float laserRange)
    {
        this.laserRange = laserRange;
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

    private IEnumerator IncreaseLaserLengthRoutine()
    {
        float timePassed = 0.0f;

        while (spriteRenderer.size.x < laserRange && isGrowing)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / laserGrowTime;

            spriteRenderer.size = new Vector2(Mathf.Lerp(1.0f, laserRange, linearT), 1.0f);

            capsuleCollider2D.size = new Vector2(Mathf.Lerp(1.0f, laserRange, linearT), capsuleCollider2D.size.y);
            capsuleCollider2D.offset = new Vector2((Mathf.Lerp(1, laserRange, linearT) / 2), capsuleCollider2D.offset.y);

            yield return null;
        }

        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }

    private void LaserFaceMouse()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = transform.position - mousePosition;

        transform.right = -direction;
    }
}