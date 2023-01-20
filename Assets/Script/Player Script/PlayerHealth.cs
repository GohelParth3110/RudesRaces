using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int flt_MaxPlayerHealth;
    [SerializeField] private float flt_CurrentPlayerHealth;


    void Start()
    {
        flt_CurrentPlayerHealth = flt_MaxPlayerHealth;  
    }

    #region DamageHandling
    public void TakeDamage(float Damage)
    {             
        flt_CurrentPlayerHealth -= Damage;
        if (flt_CurrentPlayerHealth<=0)
        {
            PlayerManager.instance.SetPlayerStatus(false);
            RaceManger.instance.RemoveGameObjectInList(this.gameObject);
            Destroy(gameObject, 0.5f);
        }
    }
    #endregion

}
