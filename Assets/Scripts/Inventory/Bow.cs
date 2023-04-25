using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo = null;

    public void Attack()
    {
        Debug.Log("Bow");
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}