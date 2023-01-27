using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject particle_ShieldVFX;
    [SerializeField] private GameObject particle_DieVfx;
    [SerializeField] private GameObject body;
    [SerializeField] private int flt_MaxPlayerHealth;
    [SerializeField] private float flt_CurrentPlayerHealth;
    private TriggerInteractionPlayer triggerInteractionPlayer;
    private Collider thisCollider;

    void Start()
    {
        thisCollider = GetComponent<Collider>();
        flt_CurrentPlayerHealth = flt_MaxPlayerHealth;
        triggerInteractionPlayer = GetComponent<TriggerInteractionPlayer>();
    }


  
   
    public void TakeDamage(float Damage)
    {             
        flt_CurrentPlayerHealth -= Damage;
        if (flt_CurrentPlayerHealth<=0)
        {
            PlayerManager.instance.SetPlayerStatus(false);
            thisCollider.enabled = false;
           
            triggerInteractionPlayer.GetPlayerMovement().enabled = false;
            triggerInteractionPlayer.GetPlayerShooting().enabled = false;
            StartCoroutine(ResetPlayer());
        }
    }

    IEnumerator ResetPlayer()
    {
        yield return new WaitForSeconds(1);

        flt_CurrentPlayerHealth = flt_MaxPlayerHealth;
        body.SetActive(true);
        transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        particle_ShieldVFX.SetActive(true);
        StartCoroutine(ClosetShieldVfx());
        triggerInteractionPlayer.GetPlayerMovement().enabled = true;
        triggerInteractionPlayer.GetPlayerShooting().enabled = true;
        triggerInteractionPlayer.GetPlayerMovement().ResetPlayerMoveMent();
        thisCollider.enabled = true;
      
    }
    IEnumerator ClosetShieldVfx()
    {
        yield return new WaitForSeconds(1);
        particle_ShieldVFX.SetActive(false);
    }
    public Collider GetCollider()
    {
        return thisCollider;
    }

   

}
