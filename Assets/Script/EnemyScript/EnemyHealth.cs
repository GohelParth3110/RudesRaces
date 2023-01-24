using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int flt_MaxHealth;
    [SerializeField] private float flt_CurrentEnemyHealth;
    private Collider thisCollider;
    private TriggerInteractionEnemy TriggerInteractionEnemy;
    
    

    private void Start()
    {
        thisCollider = GetComponent<Collider>();
        flt_CurrentEnemyHealth = flt_MaxHealth;
        TriggerInteractionEnemy = GetComponent<TriggerInteractionEnemy>();
    }

   
    public void TakeDamage(float Damage)
    {
     
        flt_CurrentEnemyHealth -= Damage;
        if (flt_CurrentEnemyHealth<=0)
        {
            thisCollider.enabled = false;
            TriggerInteractionEnemy.GetEnemyMoveMent().enabled = false;
            TriggerInteractionEnemy.GetEnemyShooting().enabled = false;
            StartCoroutine(ResetEnemy());
        }
    }

    IEnumerator ResetEnemy()
    {
        yield return new WaitForSeconds(1);
        flt_CurrentEnemyHealth = flt_MaxHealth;
        thisCollider.enabled = true;
        TriggerInteractionEnemy.GetEnemyMoveMent().enabled = true;
        TriggerInteractionEnemy.GetEnemyShooting().enabled = true;
        TriggerInteractionEnemy.GetEnemyMoveMent().ResetEnemyMoveMent();
        
    }
   public Collider GetCollider()
    {
        return thisCollider;
       
    }
   
}
