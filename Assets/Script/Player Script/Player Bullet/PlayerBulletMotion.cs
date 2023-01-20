using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletMotion : MonoBehaviour
{
    [SerializeField] private float flt_BulletSpeed;
     private float flt_CurrentDamage;
    private float persnatageOfReduceSpeed;
     private float maxTime;
   
    private string tag_Enemy = "Enemy";
    private string tag_Obstracles = "Obstacles";

    public void SetBulletDamage(float damage, float RecduceSpeed, float timeToReDuceSpeed)
    {
        flt_CurrentDamage = damage;
        persnatageOfReduceSpeed = RecduceSpeed;
        maxTime = timeToReDuceSpeed;
    }
    private void Update()
    {
        BulletMovment();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag_Enemy))
        {
            Destroy(gameObject);
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
           
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(flt_CurrentDamage);
                other.gameObject.GetComponent<enemyMovement>().SetBulletTrigger(persnatageOfReduceSpeed, maxTime);
            }
        }

        if (other.gameObject.CompareTag(tag_Obstracles))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);

        }
    }
   

    private void BulletMovment()
    {
        transform.Translate(Vector3.forward * flt_BulletSpeed * Time.deltaTime);
    }
}
