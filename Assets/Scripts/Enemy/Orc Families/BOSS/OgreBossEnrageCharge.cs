using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreBossEnrageCharge : StateMachineBehaviour
{
    Ogre ogre;
    Transform player;
    public float speed;
    public float chargeTime;
    private float chargeTimeCounter;
    private float direction;
    Rigidbody2D RB;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        RB = animator.GetComponent<Rigidbody2D>();
        ogre = animator.GetComponent<Ogre>();
        chargeTimeCounter = chargeTime;
        if(player.position.x - animator.transform.position.x < 0)
        {
            direction = -1;
        } else
        {
            direction = 1;
        }
        ogre.isCharging = true;

        FindObjectOfType<SoundManager>().Play("MonsterScream");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chargeTimeCounter -= Time.deltaTime;

        if(chargeTimeCounter > 0)
        {
            RB.velocity = new Vector2(speed * direction, RB.velocity.y);
        } else
        {
            animator.SetTrigger("Idle");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ogre.isCharging = false;
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
