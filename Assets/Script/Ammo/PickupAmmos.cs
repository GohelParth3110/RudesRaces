using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAmmos : MonoBehaviour
{
    // this scrpit procedure of Ammo PickupCollceted
    [SerializeField] private int ammo;                      // pickup how much ammo Get racer
    [SerializeField] private float flt_DelayResetPickup;    // howmuch time collceted PickupShow in game
    [SerializeField] private GameObject body;               // pickup Body 
    [SerializeField] private Collider colliderAmmo;         // pickip Collider
    [SerializeField] private GameObject particle_Destroy;   // pickup ParticleVfx When Pickup Collceted
    private bool  isBody = true;                            
    private float flt_CurrnetTimeToReSetSystem;
    // tag
    private string tag_Player = "Player";
    private string tag_Enemy = "Enemy";

    private void Update()
    {
        HandlingReSetPickup();
    }

    private void OnTriggerEnter(Collider other)
    {
        // when ammo trigger Racer  add Ammo in Racer
        // Particle Vfxplay
        // Some time Again Ammo enble
        if (other.gameObject.CompareTag(tag_Player))
        {
            other.gameObject.GetComponent<AmmoSystem>().Addammo(ammo);          
            Instantiate(particle_Destroy, transform.position, transform.rotation);
            SetReSetSystem();
        }
        else if (other.gameObject.CompareTag(tag_Enemy))
        {
            other.gameObject.GetComponent<AmmoSystem>().Addammo(ammo);
            Instantiate(particle_Destroy, transform.position, transform.rotation);
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
        // time Calculation For ammo Reset
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
