using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 1f;

    NavMeshAgent navMeshAgent;
    float distFromTarget = Mathf.Infinity;
    bool isProvoked = false;
    Animator animator;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if(GetComponent<EnemyHealth>().IsDead){
            enabled = false;
            navMeshAgent.enabled = false;
        }

        distFromTarget = Vector3.Distance(target.position, transform.position);

        if(isProvoked){
            EngageTarget();
        }else if(distFromTarget <= chaseRange){
            isProvoked = true;
        }
    }

    void EngageTarget(){ // Enemy Follow

        FaceTarget();

        if(distFromTarget >= navMeshAgent.stoppingDistance){
            animator.SetBool("isRunning", true);
            navMeshAgent.SetDestination(target.position);
        }else if(distFromTarget <= navMeshAgent.stoppingDistance){
            animator.SetBool("isRunning", false);
            animator.SetTrigger("isAttacking");
            // Debug.Log("Attacking!");
        }

        if(distFromTarget > chaseRange){
            isProvoked = false;
        }
    }

    public void OnDamageTaken(){
        isProvoked = true;
    }

    void FaceTarget(){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void OnDrawGizmosSelected(){ // Enemy Range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

    }
}
