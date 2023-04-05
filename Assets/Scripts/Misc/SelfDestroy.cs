using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    private ParticleSystem ps = null;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (ps && !ps.IsAlive())
        {
            DestroySelfAnimEvent();
        }
    }

    public void DestroySelfAnimEvent()
    {
        Destroy(gameObject);
    }
}