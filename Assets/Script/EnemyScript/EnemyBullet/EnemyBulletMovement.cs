using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    [SerializeField] private float flt_enemyBulletSpeed;
    [SerializeField] private float flt_DamageOfBullet;
    private string tag_Player = "Player";
    private string tag_Obstracles = "Obstacles";
    private string tag_Enemy = "Enemy";
    [SerializeField] private float flt_PersantageReduceSpeed;
    [SerializeField] private float flt_MaxTimeToReduceSpeed;

   
    
    void Update()
    {
        BulletMotion();
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag_Player))
        {
            Destroy(gameObject);
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamageOfPlayer(flt_DamageOfBullet);
                other.GetComponent<PlayerMovement>().SetplayerSpeedReduceBulletTouch(flt_PersantageReduceSpeed, flt_MaxTimeToReduceSpeed);
            }
        }
        if (other.gameObject.CompareTag(tag_Obstracles))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);

        }
        if (other.gameObject.CompareTag(tag_Enemy))
        {
            Destroy(gameObject);
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamageOfEnemy(flt_DamageOfBullet);
                other.GetComponent<enemyMovement>().SetBulletTrigger(flt_PersantageReduceSpeed, flt_MaxTimeToReduceSpeed);
            }
        }

    }
   



    #region Properites
    public void SetBulletProperites(float damage, float reduceSpeed,float maxTime)
    {
        flt_DamageOfBullet = damage;
        flt_PersantageReduceSpeed = reduceSpeed;
        flt_MaxTimeToReduceSpeed = maxTime;
    }
    #endregion


    #region BulletMotion
    private void BulletMotion()
    {
        transform.Translate(-transform.forward * flt_enemyBulletSpeed * Time.deltaTime);
    }
    #endregion
}
