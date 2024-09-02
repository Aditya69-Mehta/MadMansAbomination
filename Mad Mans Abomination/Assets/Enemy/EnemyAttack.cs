using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    
    [SerializeField] int damage = 2;
    PlayerHealth playerHealth;

    void Start(){
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent(){
        Debug.Log("Attacking");
        playerHealth.TakeDamage(damage);
    }
}
