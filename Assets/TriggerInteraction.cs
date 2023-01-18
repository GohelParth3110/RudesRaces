using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteraction : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private PlayerMovement playerMoveMnent;
    private PlayerShooting playerShooting;
    private AmmoSystem ammoSystem;
    [SerializeField] private bool isPlayer;
    private enemyMovement enemyMoveMent;
    private EnemyHealth enemyHealth;
    private EnemyShooting enemyShooting;
    private Rigidbody Rb;
    [SerializeField] private Collider thisCollider;
    private string tag_Obstacles = "Obstacles";
    private string tag_WinningLine = "WinningLine";
   
    
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
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

    

    public void SetMove(bool value)
    {
        if (isPlayer)
        {
            playerMoveMnent.SetIsPLayerMove(value);
        }
        else
        {
           
            enemyMoveMent.SetIsEnemyMove(value);
        }
    }

    private void SetDataOfWhenObstacleTrigger(ObstaclesProperites obstaclesProperites)
    {
        if (isPlayer)
        {
            playerHealth.TakeDamageOfPlayer(obstaclesProperites.GetDamage());
            playerMoveMnent.SetPlayerMovementWhenPlayerTouchObstacle
                (obstaclesProperites.GetReduceSpeed(), obstaclesProperites.GetMaxTimeToReduceSpeed());
        }
        else
        {
            enemyHealth.TakeDamageOfEnemy(obstaclesProperites.GetDamage());
            enemyMoveMent.SetReduceSpeedWhenTriggerObstackle(
                obstaclesProperites.GetReduceSpeed(), obstaclesProperites.GetMaxTimeToReduceSpeed());
        }
    }

    private void FinishedRace()
    {
        RaceManger.instance.PlayerFinishedRace(transform.name, isPlayer);
        Rb.velocity = new Vector3(0, 0, 0);
        
        if (isPlayer)
        {
            playerMoveMnent.enabled = false;
            playerShooting.enabled = false;

        }
        else
        {
            enemyShooting.enabled = false;
            enemyMoveMent.enabled = false;
            
        }
    }
}
