using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject player_Bullet;
    [SerializeField] private int damageOfBullet;
    private bool isBulletSpawnTime;
    [SerializeField] private float flt_CurrentSpawnBulletTime;
   [SerializeField] private float flt_BulletFirerate;
    private CharacterAnimation characterAnimation;
    private AmmoSystem ammoSystem;

    private void Start()
    {
        characterAnimation = GetComponent<CharacterAnimation>();
        ammoSystem = GetComponent<AmmoSystem>();
    }
    private void Update()
    {
        //if (!RaceManger.instance.GetRaceStatus())
        //{
        //    return;
        //}
        FireBullet();
        HandlingBulletSpawnTime();
    }

    #region BulletSpawnSystem
    private void FireBullet()
    {
        if (Input.GetKey(KeyCode.F) && isBulletSpawnTime && ammoSystem.GetAmmo() >0)
        {
            GameObject currentBullet =   Instantiate(player_Bullet, transform.position, player_Bullet.transform.rotation);
            currentBullet.GetComponent<PlayerBulletMotion>().SetBulletDamage(damageOfBullet);
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
