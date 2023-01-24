using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteractionEnemy : MonoBehaviour
{

    [Header("Scripts")]
    private enemyMovement enemyMoveMent;
    private EnemyHealth enemyHealth;
    private EnemyShooting enemyShooting;
    private string tag_Obstacles = "Obstacles";
    private string tag_WinningLine = "WinningLine";


    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyMoveMent = GetComponent<enemyMovement>();
        enemyShooting = GetComponent<EnemyShooting>();
    }

    public  enemyMovement GetEnemyMoveMent()
    {
        return enemyMoveMent;
    }
    public EnemyShooting GetEnemyShooting()
    {
        return enemyShooting;
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
            Debug.Log("Hii");
            enemyMoveMent.SetEnemyStatus(true);
            enemyHealth.GetCollider().enabled = false;
            RaceManger.instance.FinishedRace(this.gameObject.name, false);
            enemyShooting.enabled = false;
        }
    }





    private void SetDataOfWhenObstacleTrigger(ObstaclesProperites obstaclesProperites)
    {
            enemyHealth.TakeDamage(obstaclesProperites.GetDamage());
            enemyMoveMent.SetReduceSpeedWhenTriggerObstackle(
             obstaclesProperites.GetReduceSpeed(), obstaclesProperites.GetMaxTimeToReduceSpeed());
        
    }

   
}
