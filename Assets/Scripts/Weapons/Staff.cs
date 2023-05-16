using UnityEngine;
using UnityEngine.InputSystem;

public class Staff : MonoBehaviour, IWeapon
{
    private readonly int FIRE_HASH = Animator.StringToHash("Fire");

    [SerializeField] private WeaponInfo weaponInfo = null;
    [SerializeField] private GameObject magicLaser = null;
    [SerializeField] private Transform magicLaserSpawnPoint = null;

    private Animator myAnimator = null;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    public void Attack()
    {
        myAnimator.SetTrigger(FIRE_HASH);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void SpawnStaffProjectileAnimEvent()
    {
        GameObject newLaser = Instantiate(magicLaser, magicLaserSpawnPoint.position, Quaternion.identity);
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (playerScreenPoint.x > mousePos.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
