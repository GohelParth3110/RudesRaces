using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteraction : MonoBehaviour
{
    [Header("Scripts")]
    private PlayerHealth playerHealth;
    private PlayerMovement playerMoveMnent;
    private PlayerShooting playerShooting;
    private AmmoSystem ammoSystem;


    [SerializeField] private bool isPlayer;
    private enemyMovement enemyMoveMent;
    private EnemyHealth enemyHealth;
    private EnemyShooting enemyShooting;
    [SerializeField] private Collider thisCollider;


    private string tag_Obstacles = "Obstacles";
    private string tag_WinningLine = "WinningLine";
   
    
    // Start is called before the first frame update
    void Start()
    {
    
        if (isPlayer)
        {
            playerHealth = GetComponent<PlayerHealth>();
            playerMoveMnent = GetComponent<PlayerMovement>();
            playerShooting = GetComponent<PlayerShooting>();
        }
        else
        {
            enemyHealth = GetComponent<EnemyHealth>();
            enemyMoveMent = GetComponent<enemyMovement>();
            enemyShooting = GetComponent<EnemyShooting>();
        }
     
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag_Obstacles))
        {
            ObstaclesProperites obstackleProperites = other.GetComponent<ObstaclesProperites>();
            SetDataOfWhenObstacleTrigger(obstackleProperites);
        }
        if (other.gameObject.CompareTag(tag_WinningLine))
        {
            FinishedRace();
            thisCollider.enabled = false;
        }
    }

    

 

    private void SetDataOfWhenObstacleTrigger(ObstaclesProperites obstaclesProperites)
    {
        if (isPlayer)
        {
            playerHealth.TakeDamage(obstaclesProperites.GetDamage());
            playerMoveMnent.SetPlayerMovementWhenPlayerTouchObstacle
                (obstaclesProperites.GetReduceSpeed(), obstaclesProperites.GetMaxTimeToReduceSpeed());
        }
        else
        {
            enemyHealth.TakeDamage(obstaclesProperites.GetDamage()); 
            enemyMoveMent.SetReduceSpeedWhenTriggerObstackle(
                obstaclesProperites.GetReduceSpeed(), obstaclesProperites.GetMaxTimeToReduceSpeed());
        }
    }

    private void FinishedRace()
    {
        RaceManger.instance.PlayerFinishedRace(transform.name, isPlayer);
       
        
        if (isPlayer)
        {          
            playerShooting.enabled = false;      
        }
        else
        {
            enemyShooting.enabled = false;     
           
            
        }
    }
}
