using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    [Header("Paroprites Of Ground")]
    [SerializeField] private GameObject road;
    [SerializeField] private Vector3 spawnPostion = new Vector3(0, 0, 0);
    [SerializeField] private float flt_LengthRoad;
    [SerializeField] private float flt_DiatanceBetweenTwoRoad = 1.5f;
    [SerializeField] private float flt_LevelLength;
    [SerializeField] private Transform transform_StartLevel;
    [SerializeField] private Transform transform_EndLevel;
    [SerializeField] private Transform transform_Player;
    [SerializeField] private float flt_ExtraGroundLength;
    [SerializeField] private float flt_OffsetStartGround;


    [Header("PropPerites Of Obstackle")]
    [SerializeField] private GameObject[] all_obstacle;
    private bool isFirstObstckleSpawn = true;
    [SerializeField] private float flt_FirstObstackleDistnce;
    [SerializeField] private float flt_MaxDistanceOfSpawnObstcleInZDirection;
    [SerializeField] private float flt_MinDistanceOfSpawnObstcleInZDirection;
    [SerializeField] private float flt_MaxDistanceOfSpawnObstcleInxDirection;
    [SerializeField] private float flt_MinDistanceOfSpawnObstcleInxDirection;
    [SerializeField] private float lastObstckleZPositon;

    [Header("Properites Of Ammo")]
    [SerializeField] private GameObject ammo;
    [SerializeField] private int noOfAmmoSpawn;
    [SerializeField] private float flt_MaxDistanceOfSpawnAmmosInZDirection;
    [SerializeField] private float flt_MinDistanceOfSpawnAmmosInZdirection;
    [SerializeField] private float flt_MinDistanceOfSpawnAmmosInXDireCtion;
    [SerializeField] private float flt_MaxDistanceOfSpawnAmmosInXDireCtion;
    [SerializeField] private LayerMask layerMaskForAmmo;
    [SerializeField] private float radiusOfCircle;
  
    



    // Start is called before the first frame update
    void Start()
    {
        SetRaceEndPostion();
        SpawnGround();
        SpawnObstackle();
        SpawnAmmo();

    }

    private void SpawnGround()
    {
        flt_LengthRoad = flt_LevelLength  + flt_ExtraGroundLength;
        spawnPostion = new Vector3(0, 0, -flt_OffsetStartGround);

            while (spawnPostion.z < flt_LengthRoad)
            {

                Instantiate(road, spawnPostion, transform.rotation);
                spawnPostion = new Vector3(spawnPostion.x, spawnPostion.y, spawnPostion.z + flt_DiatanceBetweenTwoRoad);
            }
       
    }

    private void SetRaceEndPostion()
    {
       
        transform_EndLevel.position = new Vector3(transform_EndLevel.position.x, transform_EndLevel.position.y
                                        ,transform_StartLevel.position.z + flt_LevelLength);
        RaceManger.instance.SettransformOfWinningBoundry(transform_EndLevel);
    }
  
    private void SpawnObstackle()
    {
        Vector3 postion = new Vector3(0, 0.5f,0);


        while (postion.z < transform_EndLevel.position.z)
        {
           
            float flt_XSpawnPostion = Random.Range(flt_MinDistanceOfSpawnObstcleInxDirection, flt_MaxDistanceOfSpawnObstcleInxDirection);
            float flt_Zpostion = Random.Range(flt_MinDistanceOfSpawnObstcleInZDirection, flt_MaxDistanceOfSpawnObstcleInZDirection);

            if (isFirstObstckleSpawn)
            {
                postion = new Vector3(flt_XSpawnPostion, 0.5f, transform_StartLevel.position.z + flt_FirstObstackleDistnce + flt_Zpostion);

                lastObstckleZPositon = transform_StartLevel.position.z + flt_FirstObstackleDistnce + flt_Zpostion;
                isFirstObstckleSpawn = false;
            }
            else
            {
                postion = new Vector3(flt_XSpawnPostion, 0.5f, lastObstckleZPositon + flt_Zpostion);
                lastObstckleZPositon = postion.z + flt_Zpostion;
            }
            if (postion.z>transform_EndLevel.position.z)
            {
                continue;
            }


            int index = Random.Range(0, all_obstacle.Length);
            Instantiate(all_obstacle[index], postion, transform.rotation);
        }
    }

    private void SpawnAmmo()
    {
        for (int i = 0; i < noOfAmmoSpawn; i++)
        {
           
            bool isgetPostion = false;
            while (!isgetPostion)
            {
                float Xposition = Random.Range(flt_MinDistanceOfSpawnAmmosInXDireCtion, flt_MaxDistanceOfSpawnAmmosInXDireCtion);
                float Zpostion = Random.Range(flt_MinDistanceOfSpawnAmmosInZdirection, flt_MaxDistanceOfSpawnAmmosInZDirection);
                Vector3 position = new Vector3(Xposition, 0, Zpostion);
             

                Collider[] hit;

                hit = Physics.OverlapSphere(position,radiusOfCircle,layerMaskForAmmo);

               
                if (hit.Length == 0)
                {
                    Instantiate(ammo, position, transform.rotation);
                    isgetPostion = true;
                }
              
            }
           
        }
    }
}
