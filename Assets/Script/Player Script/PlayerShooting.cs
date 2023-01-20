using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Scripts")]
    private AmmoSystem ammoSystem;

    [Header("Bullet")]
    [SerializeField] private GameObject player_Bullet;
    [SerializeField] private Transform bulletSpawnPostion;
    [SerializeField] private Transform tower;
    [SerializeField] private int damageOfBullet;
    [SerializeField] private float flt_ReduceSpeedInPercentage;
    [SerializeField] private float flt_MaxTimeToReduceSpeed;

    private float flt_CurrentSpawnBulletTime;
    private float flt_BulletFirerate;
    


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
        flt_CurrentSpawnBulletTime += Time.deltaTime;

        if (ammoSystem.GetAmmo() <= 0)
        {
            return; // no ammo
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(flt_CurrentSpawnBulletTime < flt_BulletFirerate)
            {
                return; // wait
            }

            flt_CurrentSpawnBulletTime = 0f;
            GameObject currentBullet =   Instantiate(player_Bullet, bulletSpawnPostion.position, bulletSpawnPostion.rotation);
            currentBullet.GetComponent<PlayerBulletMotion>().SetBulletDamage(damageOfBullet,flt_ReduceSpeedInPercentage,flt_MaxTimeToReduceSpeed);
            ammoSystem.SubtarctAmmoValue(1);
         
        }
    }


  

    #endregion

}
