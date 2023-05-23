using UnityEngine;
using UnityEngine.UI;

public class HeartContainerUI : MonoBehaviour
{
    [SerializeField] private Image healthImage = null;

    private void Start()
    {
        PlayerHealth.Instance.OnHealthUpdated += PlayerHealth_OnHealthUpdated;
    }

    private void OnDisable()
    {
        PlayerHealth.Instance.OnHealthUpdated -= PlayerHealth_OnHealthUpdated;
    }

    private void PlayerHealth_OnHealthUpdated(float healthRatio)
    {
        healthImage.fillAmount = healthRatio;
    }
}