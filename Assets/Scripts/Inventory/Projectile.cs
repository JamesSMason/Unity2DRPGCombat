using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX = null;

    private WeaponInfo weaponInfo = null;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        Indestructible indestructible = collision.GetComponent<Indestructible>();

        if (!collision.isTrigger && (enemyHealth || indestructible))
        {
            enemyHealth?.TakeDamage(weaponInfo.weaponDamage);
            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void UpdateWeaponInfo(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
    }

    private void MoveProjectile()
    {
        Vector3 moveDir = Vector3.right * moveSpeed * Time.deltaTime;
        transform.Translate(moveDir);
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > weaponInfo.weaponRange)
        {
            Destroy(gameObject);
        }
    }
}