using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    private const string ATTACK_TRIGGER_STRING = "Attack";

    private PlayerControls playerControls = null;
    private Animator myAnimator = null;

    private void Awake()
    {
        playerControls = new PlayerControls();
        myAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {
        playerControls.Combat.Attack.started += Attack;
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        myAnimator.SetTrigger(ATTACK_TRIGGER_STRING);
    }

    //private void Attack()
    //{
    //    myAnimator.SetTrigger(ATTACK_TRIGGER_STRING);
    //}
}