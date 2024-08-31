using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;

    NavMeshAgent navMeshAgent;
    float distFromTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        distFromTarget = Vector3.Distance(target.position, transform.position);

        if(isProvoked){
            EngageTarget();
        }else if(distFromTarget <= chaseRange){
            isProvoked = true;
        }
    }

    void EngageTarget(){
        if(distFromTarget >= navMeshAgent.stoppingDistance){
            navMeshAgent.SetDestination(target.position);
        }
        if(distFromTarget <= navMeshAgent.stoppingDistance){
            Debug.Log("Attacking!");
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

    }
}
