using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon")]
public class WeaponInfo : ScriptableObject
{
    public GameObject weaponPrefab = null;
    public float weaponCooldown = 3.0f;
    public int weaponDamage = 0;
    public float weaponRange = 0;
}