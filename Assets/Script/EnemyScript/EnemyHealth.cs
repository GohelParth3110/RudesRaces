using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int max_EnemyHealth;
    [SerializeField] private float flt_CurrentEnemyHealth;
  
    private enemyMovement enemyMovement;

    private void Start()
    {
       
        flt_CurrentEnemyHealth = max_EnemyHealth;
        enemyMovement = GetComponent<enemyMovement>();
    }

    #region DamageHandling
    public void TakeDamageOfEnemy(float Damage)
    {
        if (!enemyMovement.GetIsEnemyMove())
        {
            return;
        }
        flt_CurrentEnemyHealth -= Damage;
        if (flt_CurrentEnemyHealth<=0)
        {
            RaceManger.instance.RemoveGameObjectInList(this.gameObject);
            Destroy(gameObject, 0.5f);
        }
    }
    #endregion
}
