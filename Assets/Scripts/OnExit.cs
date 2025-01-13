using System.Collections;
using UnityEngine;

public class OnExit : StateMachineBehaviour
{
    [SerializeField] private Animations animation;
    [SerializeField] private bool lockLayer;
    [SerializeField] private float crossfade = 0.2f;
    [HideInInspector] public bool cancel = false;
    [HideInInspector] public int layerIndex = -1;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.layerIndex = layerIndex;
        cancel = false;
        TestAnimations.instance.StartCoroutine(Wait());

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(stateInfo.length - crossfade);

            if (cancel) yield break;

            AnimatorBrain target = animator.GetComponent<AnimatorBrain>();
            target.SetLocked(false, layerIndex);
            target.Play(animation, layerIndex, lockLayer, false, crossfade);
        }
    }
}
