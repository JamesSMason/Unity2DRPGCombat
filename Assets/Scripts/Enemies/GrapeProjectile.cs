using System.Collections;
using UnityEngine;

public class GrapeProjectile : MonoBehaviour
{
    [SerializeField] private float duration = 1.0f;
    [SerializeField] private AnimationCurve animCurve = new AnimationCurve();
    [SerializeField] private float heightY = 3.0f;
    [SerializeField] private GameObject grapeProjectileShadow = null;
    [SerializeField] private Vector3 grapeProjectileShadowOffset = new Vector3(0.0f, -0.3f, 0.0f);

    float timePassed = 0.0f;

    private void Start()
    {
        GameObject grapeShadow = Instantiate(grapeProjectileShadow, transform.position + grapeProjectileShadowOffset, Quaternion.identity);

        Vector3 playerPos = PlayerController.Instance.transform.position;

        StartCoroutine(ProjectileCurveRoutine(transform.position, playerPos));
        StartCoroutine(MoveGrapeShadowRoutine(grapeShadow, grapeShadow.transform.position, playerPos));
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
    }

    private IEnumerator ProjectileCurveRoutine(Vector3 startPos, Vector3 endPos)
    {
        while (timePassed < duration)
        {
            float linearT = timePassed / duration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0.0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPos, endPos, linearT) + new Vector2(0.0f, height);

            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator MoveGrapeShadowRoutine(GameObject grapeShadow, Vector3 startPos, Vector3 endPos)
    {
        while (timePassed < duration)
        {
            float linearT = timePassed / duration;
            grapeShadow.transform.position = Vector2.Lerp(startPos, endPos, linearT);

            yield return null;
        }

        Destroy(grapeShadow);
    }
}