using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseOne : StateMachineBehaviour
{
    private int nextPhaseHp;
    private Boss _boss; 
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = animator.gameObject.transform.parent.GetComponent<Boss>();
        nextPhaseHp = (int) Mathf.Floor(_boss.hp * _boss.phaseTwoPercentage);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_boss.hp <= nextPhaseHp)
        {
            _boss.ShootHead();
            animator.SetTrigger("PhaseTwo");
        }
    }

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
