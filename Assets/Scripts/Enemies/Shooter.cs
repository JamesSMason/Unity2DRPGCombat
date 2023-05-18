using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField][Tooltip("Defines the speed of the bullets.")] private float bulletMoveSpeed = 8.0f;
    [SerializeField][Tooltip("The number of bursts in a single attack.")] private int burstCount = 5;
    [SerializeField][Tooltip("The number of bullets in each burst.")] private int projectilesPerBurst = 1;
    [SerializeField][Range(0, 359)][Tooltip("The angle the bullets in a burst will disperse over.")] private float angleSpread = 0.0f;
    [SerializeField][Tooltip("The distance the bullets spawn away from the spawning object.")] private float startingDistance = 0.1f;
    [SerializeField][Tooltip("The delay between each burst in a single attack.")] private float timeBetweenBursts = 0.3f;
    [SerializeField][Tooltip("The recovery time after an attack.")] private float restTime = 2.0f;

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

        float startAngle, currentAngle, angleStep;
        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);

        for (int i = 0; i < burstCount; i++)
        {
            for (int j = 0; j < projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);

                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateProjectileSpeed(bulletMoveSpeed);
                }

                currentAngle += angleStep;
            }

            currentAngle = startAngle;

            yield return new WaitForSeconds(timeBetweenBursts);

            TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);
        }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep)
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        startAngle = targetAngle;
        float endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0.0f;
        angleStep = 0.0f;
        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projectilesPerBurst - 1);
            halfAngleSpread = angleSpread / 2;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + (startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad));
        float y = transform.position.y + (startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad));

        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}