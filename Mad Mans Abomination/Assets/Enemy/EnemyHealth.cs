using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] float despawnTimer = 5f;

    bool isDead = false;

    public bool IsDead {get{return isDead;}}

    public void TakeDamage(int damage){
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        if(hitPoints <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        if(!isDead){
            isDead = true;
            GetComponent<Animator>().SetTrigger("isDying");
            yield return new WaitForSeconds(despawnTimer);
            Destroy(gameObject);
        }
    }
}
