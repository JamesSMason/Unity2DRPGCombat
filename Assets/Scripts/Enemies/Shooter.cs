using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private float bulletMoveSpeed = 8.0f;
    [SerializeField] private int burstCount = 5;
    [SerializeField] private float timeBetweenBursts = 2.0f;
    [SerializeField] private float restTime = 0.3f;

    private bool isShooting = false;

    public void Attack()
    {
        if (!isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootRoutine());
        }
    }

    private IEnumerator ShootRoutine()
    {
        isShooting = true;

        for (int i = 0; i < burstCount; i++)
        {
            Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;

            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.transform.right = targetDirection;

            if (newBullet.TryGetComponent(out Projectile projectile))
            {
                projectile.UpdateProjectileSpeed(bulletMoveSpeed);
            }

            yield return new WaitForSeconds(restTime);
        }

        yield return new WaitForSeconds(timeBetweenBursts);
        isShooting = false;
    }
}