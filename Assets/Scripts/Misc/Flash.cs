using System;
using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat = null;
    [SerializeField] private float restoreDefaultMatTime = 0.2f;
    
    private SpriteRenderer spriteRenderer = null;
    private Material defaultMat = null;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }

    public IEnumerator FlashRoutine(Action action = null)
    {
        spriteRenderer.material = whiteFlashMat;
        yield return new WaitForSeconds(restoreDefaultMatTime);
        spriteRenderer.material = defaultMat;
        if (action != null)
        {
            action();
        }
    }
}