using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject player_Bullet;
    [SerializeField] private int damageOfBullet;
    [SerializeField] private float flt_PersantageReduceSpeed;
    [SerializeField] private float flt_MaxTimeToReduceSpeed;
    private bool isBulletSpawnTime;
    [SerializeField] private float flt_CurrentSpawnBulletTime;
   [SerializeField] private float flt_BulletFirerate;
    [SerializeField] private Transform bulletSpawnPoint;
    private AmmoSystem ammoSystem;

    [Header("Touch Bullet")]
    [SerializeField] private Transform bulletSpawnPostion;
    [SerializeField] private Transform tower;
  

    private void Start()
    {
       
        ammoSystem = GetComponent<AmmoSystem>();
    }
    private void Update()
    {
        RotateTower();
        
        if (!RaceManger.instance.GetRaceStatus())
        {
            return;
        }
        FireBullet();
        HandlingBulletSpawnTime();
    }
    private void RotateTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            tower.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

    }
   


    #region BulletSpawnSystem
    private void FireBullet()
    {
        if (Input.GetKey(KeyCode.F) && isBulletSpawnTime && ammoSystem.GetAmmo() >0)
        {
            GameObject currentBullet =   Instantiate(player_Bullet, bulletSpawnPostion.position, player_Bullet.transform.rotation);
            currentBullet.GetComponent<PlayerBulletMotion>().SetBulletDamage(damageOfBullet,flt_PersantageReduceSpeed,flt_MaxTimeToReduceSpeed);
            ammoSystem.SubtarctAmmoValue(1);

           
            isBulletSpawnTime = false;

        }
    }


    
    private void HandlingBulletSpawnTime()
    {
        if (isBulletSpawnTime)
        {
            return;
        }
        flt_CurrentSpawnBulletTime += Time.deltaTime;
        if (flt_CurrentSpawnBulletTime >flt_BulletFirerate)
        {
            isBulletSpawnTime = true;
            flt_CurrentSpawnBulletTime = 0;
        }

    }

    #endregion

}
