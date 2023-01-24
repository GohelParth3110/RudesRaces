using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    [Header("Properties Of Grass")]
    [SerializeField] private Transform transform_PlaceWhereGrassSpawn;
    [SerializeField] private Vector3 currentPostion;
    [SerializeField] private float flt_GrassOffset;
    [SerializeField] private int line;
    [SerializeField] private GameObject Grass;
    [SerializeField] private float yPostion;

    [Header("Paroprites Of Ground")]
    [SerializeField] private GameObject startGround;
    [SerializeField] private GameObject road;
    [SerializeField] private Transform transform_PlaceWhereRoadSpawn;
    [SerializeField] private float flt_DiatanceBetweenTwoRoad = 1.5f;
    [SerializeField] private float flt_LevelLength;
    [SerializeField] private Transform transform_StartLevel;
    [SerializeField] private Transform transform_EndLevel;
    [SerializeField] private float flt_ExtraGroundLength;        // this value get extra ground after winning line
    [SerializeField] private float flt_BoundryPositionOffset;
    [SerializeField] private float flt_BoundryPositionY;
    [SerializeField] private Transform transform_LeftBoundry;
    [SerializeField] private Transform transform_RightBoundry;
    private Vector3 spawnPostion = new Vector3(0, 0, 0);
    private float flt_LengthRoad;


    [Header("PropPerites Of Obstackle")]
    [SerializeField] private Transform transform_PlaceWhereObstackleSpawn;
    [SerializeField] private GameObject[] all_obstacle;
    private bool isFirstObstckleSpawn = true;
    [SerializeField] private float flt_FirstObstackleDistnce;  
    [SerializeField] private float flt_LastObstackleDistance;
    [SerializeField] private float flt_MaxDistanceOfSpawnObstcleInZDirection;
    [SerializeField] private float flt_MinDistanceOfSpawnObstcleInZDirection;
    [SerializeField] private float flt_MaxDistanceOfSpawnObstcleInxDirection;
    [SerializeField] private float flt_MinDistanceOfSpawnObstcleInxDirection;
    [SerializeField] private float lastObstckleZPositon;

    [Header("Properites Of Ammo")]
    [SerializeField] private Transform transform_PlaceWherAmmoSpawn;
    [SerializeField] private GameObject ammo;
    [SerializeField] private int noOfAmmoSpawn;
    [SerializeField] private float flt_MaxDistanceOfSpawnAmmosInZDirection;
    [SerializeField] private float flt_MinDistanceOfSpawnAmmosInZdirection;
    [SerializeField] private float flt_MinDistanceOfSpawnAmmosInXDireCtion;
    [SerializeField] private float flt_MaxDistanceOfSpawnAmmosInXDireCtion;
    [SerializeField] private LayerMask layerMaskForAmmo;
    [SerializeField] private float radiusOfCircle;
  
    
    public Vector3 GetEndPostion()
    {
        return transform_EndLevel.position;
    }


    // Start is called before the first frame update
    void Start()
    {
      
        SetRaceEndPostion();
        SpawnGround();
       // SpawnGrass();
        SpawnObstackle();
        SpawnAmmo();

    }

    private void SpawnGrass()
    {
        currentPostion = new Vector3(-flt_BoundryPositionOffset-(flt_GrassOffset/2), yPostion, 0);

        for (int i = 0; i < line ; i++)
        {
            while (currentPostion.z < flt_LengthRoad + 10)
            {
                Instantiate(Grass, currentPostion, transform.rotation, transform_PlaceWhereGrassSpawn);
                currentPostion = new Vector3(currentPostion.x, yPostion, currentPostion.z+flt_GrassOffset);
            }
            currentPostion = new Vector3(currentPostion.x- flt_GrassOffset, yPostion, 0);
        }

        currentPostion = new Vector3(flt_BoundryPositionOffset + (flt_GrassOffset / 2), yPostion, 0);
        for (int i = 0; i < line; i++)
        {
            while (currentPostion.z < flt_LengthRoad + 10)
            {
                Instantiate(Grass, currentPostion, transform.rotation, transform_PlaceWhereGrassSpawn);
                currentPostion = new Vector3(currentPostion.x, yPostion, currentPostion.z + flt_GrassOffset);
            }
            currentPostion = new Vector3(currentPostion.x + flt_GrassOffset, yPostion, 0);
        }
    }
    private void SpawnGround()
    {
        spawnPostion = new Vector3(-73.25f, 0, 5);
        for (int i = 0; i < RaceManger.instance.GetNoOfRacerPostionInGame(); i++)
        {
           
            Instantiate(startGround, spawnPostion, transform.rotation,transform_PlaceWhereRoadSpawn);
            spawnPostion += new Vector3(14.65f, 0, 0);
        }
        flt_LengthRoad = flt_LevelLength  + flt_ExtraGroundLength;
        spawnPostion = new Vector3(0, 0,10);

            while (spawnPostion.z < flt_LengthRoad)
            {

                Instantiate(road, spawnPostion, transform.rotation, transform_PlaceWhereRoadSpawn);
                spawnPostion = new Vector3(spawnPostion.x, spawnPostion.y, spawnPostion.z + flt_DiatanceBetweenTwoRoad);
            }

        transform_LeftBoundry.position = new Vector3(-flt_BoundryPositionOffset, 0, flt_LengthRoad / 2);
        transform_LeftBoundry.localScale = new Vector3(1, flt_BoundryPositionY, flt_LengthRoad);
        transform_RightBoundry.position = new Vector3(flt_BoundryPositionOffset, 0, flt_LengthRoad / 2);
        transform_RightBoundry.localScale = new Vector3(1, flt_BoundryPositionY, flt_LengthRoad);


    }

    

    private void SetRaceEndPostion()
    {
       
        transform_EndLevel.position = new Vector3(transform_EndLevel.position.x, transform_EndLevel.position.y
                                        ,transform_StartLevel.position.z + flt_LevelLength);
       
    }
  
    private void SpawnObstackle()
    {
        Vector3 postion = new Vector3(0, 0,0);


        while (postion.z < transform_EndLevel.position.z)
        {
           
            float flt_XSpawnPostion = Random.Range(flt_MinDistanceOfSpawnObstcleInxDirection, flt_MaxDistanceOfSpawnObstcleInxDirection);
            float flt_Zpostion = Random.Range(flt_MinDistanceOfSpawnObstcleInZDirection, flt_MaxDistanceOfSpawnObstcleInZDirection);

            if (isFirstObstckleSpawn)
            {
                postion = new Vector3(flt_XSpawnPostion,0, transform_StartLevel.position.z + flt_FirstObstackleDistnce + flt_Zpostion);

                lastObstckleZPositon = transform_StartLevel.position.z + flt_FirstObstackleDistnce + flt_Zpostion;
                isFirstObstckleSpawn = false;
            }
            else
            {
                postion = new Vector3(flt_XSpawnPostion, 0, lastObstckleZPositon + flt_Zpostion);
                lastObstckleZPositon = postion.z + flt_Zpostion;
            }
            if (postion.z>transform_EndLevel.position.z- flt_LastObstackleDistance)
            {
                continue;
            }


            int index = Random.Range(0, all_obstacle.Length);
            Instantiate(all_obstacle[index], postion, transform.rotation, transform_PlaceWhereObstackleSpawn);
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
                Vector3 position = new Vector3(Xposition, 0.5f, Zpostion);
             

                Collider[] hit;

                hit = Physics.OverlapSphere(position,radiusOfCircle,layerMaskForAmmo);

               
                if (hit.Length == 0)
                {
                    Instantiate(ammo, position, transform.rotation, transform_PlaceWherAmmoSpawn);
                    isgetPostion = true;
                }
              
            }
           
        }
    }
}
