using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "";
    [SerializeField] private string sceneTransitionName = "";
    [SerializeField] private float waitToLoadTime = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            StartCoroutine(LoadSceneRoutine());
            UIFade.Instance.FadeToBlack();
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        float trackedTime = waitToLoadTime;
        while (trackedTime > 0)
        {
            trackedTime -= Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneToLoad);
    }
}