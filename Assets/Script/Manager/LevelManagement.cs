using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    [Header("-------Properties Of Grass------")]
    [SerializeField] private Transform transform_PlaceWhereGrassSpawn;
    [SerializeField] private Transform transform_PlaceWhereTreeSpawn;
    [SerializeField] private Transform transform_PlaceWhereBuildingPlace;
    [SerializeField] private Vector3 currentPostion;
    [SerializeField] private float flt_GrassOffset;
    [SerializeField] private int line;
    [SerializeField] private GameObject Grass;
    [SerializeField] private float yPostion;
    [SerializeField] private GameObject[] all_Tree;
    [SerializeField] private GameObject[] all_Building;
    [SerializeField] private float probabiltyOfBulidingSpawn;
    [SerializeField] private float probabilityOfTreeSpawn;
    [SerializeField] private int MaxScale;

    [Space]
    [Space]
    [Header("-------Paroprites Of Ground------")]
    [SerializeField] private GameObject startGround;
    [SerializeField] private GameObject road;
    [SerializeField] private Transform transform_PlaceWhereRoadSpawn;
    [SerializeField] private float flt_DiatanceBetweenTwoRoad = 1.5f;
    [SerializeField] private float flt_LevelLength;
    [SerializeField] private Transform transform_StartLevel;
    [SerializeField] private Transform transform_EndLevel;
    [SerializeField] private float flt_ExtraGroundLength;        // this value get extra ground after winning line
    private Vector3 spawnPostion = new Vector3(0, 0, 0);
    private float flt_LengthRoad;

    [Space]
    [Space]
    [Header("-------Paroprites Of Boundry------")]
    [SerializeField] private Transform transform_PlaceWhereBoundrySpawn;
    [SerializeField] private GameObject boundry;
    [SerializeField] private float flt_ZAxisOffsetBoundry = 20;
    [SerializeField] private float flt_yPostionofBoundry;
    [SerializeField] private float flt_BoundryPositionOffset;
    [SerializeField] private float flt_BoundryPositionY;
    [SerializeField] private Transform transform_LeftBoundry;
    [SerializeField] private Transform transform_RightBoundry;
   
   

    [Space]
    [Space]
    [Header("-------PropPerites Of Obstackle-------")]
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

    [Space]
    [Space]
    [Header("-------Properites Of Ammo------")]
    [SerializeField] private Transform transform_PlaceWherAmmoSpawn;
    [SerializeField] private GameObject ammo;
    [SerializeField] private int noOfAmmoSpawn;
    [SerializeField] private float flt_YoffsetForAmmo;
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
    public float GetBoundryPostion()
    {
        return flt_BoundryPositionOffset;
    }
   
    // Start is called before the first frame update
    void Start()
    {
      
        SetRaceEndPostion();
        SpawnGround();
        SpawnBoundry();
        SpawnGrass();
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
              GameObject current =   Instantiate(Grass, currentPostion, transform.rotation, transform_PlaceWhereGrassSpawn);

               
                if (i <line/2)
                {
                    SpawnTree(currentPostion);
                }
                else
                {
                    SpawnBuilding(currentPostion);
                }
                
                currentPostion = new Vector3(currentPostion.x, yPostion, currentPostion.z+flt_GrassOffset);
            }
            currentPostion = new Vector3(currentPostion.x- flt_GrassOffset, yPostion, 0);
        }

        currentPostion = new Vector3(flt_BoundryPositionOffset + (flt_GrassOffset / 2), yPostion, 0);
        for (int i = 0; i < line; i++)
        {
            while (currentPostion.z < flt_LengthRoad + 10)
            {
               GameObject current = Instantiate(Grass, currentPostion, transform.rotation, transform_PlaceWhereGrassSpawn);

               
                if (i < line / 2)
                {
                    SpawnTree(currentPostion);
                }
                else
                {
                    SpawnBuilding(currentPostion);
                }
                currentPostion = new Vector3(currentPostion.x, yPostion, currentPostion.z + flt_GrassOffset);
            }
            currentPostion = new Vector3(currentPostion.x + flt_GrassOffset, yPostion, 0);
        }
    }

    private void SpawnBuilding(Vector3 postion)
    {
        int index = Random.Range(0, 100);
        if (index <probabiltyOfBulidingSpawn)
        {
            int treeIndex = Random.Range(0, all_Building.Length);
            Vector3 currentTreePostion = new Vector3(postion.x, 0, postion.z);
         GameObject current = Instantiate(all_Building[treeIndex], currentTreePostion,
                                                        transform.rotation, transform_PlaceWhereBuildingPlace);
           
            int value = Random.Range(1, MaxScale);
            current.transform.localScale = new Vector3(10,10+ value, 10);
            
        }
    }

    private void SpawnTree(Vector3 postion)
    {
        int index = Random.Range(0, 100);
        if (index<probabilityOfTreeSpawn)
        {
            int treeIndex = Random.Range(0, all_Tree.Length);
            Vector3 currentTreePostion = new Vector3(postion.x, 0, postion.z);
            GameObject current = Instantiate(all_Tree[treeIndex], currentTreePostion, transform.rotation, transform_PlaceWhereTreeSpawn);
           
        }
    }

    private void SpawnGround()
    {
        spawnPostion = new Vector3(-73.25f, 0, 5);
        for (int i = 0; i < RaceManger.instance.GetNoOfRacerPostionInGame(); i++)
        {
           
         GameObject current = Instantiate(startGround, spawnPostion, transform.rotation,transform_PlaceWhereRoadSpawn);
          
            spawnPostion += new Vector3(14.65f, 0, 0);
        }
        flt_LengthRoad = flt_LevelLength  + flt_ExtraGroundLength;
        spawnPostion = new Vector3(0, 0,12.5f);

            while (spawnPostion.z < flt_LengthRoad)
            {

             GameObject current =  Instantiate(road, spawnPostion, transform.rotation, transform_PlaceWhereRoadSpawn);
            
            spawnPostion = new Vector3(spawnPostion.x, spawnPostion.y, spawnPostion.z + flt_DiatanceBetweenTwoRoad);
            }

    }
    private void SpawnBoundry()
    {
        transform_LeftBoundry.position = new Vector3(-flt_BoundryPositionOffset, 0, flt_LengthRoad / 2);
        transform_LeftBoundry.localScale = new Vector3(1, flt_BoundryPositionY, flt_LengthRoad);
        transform_RightBoundry.position = new Vector3(flt_BoundryPositionOffset, 0, flt_LengthRoad / 2);
        transform_RightBoundry.localScale = new Vector3(1, flt_BoundryPositionY, flt_LengthRoad);
        Vector3 spawnPostion = new Vector3(transform_LeftBoundry.position.x+2,0,0);
        while (spawnPostion.z < flt_LengthRoad)
        {
          GameObject current = Instantiate(boundry, spawnPostion, boundry.transform.rotation, 
              transform_PlaceWhereBoundrySpawn);
           

            spawnPostion = new Vector3(spawnPostion.x, spawnPostion.y, spawnPostion.z + flt_ZAxisOffsetBoundry);
        }
         spawnPostion = new Vector3(transform_RightBoundry.position.x + 2, 0, 0);
        while (spawnPostion.z < flt_LengthRoad)
        {
           GameObject current = Instantiate(boundry, spawnPostion, boundry.transform.rotation,
               transform_PlaceWhereBoundrySpawn);
            
            spawnPostion = new Vector3(spawnPostion.x, spawnPostion.y, spawnPostion.z + flt_ZAxisOffsetBoundry);
        }

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
                Vector3 position = new Vector3(Xposition, flt_YoffsetForAmmo, Zpostion);
             

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
