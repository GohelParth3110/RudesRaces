using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManger : MonoBehaviour
{
    public static RaceManger instance;

    [SerializeField] private List<Transform> list_RaceStartPositions;  // this list get All Spawn Postion of Racer
    
    [SerializeField] private GameObject[] enemy;      // enemy 
    [SerializeField] private bool hasRaceStarted = false;   // get Status Race
    
  
    [SerializeField] private int total_Postion = 11;
    [SerializeField] [Range(0, 11)] private int noOfRacerToSpawn;

    [SerializeField] private List<GameObject> list_Racers;
    [SerializeField] private float[] distnce;           // this array get Value of HowFar Racer of Winning Line
    [SerializeField] private List<string> list_RacerName;
    [SerializeField] private List<float> list_RacerCompleteTime;
    [SerializeField] private float currentTime;
    [SerializeField] private int noOfPlayerCompleteRace = 0;
    [SerializeField] private int playerRank;
    [SerializeField] private LevelManagement levelManagement;
    private Vector3 raceEndPosition;

    private void Awake()
    {
        instance = this;
    }

   
   
    #region Properites
    public bool GetRaceStatus()
    {
        return hasRaceStarted;
    }
    public int GetNoOfRacerPostionInGame()
    {
        return total_Postion;
    }

    public void RemoveGameObjectInList(GameObject current)
    {
        list_Racers.Remove(current);
    }


  


    #endregion

    void Start()
    {
        hasRaceStarted = false;
        distnce = new float[noOfRacerToSpawn];
        SpawnRacer();
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !hasRaceStarted)
        {
           
            hasRaceStarted = true;
        }

        if (!hasRaceStarted)
        {
            return;
        }

        CalculateTime();
        LiveStatus();
        raceEndPosition = levelManagement.GetEndPostion();

    }
    private void SpawnRacer()
    {
        int index = Random.Range(0, list_RaceStartPositions.Count);
        PlayerManager.instance.SpawnPlayer();
        PlayerManager.instance.GetPlayer().transform.position = list_RaceStartPositions[index].position;
        list_Racers.Add(PlayerManager.instance.GetPlayer());
        
        list_RaceStartPositions.RemoveAt(index);

        for (int i = 1; i < noOfRacerToSpawn; i++)
        {
            index = Random.Range(0, list_RaceStartPositions.Count);
            int enemyIndex = Random.Range(0, enemy.Length);
            GameObject currentEnemy = Instantiate(enemy[enemyIndex], list_RaceStartPositions[index].position,
              enemy[enemyIndex].transform.rotation);


            list_Racers.Add(currentEnemy);
            list_RaceStartPositions.RemoveAt(index);

        }
    }

    private void LiveStatus()
    {

        for (int i = 0; i < list_Racers.Count; i++)
        {
            distnce[i] = raceEndPosition.z - list_Racers[i].transform.position.z;
        }
    }
    private void CalculateTime()
    {
       
        currentTime += Time.deltaTime;

    }
    public void PlayerFinishedRace(string name, bool isplayer)
    {
        list_RacerName.Add(name);
        list_RacerCompleteTime.Add(currentTime);
        noOfPlayerCompleteRace++;
        if (isplayer)
        {
            playerRank = noOfPlayerCompleteRace;
        }
        if (noOfPlayerCompleteRace >= list_Racers.Count)
        {
            Debug.Log("GameOver");
            hasRaceStarted = false;
        }

    }
}
