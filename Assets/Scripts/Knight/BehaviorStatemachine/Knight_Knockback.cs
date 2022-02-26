using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Knockback : StateMachineBehaviour
{
    public float knockbackForceX;
    public float knockbackForceY;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Rigidbody2D rb = animator.GetComponent<Rigidbody2D>();
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        animator.GetComponent<LifeEnemy>().recovering = true;

        if (rb.transform.position.x < player.position.x)
        {
            rb.AddForce(new Vector2(-knockbackForceX, knockbackForceY), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(knockbackForceX, knockbackForceY), ForceMode2D.Impulse);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<LifeEnemy>().recovering = false;
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
