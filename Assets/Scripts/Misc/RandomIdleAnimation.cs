using UnityEngine;

public class RandomIdleAnimation : MonoBehaviour
{
    [SerializeField] private Animator myAnimator = null;

    private void Start()
    {
        AnimatorStateInfo state = myAnimator.GetCurrentAnimatorStateInfo(0);
        myAnimator.Play(state.fullPathHash, -1, Random.Range(0.0f, 1.0f));
    }
}