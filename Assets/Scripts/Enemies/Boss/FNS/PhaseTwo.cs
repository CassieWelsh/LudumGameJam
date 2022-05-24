using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseTwo : StateMachineBehaviour
{
    private Boss _boss; 
    private int _nextPhaseHp;
    private float _coolDownTime;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = animator.gameObject.transform.parent.GetComponent<Boss>();
        _nextPhaseHp = (int) Mathf.Floor(_boss.maxHp * _boss.phaseThreePercentage);
        _coolDownTime = Time.time;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time > _coolDownTime)
        {
            _boss.Shoot();
            _coolDownTime = Time.time + _boss.projectileCooldown;
        }

        if (_boss.hp <= _nextPhaseHp)
        {
            animator.SetTrigger("PhaseThree");        
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
