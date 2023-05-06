using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    private NavMeshAgent AI_Agent;
    private GameObject Player;
    public GameObject Panel_GaveOver;

    public Transform[] WayPoints;
    public int Current_Patch;

    public enum AI_State { Patrol, Stay, Chase };
    public AI_State AI_Enemy;

    private Transform Last_point;
    public bool Check_LastPoint;
    float i_stay;

    void Start()
    {
        AI_Agent = gameObject.GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (Check_LastPoint == false)
        {
            if (AI_Enemy == AI_State.Patrol)
            {
                AI_Agent.Resume();
                // gameObject.GetComponent<Animator>().SetBool("Move", true);
                AI_Agent.SetDestination(WayPoints[Current_Patch].transform.position);
                float Patch_Dist = Vector3.Distance(WayPoints[Current_Patch].transform.position, gameObject.transform.position);
                if (Patch_Dist < 2)
                {
                    Current_Patch++;
                    Current_Patch = Current_Patch % WayPoints.Length;
                }
            }
            if (AI_Enemy == AI_State.Stay)
            {
                // gameObject.GetComponent<Animator>().SetBool("Move", false);
                AI_Agent.Stop();
            }
            if (AI_Enemy == AI_State.Chase)
            {
                AI_Agent.Resume();
                // gameObject.GetComponent<Animator>().SetBool("Move", true);
                if (gameObject.GetComponent<FieldOfView>().canSeePlayer == false)
                {
                    Last_point = Player.transform;
                    Check_LastPoint = true;
                }
                else
                {
                    AI_Agent.SetDestination(Player.transform.position);
                }
            }
        }
        else
        {
            AI_Agent.Resume();
            i_stay += 1 * Time.deltaTime;
            float Point_Dist = Vector3.Distance(Last_point.transform.position, gameObject.transform.position);
            if (Point_Dist <1 || i_stay >= 7)
            {
                Check_LastPoint = false;
                AI_Enemy = AI_State.Patrol;
                i_stay = 0;
            }
            else
            {
                AI_Agent.Resume();
                // gameObject.GetComponent<Animator>().SetBool("Move", true);

            }
        }

        float Dist_Player = Vector3.Distance(Player.transform.position, gameObject.transform.position);
        if (Dist_Player < 2)
        {
            Player.SetActive(false);
            Panel_GaveOver.SetActive(true);
        }
    }
}
