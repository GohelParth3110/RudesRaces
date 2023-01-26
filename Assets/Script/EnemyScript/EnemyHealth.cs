using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject particle_ShieldVFX;
    [SerializeField] private GameObject particle_DieVfx;
    [SerializeField] private int flt_MaxHealth;
    [SerializeField] private float flt_CurrentEnemyHealth;
    [SerializeField] private GameObject body;
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
            Instantiate(particle_DieVfx, transform.position, transform.rotation);
            body.SetActive(false);
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
     
        body.SetActive(true);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        particle_ShieldVFX.SetActive(true);
        StartCoroutine(ClosetShieldVfx());
        TriggerInteractionEnemy.GetEnemyMoveMent().enabled = true;
        TriggerInteractionEnemy.GetEnemyShooting().enabled = true;
        TriggerInteractionEnemy.GetEnemyMoveMent().ResetEnemyMoveMent();
        
    }
    IEnumerator ClosetShieldVfx()
    {
        yield return new WaitForSeconds(1);
        particle_ShieldVFX.SetActive(false);
        thisCollider.enabled = true;
       
    }
   public Collider GetCollider()
    {
        return thisCollider;
       
    }
   
}
