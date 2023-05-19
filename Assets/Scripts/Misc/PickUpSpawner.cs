using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pickUpPrefab = null;

    public void DropItems()
    {
        Instantiate(pickUpPrefab, transform.position, Quaternion.identity);
    }
}