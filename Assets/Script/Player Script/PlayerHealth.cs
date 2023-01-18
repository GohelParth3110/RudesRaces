using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int max_playerHealth;
    [SerializeField] private float flt_CurrentPlayerHealth;
   
    private PlayerMovement playerMovement;


    void Start()
    {
        flt_CurrentPlayerHealth = max_playerHealth;
      
        playerMovement = GetComponent<PlayerMovement>();
    }

    #region DamageHandling
    public void TakeDamageOfPlayer(float Damage)
    {
        if (!playerMovement.GetIsPlayerMove())
        {
            return;
        }
        
        flt_CurrentPlayerHealth -= Damage;
        if (flt_CurrentPlayerHealth<0)
        {
            PlayerManager.instance.SetPlayerStatus(false);
            RaceManger.instance.RemoveGameObjectInList(this.gameObject);
            Destroy(gameObject, 0.5f);
        }
    }
    #endregion

}
