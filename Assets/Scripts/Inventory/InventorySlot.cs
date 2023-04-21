using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private WeaponInfo weaponInfo = null;

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}