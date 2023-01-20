using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int flt_MaxHealth;
    [SerializeField] private float flt_CurrentEnemyHealth;
  
    private enemyMovement enemyMovement;

    private void Start()
    {
       
        flt_CurrentEnemyHealth = flt_MaxHealth;
        enemyMovement = GetComponent<enemyMovement>();
    }

    #region DamageHandling
    public void TakeDamage(float Damage)
    {
     
        flt_CurrentEnemyHealth -= Damage;
        if (flt_CurrentEnemyHealth<=0)
        {
            RaceManger.instance.RemoveGameObjectInList(this.gameObject);
            Destroy(gameObject, 0.5f);
        }
    }
    #endregion
}
