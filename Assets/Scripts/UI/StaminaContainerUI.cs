using UnityEngine;
using UnityEngine.UI;

public class StaminaContainerUI : MonoBehaviour
{
    [SerializeField] private Image[] staminaImageArray = null;

    private void Start()
    {
        PlayerStamina.Instance.OnStaminaUpdated += PlayerStamina_OnStaminaUpdated;
    }

    private void PlayerStamina_OnStaminaUpdated(int currentStamina)
    {
        int staminaImageArrayLength = staminaImageArray.Length;

        for (int i = 0; i < staminaImageArrayLength; i++)
        {
            staminaImageArray[i].enabled = false;
        }

        for (int i = 1; i <= currentStamina; i++)
        {
            int imageIndex = staminaImageArrayLength - i;
            staminaImageArray[imageIndex].enabled = true;
        }
    }
}