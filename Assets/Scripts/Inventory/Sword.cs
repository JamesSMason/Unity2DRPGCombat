using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    private const string ATTACK_TRIGGER_STRING = "Attack";

    [SerializeField] private GameObject slashAnimPrefab = null;
    [SerializeField] private Transform slashAnimSpawnPoint = null;
    [SerializeField] private Transform weaponCollider = null;
    [SerializeField] private float swordAttackCD = 0.5f;

    private PlayerController playerController = null;
    private ActiveWeapon activeWeapon = null;
    private Animator myAnimator = null;

    private GameObject slashAnim;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    public void SwingUpFlipAnimEvent()
    {
        slashAnim.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        slashAnim.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (playerScreenPoint.x > mousePos.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }

    public void Attack()
    {
        //isAttacking = true;
        myAnimator.SetTrigger(ATTACK_TRIGGER_STRING);
        weaponCollider.gameObject.SetActive(true);

        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
        StartCoroutine(AttackCDRoutine());
    }
}