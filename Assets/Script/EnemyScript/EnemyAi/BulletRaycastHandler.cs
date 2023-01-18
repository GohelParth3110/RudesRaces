using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRaycastHandler : MonoBehaviour
{
    [SerializeField] private bool isCenterRaycast;
    [SerializeField] private bool isleftRayCast;
    [SerializeField] private bool isRightRaycast;
    [SerializeField] private bool isCompetitorCenter;
    [SerializeField] private bool isCompetitorLeft;
    [SerializeField] private bool isCompetitorRight;
    [SerializeField] private int input;
    [SerializeField] private Transform[] all_Transform;           // 0-center ,1 - left ,2 - right,
    [SerializeField] private float flt_ObstaclesRange;
    [SerializeField] private LayerMask ObstackleLayer;
    [SerializeField] private LayerMask competitorLayer;
    [SerializeField] private float flt_CompetitorRange;


    private void FixedUpdate()
    {
        GetInput();
        CheckingAllRaycast();
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
        if (!isCenterRaycast && !isleftRayCast && !isRightRaycast && !isCompetitorCenter && !isCompetitorRight
                        && !isCompetitorLeft)
        {
            input = 0;
        }
        else
        {
            input = 1;
 
        }

    }


    private void CheckingAllRaycast()
    {
        for (int i = 0; i < all_Transform.Length; i++)
        {
           
            RaycastHit hit;
            
            if (Physics.Raycast(all_Transform[i].position,all_Transform[i].forward, out hit,flt_ObstaclesRange , ObstackleLayer))
            {
                SetStatusOfBool(i, true);
            }
            else
            {
                SetStatusOfBool(i, false);
                continue;
            }

        }
        for (int i = 0; i < all_Transform.Length; i++)
        {

            RaycastHit hit;

            if (Physics.Raycast(all_Transform[i].position, all_Transform[i].forward, out hit, 
                flt_CompetitorRange, competitorLayer))
            {
                SetStatusCompetitor(i, true);
            }
            else
            {
                SetStatusCompetitor(i, false);
               
            }
            
        }
    }
    private void SetStatusCompetitor(int i , bool value)
    {
        if (i == 0)
        {
            isCompetitorCenter = value;


        }
        else if (i == 1)
        {
            isCompetitorLeft = value;

        }
        else if (i == 2)
        {
            isCompetitorRight = value;
        }
    }
    private void SetStatusOfBool(int i, bool value)
    {
        if (i == 0)
        {
            isCenterRaycast = value;


        }
        else if (i == 1)
        {
            isleftRayCast = value;

        }
        else if (i == 2)
        {
            isRightRaycast = value;

        }
        
    }
    #endregion
}
