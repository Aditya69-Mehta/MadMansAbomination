using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{

    [SerializeField] AmmoType ammoType;
    [SerializeField] int ammoAmount;



    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            // Debug.Log("Ammo Lele");
            ProcessPickup();
        }
    }

    void ProcessPickup(){
        FindObjectOfType<Ammo>().IncreaseAmmo(ammoType, ammoAmount);
        Destroy(gameObject);
    }
}
