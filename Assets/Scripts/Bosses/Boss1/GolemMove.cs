using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMove : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 3f;
    [SerializeField] List<string> Attacktriggers;

    Transform player;
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponentInChildren<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Boss>().LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.velocity = Vector2.zero;
        rb.MovePosition(newPos);

        if(Vector2.Distance(animator.transform.position, player.position) <= attackRange)
        {
            int Index = Random.Range(0, Attacktriggers.Count);
            animator.SetTrigger(Attacktriggers[Index]);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach(string i in Attacktriggers)
        {
            animator.ResetTrigger(i);
        }
    }

}
