using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float knockbackTime = 0.2f;

    public bool gettingKnockedBack { get; private set; } = false;

    private Rigidbody2D rb = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knockbackThrust)
    {
        gettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockbackThrust * rb.mass;
        Debug.Log(difference);
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        gettingKnockedBack = false;
    }
}