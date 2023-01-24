using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteractionPlayer : MonoBehaviour
{

    [Header("Scripts")]
    private PlayerHealth playerHealth;
    private PlayerShooting playerShooting;
    private PlayerMovement playerMoveMnent;
    private string tag_Obstacles = "Obstacles";
    private string tag_WinningLine = "WinningLine";


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerMoveMnent = GetComponent<PlayerMovement>();
        playerShooting = GetComponent<PlayerShooting>();
    }

    public PlayerMovement GetPlayerMovement()
    {
        return playerMoveMnent;
    }
    public PlayerShooting GetPlayerShooting()
    {
        return playerShooting;
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
            RaceManger.instance.FinishedRace(this.gameObject.name, true);
            playerHealth.GetCollider().enabled = false;
            playerMoveMnent.SetPlayerRaceStatus(false);
            playerShooting.enabled = false;
        }
    }

    private void SetDataOfWhenObstacleTrigger(ObstaclesProperites obstaclesProperites)
    {
       
        playerHealth.TakeDamage(obstaclesProperites.GetDamage());
        playerMoveMnent.SetPlayerMovementWhenPlayerTouchObstacle
            (obstaclesProperites.GetReduceSpeed(), obstaclesProperites.GetMaxTimeToReduceSpeed());
     
       
    }

   
}
