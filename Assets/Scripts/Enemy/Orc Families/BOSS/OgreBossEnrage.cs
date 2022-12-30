using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreBossEnrage : StateMachineBehaviour
{
    Transform player;
    Ogre ogre;
    public float timeWait;
    private float timeWaitCounter;
    private float rand;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ogre = animator.GetComponent<Ogre>();
        timeWaitCounter = timeWait;
        rand = Random.Range(0,2);
        // int rando = Random.Range(0,3);
        // if(rando == 0)
        // {
        //     FindObjectOfType<SoundManager>().Play("WeaponSwing1");
        // } else if(rando == 1)
        // {
        //     FindObjectOfType<SoundManager>().Play("WeaponSwing2");
        // } else
        // {
        // }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(timeWaitCounter <= 0)
        {
            if(ogre.inRange)
            {
                animator.SetTrigger("Attack");
            } else if(rand == 0)
            {
                animator.SetTrigger("Run");
            } else
            {
                animator.SetTrigger("Jump");
            }
        } else
        {
            timeWaitCounter -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
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
