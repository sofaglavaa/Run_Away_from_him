using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseBehavior : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    float attackRange = 2;
    float chaseRange = 10;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    //    Ускорение для погони
       agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 4;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(player.position);
        float distance = Vector3.Distance(animator.transform.position, player.position);

        // Атака при приближении к игроку
        if (distance < attackRange){
            animator.SetBool("isAttacking", true);
        }

        // Патрулирование
        if (distance > 10){
            animator.SetBool("isChasing", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(agent.transform.position);
        agent.speed = 2;
    }
}
