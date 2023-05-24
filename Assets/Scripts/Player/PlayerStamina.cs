using System;
using System.Collections;
using UnityEngine;

public class PlayerStamina : Singleton<PlayerStamina>
{
    public Action<int> OnStaminaUpdated;

    [SerializeField] private float timeBetweenStaminaRefrsh = 3.0f;

    public int CurrentStamina { get; private set; }

    private int startingStamina = 3;
    private int maxStamina;

    protected override void Awake()
    {
        base.Awake();

        maxStamina = startingStamina;
        CurrentStamina = startingStamina;
    }

    public void UseStamina()
    {
        CurrentStamina--;
        OnStaminaUpdated?.Invoke(CurrentStamina);
        StopAllCoroutines();
        StartCoroutine(RefreshStaminaRoutine());
    }

    public void RefreshStamina()
    {
        if (CurrentStamina < maxStamina) 
        {
            CurrentStamina++;
            OnStaminaUpdated?.Invoke(CurrentStamina);
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
        }
    }

    private IEnumerator RefreshStaminaRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStaminaRefrsh);
            RefreshStamina();
        }
    }
}