using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAmmos : MonoBehaviour
{
    [SerializeField] private int ammo;
    [SerializeField] private float flt_DelayResetPickup;
    [SerializeField] private GameObject body;
    [SerializeField] private Collider colliderAmmo;
    private bool  isBody = true;
    private float flt_CurrnetTimeToReSetSystem;

    private string tag_Player = "Player";
    private string tag_Enemy = "Enemy";

    private void Update()
    {
        HandlingReSetPickup();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag_Player))
        {
            other.gameObject.GetComponent<AmmoSystem>().Addammo(ammo);
          
            SetReSetSystem();
        }
        else if (other.gameObject.CompareTag(tag_Enemy))
        {
            other.gameObject.GetComponent<AmmoSystem>().Addammo(ammo);
           
            SetReSetSystem();
        }
    }

   
   
    private void SetReSetSystem()
    {
        isBody = false;
        body.SetActive(false);
        colliderAmmo.enabled = false;
        flt_CurrnetTimeToReSetSystem = 0;
    }
    private void HandlingReSetPickup()
    {
        if (isBody)
        {
            return;
        }
        flt_CurrnetTimeToReSetSystem += Time.deltaTime;
        if (flt_CurrnetTimeToReSetSystem>flt_DelayResetPickup)
        {
            body.SetActive(true);
            colliderAmmo.enabled = true;
        }
    }
}
