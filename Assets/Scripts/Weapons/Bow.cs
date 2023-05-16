using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    private readonly int FIRE_HASH = Animator.StringToHash("Fire");

    [SerializeField] private WeaponInfo weaponInfo = null;
    [SerializeField] private GameObject arrowPrefab = null;
    [SerializeField] private Transform arrowSpawnPoint = null;

    private Animator myAnimator = null;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void Attack()
    {
        myAnimator.SetTrigger(FIRE_HASH);
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        newArrow.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}