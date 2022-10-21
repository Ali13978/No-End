using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDown : StateMachineBehaviour
{
    float CooldownTimer = 1.5f;
    float CurrentTimer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        CurrentTimer = CooldownTimer;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CurrentTimer -= Time.deltaTime;
        if(CurrentTimer <= 0)
        {
            animator.SetTrigger("CoolOff");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        animator.ResetTrigger("CoolOff");
    }
}
