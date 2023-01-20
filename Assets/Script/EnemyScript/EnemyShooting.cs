using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Bullet properites")]
    [SerializeField] private int damageOfBullet;            
    [SerializeField] private float flt_PersantageofReduceSpeed;
    [SerializeField] private float flt_MaxTimeToReduceSpeed;

    [Header("BulletShooting Data")]
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private Transform bulletSpawnPoint;
    private float currentBulletWaitTime = 0f;
    private float bulletFireRate = 1f;
   
    private AmmoSystem ammoSystem;

    private void Start()
    {
        ammoSystem = GetComponent<AmmoSystem>();
    }

    private void Update()
    {
        currentBulletWaitTime += Time.deltaTime;
    }

    public void Fire()
    {
        if(currentBulletWaitTime < bulletFireRate)
        {
            return;
        }

        currentBulletWaitTime = 0f;
        GameObject currentenemyBullet = Instantiate(enemyBullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        currentenemyBullet.GetComponent<EnemyBulletMovement>().SetBulletProperites(damageOfBullet, flt_PersantageofReduceSpeed,
            flt_MaxTimeToReduceSpeed);
        ammoSystem.SubtarctAmmoValue(1);
    }

   
}
