using TMPro;
using UnityEngine;

public class GoldCoinContainerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText = null;

    private void Start()
    {
        EconomyManager.Instance.OnGoldUpdated += EconomyManager_OnGoldUpadted;
    }

    private void EconomyManager_OnGoldUpadted(int goldAmount)
    {
        goldText.text = goldAmount.ToString("D3");
    }
}