using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("Staff");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}
