using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehavior : StateMachineBehaviour
{
    float timer; // счетчик
    // Создаем лист точек по которым передвигается противник
    List<Transform> points = new List<Transform>();
    NavMeshAgent agent;

    Transform player;
    float chaseRange = 10;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       timer = 0;
        // Работа с точками патрулирования
        Transform pointsObject = GameObject.FindGameObjectWithTag("Points").transform;
        foreach (Transform t in pointsObject){
            points.Add(t);
        }
        // Задаем перемещения объекта к точкам
        agent = animator.GetComponent<NavMeshAgent>();
        agent.SetDestination(points[0].position);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Движемся к точкам в случайном порядке
       if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(points[Random.Range(0, points.Count)].position);
        // После 15 секунд отключаем анимацию патрулирования
        timer += Time.deltaTime;
        if (timer > 15)
            animator.SetBool("isPatrolling", false);
        // Включаем погоню
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance < chaseRange)
            animator.SetBool("isChasing", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Остановка объекта при отключении патрулирования
       agent.SetDestination(agent.transform.position);
    }
}
