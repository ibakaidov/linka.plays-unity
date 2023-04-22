using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBehaviour : StateMachineBehaviour
{
    public GameObject bubble;
    public float speed = 2;
    public int bubbleCountMin = 10;
    public int bubbleCountMax = 30;
    public float bubbleSpread = 1;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("JiggleTime", 0);
        ChestScript.soundOpen.Play();
        //ChestScript.soundOpen.SetScheduledEndTime(2);
        for (int i = 0; i < Random.value * (bubbleCountMax - bubbleCountMin) + bubbleCountMin; i++)
        {
            var newBubble = Instantiate(bubble);
            var pos = newBubble.transform.position + (Vector3)Random.insideUnitCircle * bubbleSpread;
            newBubble.GetComponent<BubbleScript>().speed = speed;
            //pos.z = -7;
            newBubble.transform.position = pos;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("OpenTime", animator.GetFloat("OpenTime") + Time.deltaTime);
        Debug.Log(animator.GetFloat("OpenTime"));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ChestScript.soundOpen.Stop();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
