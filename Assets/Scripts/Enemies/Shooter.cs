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
    [SerializeField][Tooltip("Stagger the spawn of the bullets over the time between bursts.")] private bool stagger = false;
    [SerializeField][Tooltip("Reverse the direction of each burst.")] private bool oscillate = false;

    private bool isShooting = false;

    private void OnValidate()
    {
        if (oscillate) { stagger = true; }
        if (projectilesPerBurst < 1) { projectilesPerBurst = 1; }
        if (burstCount < 1) { burstCount = 1; }
        if (timeBetweenBursts < 0.1f) { timeBetweenBursts = 0.1f; }
        if (restTime < 0.1f) { restTime = 0.1f; }
        if (startingDistance < 0.1f) { startingDistance = 0.1f; }
        if (angleSpread == 0.0f) { projectilesPerBurst = 1; }
        if (bulletMoveSpeed < 0.0f) { bulletMoveSpeed = 1f; }
    }

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

        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0.0f;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        if (stagger)
        {
            timeBetweenProjectiles = timeBetweenBursts / projectilesPerBurst;
        }

        for (int i = 0; i < burstCount; i++)
        {
            if (!oscillate)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }

            if (oscillate && i % 2 != 1)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            else
            {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }

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

                if (stagger)
                {
                    yield return new WaitForSeconds(timeBetweenProjectiles);
                }
            }

            currentAngle = startAngle;

            if (!stagger)
            {
                yield return new WaitForSeconds(timeBetweenBursts);
            }
        }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        startAngle = targetAngle;
        endAngle = targetAngle;
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