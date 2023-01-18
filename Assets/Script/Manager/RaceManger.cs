using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManger : MonoBehaviour
{
    public static RaceManger instance;
    [SerializeField] private List<Transform> list_RacerPostion;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] enemy;
   [SerializeField] private bool isRaceStart = false;
    private bool isPlayerSpawn =true;
    private Transform transform_RacerWinningPosition;
    [SerializeField] [Range(0,11)] private int noOfRacerToSpawn;
    [SerializeField] private List<GameObject> list_Racers;
    [SerializeField] private float[] distnce;
    [SerializeField] private List<string> list_RacerName;
    [SerializeField] private List<float> list_RacerCompleteTime;
    [SerializeField] private float currentTime;
    [SerializeField] private int noOfPlayerCompleteRace = 0;

    [SerializeField] private  int playerRank;

    private void Awake()
    {
        instance = this;
    }
    public int GetNoOfRacerInGame()
    {
        return noOfRacerToSpawn;
    }

    public void RemoveGameObjectInList(GameObject current)
    {
        list_Racers.Remove(current);
    }
   

    public  void SettransformOfWinningBoundry(Transform transform)
    {
        transform_RacerWinningPosition = transform;
    }
   
    void Start()
    {
        isRaceStart = false;
        distnce = new float[noOfRacerToSpawn];
        SpawnRacer();
       
        
    }
    void Update()
    {
       
        CalculateTime();
        LiveStatus();
        if (Input.GetKeyDown(KeyCode.Space) && !isRaceStart)
        {
            for (int i = 0; i < list_Racers.Count; i++)
            {
                list_Racers[i].GetComponent<TriggerInteraction>().SetMove(true);
            }
            isRaceStart = true;
        }

    }

    private void LiveStatus() 
    {

        for (int i = 0; i < list_Racers.Count; i++)
        {
            distnce[i] = transform_RacerWinningPosition.position.z - list_Racers[i].transform.position.z;

        }
    }

    private void CalculateTime()
    {
        if (!isRaceStart)
        {
            return;
        }
        currentTime += Time.deltaTime;

    }

    #region Properites
    public bool GetRaceStatus()
    {
        return isRaceStart;
    }
   
    #endregion



    #region StartRaceFunction

   
    private void SpawnRacer()
    {
        for (int i = 0; i < noOfRacerToSpawn; i++)
        {
           

           
         
            if (isPlayerSpawn)
            {
                PlayerManager.instance.SpawnPlayer();
                PlayerManager.instance.GetPlayer().transform.position = list_RacerPostion[i].position;
                list_Racers.Add(PlayerManager.instance.GetPlayer());
                isPlayerSpawn = false;
                list_RacerPostion.RemoveAt(i);
            }
            else
            {
                int index = Random.Range(0, list_RacerPostion.Count);
                int enemyIndex = Random.Range(0, enemy.Length);
               GameObject currentEnemy =  Instantiate(enemy[enemyIndex], list_RacerPostion[index].position,
                 enemy[enemyIndex].transform.rotation);
               
                
                list_Racers.Add(currentEnemy);
                list_RacerPostion.RemoveAt(index);
            }

            
        }
    }
    #endregion

    public void PlayerFinishedRace(string name, bool isplayer)
    {
        list_RacerName.Add(name);
        list_RacerCompleteTime.Add(currentTime);
        noOfPlayerCompleteRace++;
        if (isplayer)
        {
            playerRank = noOfPlayerCompleteRace;
        }

        if (noOfPlayerCompleteRace >=list_Racers.Count)
        {
            Debug.Log("GameOver");
            isRaceStart = false;
        }
        
    }
}
