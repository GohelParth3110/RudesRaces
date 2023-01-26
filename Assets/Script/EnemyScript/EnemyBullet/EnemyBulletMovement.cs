using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    [Header("Paticle Vfx")]
    [SerializeField] private GameObject particle_BulletTouch;
   
    [Header("BulletData")]
    [SerializeField] private float flt_enemyBulletSpeed;
    [SerializeField] private float force;
    private float flt_DamageOfBullet;
    private float flt_ReduceSpeedInPercentage;
    private float flt_MaxTimeToReduceSpeed;

    // tags
    private string tag_Player = "Player";
    private string tag_Obstracles = "Obstacles";
    private string tag_Enemy = "Enemy";
    private string tag_Ammo = "Ammo";

    void Update()
    {
        BulletMotion();
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag_Ammo))
        {
            return;
        }
      
        Instantiate(particle_BulletTouch, transform.position, transform.rotation);
        if (other.gameObject.CompareTag(tag_Player))
        {
           
            Destroy(gameObject);
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
          
           
            
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(flt_DamageOfBullet);

               // other.GetComponent<PlayerMovement>().SetplayerSpeedReduceBulletTouch(flt_ReduceSpeedInPercentage, flt_MaxTimeToReduceSpeed);
            }
        }
        if (other.gameObject.CompareTag(tag_Obstracles))
        {
            other.GetComponent<ObstaclesProperites>().playVfx();
            Destroy(gameObject);
            Destroy(other.gameObject);

        }
        if (other.gameObject.CompareTag(tag_Enemy))
        {
           
            Destroy(gameObject);
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(flt_DamageOfBullet);
               // other.GetComponent<enemyMovement>().SetBulletTrigger(flt_ReduceSpeedInPercentage, flt_MaxTimeToReduceSpeed);
            }
        }

    }
   



    #region Properites
    public void SetBulletProperites(float damage, float reduceSpeed,float maxTime)
    {
        flt_DamageOfBullet = damage;
        flt_ReduceSpeedInPercentage = reduceSpeed;
        flt_MaxTimeToReduceSpeed = maxTime;
    }
    #endregion


    #region BulletMotion
    private void BulletMotion()
    {
        transform.Translate(Vector3.forward * flt_enemyBulletSpeed * Time.deltaTime);
    }
    #endregion
}
