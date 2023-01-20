using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRaycastHandler : MonoBehaviour
{
    
    [Header("Scripts")]
     private AmmoSystem ammoSystem;
    private EnemyShooting enemyShooting;

    [Header("RayCast Data")]
    [SerializeField] private Transform[] all_RaycastPositions;           // 0-center ,1 -45, 2 45, 3 -90, 4 -90
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float flt_MaxTargetFindRange;

    [Header("Tank Tower Target Handling")]
    private Vector3 defaultPostion;
    private Transform target;
    private bool isTargetLocked;
    [SerializeField] private Transform tank_Tower;
    [SerializeField] private float flt_RotationSpeedTowardsTarget;
    private float shortestDistance;
    private bool shouldBeInDefaultState = false;


    private void Start()
    {
        ammoSystem = GetComponent<AmmoSystem>();
        enemyShooting = GetComponent<EnemyShooting>();
        defaultPostion = tank_Tower.forward;
    }

    private void Update()
    {
        if (!RaceManger.instance.GetRaceStatus())
        {
            return;
        }
       
        if (isTargetLocked)
        {
            HandleBulletShooting();
        }

       

        if (shouldBeInDefaultState)
        {
            tank_Tower.forward = Vector3.Lerp(tank_Tower.forward, defaultPostion, flt_RotationSpeedTowardsTarget * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        if (ammoSystem.GetAmmo() <= 0)
        {
            shouldBeInDefaultState = true;
            return;
        }

        CheckingAllRaycast();
    }

 
    private void CheckingAllRaycast()
    {
        if (isTargetLocked)
        {
            return;
        }

        shortestDistance = 0;

        // hit all raycasts to find target
        for (int i = 0; i < all_RaycastPositions.Length; i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(all_RaycastPositions[i].position, all_RaycastPositions[i].forward, out hit, 
                flt_MaxTargetFindRange, targetLayer))
            {

                Transform currentTarget = hit.collider.transform;
                float currentDistance = Vector3.Distance(transform.position, currentTarget.position);
              
                if (shortestDistance == 0)
                {
                    target = currentTarget;
                    shortestDistance = currentDistance;
                }
                else 
                {
                    if (currentDistance < shortestDistance)
                    {
                        target = currentTarget;
                        shortestDistance = currentDistance;
                    }
                }
            }
           
            
        }

       

        if (shortestDistance != 0) // if not 0, found a target
        {
            shouldBeInDefaultState = false;
            isTargetLocked = true;
            
        }
        else
        {

            shouldBeInDefaultState = true;
        }
      
    }

   
    
    private void HandleBulletShooting()
    {

        if (target== null)
        {
            return;
        }
        Vector3 tagetDirection = (target.position - transform.position).normalized;
        tank_Tower.forward = Vector3.Lerp(tank_Tower.forward, tagetDirection, flt_RotationSpeedTowardsTarget * Time.deltaTime);
      



        float difference = Vector3.Distance(tank_Tower.forward, tagetDirection);

        if(Mathf.Abs(difference) < 0.1f)
        {
            ShootBulletTowardsTarget();
        }

       
    }
   
    private void ShootBulletTowardsTarget()
    { 
        enemyShooting.Fire();
        isTargetLocked = false;
    }
   
}
