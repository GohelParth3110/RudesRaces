using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRaycastHandler : MonoBehaviour
{
    
    [SerializeField] private int input;
     private AmmoSystem ammoSystem;
    [SerializeField] private Transform[] all_Transform;           // 0-center ,1 -45, 2 45, 3 -90, 4 -90
    [SerializeField] private bool[] all_Bool;
    [SerializeField] private float[] all_AngleRotate;
    [SerializeField] private Transform tank_Tower;
    [SerializeField] private Transform target;
    [SerializeField] private bool isGetPostion;
    [SerializeField] private float distance;
    [SerializeField] private float flt_ObstaclesRange;
    [SerializeField] private LayerMask ObstackleLayer;
    [SerializeField] private LayerMask competitorLayer;
    [SerializeField] private float flt_CompetitorRange;



    private void Start()
    {
        ammoSystem = GetComponent<AmmoSystem>();
    }

    private void FixedUpdate()
    {
        if (ammoSystem.GetAmmo()>0)
        {
            GetInput();
            CheckingAllRaycast();
        }
       
    }

  

    #region GetAndSetProperites

    public int GetInputOfEnemy()
    {
        return input;
    }
    #endregion

    #region RaycastCheckingMethod
    private void GetInput()
    {
        

    }


    private void CheckingAllRaycast()
    {
        if (isGetPostion)
        {
            return;
        }
        for (int i = 0; i < all_Transform.Length; i++)
        {
           
            //RaycastHit hit;
            
            //if (Physics.Raycast(all_Transform[i].position,all_Transform[i].forward, out hit,flt_ObstaclesRange , ObstackleLayer))
            //{
            //    SetStatusOfBool(i, true);
            //}
            //else
            //{
            //    SetStatusOfBool(i, false);
               
            //}

        }
        for (int i = 0; i < all_Transform.Length; i++)
        {

            RaycastHit hit;

            if (Physics.Raycast(all_Transform[i].position, all_Transform[i].forward, out hit, 
                flt_CompetitorRange, competitorLayer))
            {

                Transform currentTarget = hit.collider.transform;
                float currentDistance = Vector3.Distance(transform.position, currentTarget.position);
                if (target == null)
                {
                    target = hit.collider.transform;
                    distance = Vector3.Distance(transform.position, target.position);
                }
                else 
                {
                    if (currentDistance<distance)
                    {
                        target = currentTarget;
                        distance = currentDistance;
                    }
                }
            }
           
            
        }
        if (target != null)
        {
            isGetPostion = true;
        }
    }
  
   
    #endregion
}
