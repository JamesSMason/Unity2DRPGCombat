using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoin, staminaGlobe, healthGlobe = null;
    [SerializeField] private int maxNumberOfDrops = 4;

    public void DropItems()
    {
        int randomNum = Random.Range(1, 4);

        if (randomNum == 1)
        {
            Instantiate(staminaGlobe, transform.position, Quaternion.identity);
        }

        if (randomNum == 2)
        {
            Instantiate(healthGlobe, transform.position, Quaternion.identity);
        }

        if (randomNum == 3)
        {
            int randomAmountOfGold = Random.Range(1, maxNumberOfDrops);
            for (int i = 0; i < randomAmountOfGold; i++)
            {
                Instantiate(goldCoin, transform.position, Quaternion.identity);
            }
        }
    }
}