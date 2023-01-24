using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colider : MonoBehaviour
{
    private Rigidbody playerrb;
    private string tag_Boundry = "Boundry";
    private void Start()
    {
        playerrb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collder with boundry");
        if (collision.gameObject.CompareTag(tag_Boundry))
        {
            playerrb.velocity = new Vector3(0, 0, 0);
            Debug.Log("Collder with boundry");
        }
    }
   
}
