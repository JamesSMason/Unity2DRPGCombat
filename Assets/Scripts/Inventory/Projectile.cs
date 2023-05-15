using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;

    void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        Vector3 moveDir = Vector3.right * moveSpeed * Time.deltaTime;
        transform.Translate(moveDir);
    }
}
