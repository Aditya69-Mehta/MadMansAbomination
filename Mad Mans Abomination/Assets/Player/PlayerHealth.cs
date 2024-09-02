using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerHealth = 10;
    DeathHandler deathHandler;


    public void TakeDamage(int damage){
        playerHealth -= damage;
        if(playerHealth <= 0){
            Debug.Log("Boy You Ded!!");
            deathHandler = GetComponent<DeathHandler>();
            deathHandler.DeathHandle();
        }
    }
}
