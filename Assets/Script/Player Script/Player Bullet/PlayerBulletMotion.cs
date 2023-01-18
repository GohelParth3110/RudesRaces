using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletMotion : MonoBehaviour
{
    [SerializeField] private float flt_BulletSpeed;
    [SerializeField] private float flt_CurrentDamage;
   
    private string tag_Enemy = "Enemy";
    private string tag_Obstracles = "Obstacles";

    public void SetBulletDamage(float damage)
    {
        flt_CurrentDamage = damage;
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
                enemyHealth.TakeDamageOfEnemy(flt_CurrentDamage);
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
        transform.Translate(-transform.forward * flt_BulletSpeed * Time.deltaTime);
    }
}
