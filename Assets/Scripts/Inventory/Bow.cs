using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("Bow");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}