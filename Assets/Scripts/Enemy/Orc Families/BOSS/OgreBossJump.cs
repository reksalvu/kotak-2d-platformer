using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreBossJump : StateMachineBehaviour
{
    Transform player;
    Ogre ogre;
    Rigidbody2D RB;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ogre = animator.GetComponent<Ogre>();
        RB = animator.GetComponent<Rigidbody2D>();
 
        float distanceFromPlayer = player.position.x - animator.transform.position.x; 
        RB.AddForce(new Vector2(distanceFromPlayer,(Mathf.Abs(distanceFromPlayer)) + 5), ForceMode2D.Impulse);

        int rand = Random.Range(0,2);
        if(rand == 0)
        {
            FindObjectOfType<SoundManager>().Play("MonsterGrowl1");
        } else 
        {
            FindObjectOfType<SoundManager>().Play("MonsterGrowl2");
        } 
        FindObjectOfType<SoundManager>().Play("JumpWhoosh");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(ogre.isGrounded)
        {
            animator.SetTrigger("Idle");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Idle");
        CinemachineShake[] cams =  FindObjectsOfType<CinemachineShake>();
        foreach (CinemachineShake cam in cams)
        {
            if(cam.enabled == true)
            {
                cam.ShakeCamera(3,.5f);
            }
        } 
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
