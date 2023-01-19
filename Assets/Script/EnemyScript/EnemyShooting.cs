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
    [SerializeField] private bool isenemyBulletSpawn;
    [SerializeField] private float flt_EnemyBulletFireRate;
    [SerializeField] private float flt_CurrentSpawnTime;
    private BulletRaycastHandler bulletRaycastHandler;
    [SerializeField] private Transform bulletSpawnPoint;
   
    private AmmoSystem ammoSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletRaycastHandler = GetComponent<BulletRaycastHandler>();
       
        ammoSystem = GetComponent<AmmoSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!RaceManger.instance.GetRaceStatus())
        {
            return;
        }
        GetInput();
        HandlingBulletFireRate();
    }

    private void GetInput()
    {
        if (bulletRaycastHandler.GetInputOfEnemy()>0  && isenemyBulletSpawn && ammoSystem.GetAmmo()>0)
        {
            GameObject currentenemyBullet = Instantiate(enemyBullet, bulletSpawnPoint.position, enemyBullet.transform.rotation);
            currentenemyBullet.GetComponent<EnemyBulletMovement>().SetBulletProperites(damageOfBullet,flt_PersantageofReduceSpeed,
                flt_MaxTimeToReduceSpeed);
            ammoSystem.SubtarctAmmoValue(1);
          
            isenemyBulletSpawn = false;
        }
    }
    private void HandlingBulletFireRate()
    {
        if (isenemyBulletSpawn)
        {
            return;
        }
        flt_CurrentSpawnTime += Time.deltaTime;
        if (flt_CurrentSpawnTime>flt_EnemyBulletFireRate)
        {
            flt_CurrentSpawnTime = 0;
            isenemyBulletSpawn = true;
        }
    }
}
