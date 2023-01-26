using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastHandler : MonoBehaviour
{

    [SerializeField] private Transform[] all_Transform;           // 0-center ,1 - left ,2 - right,1- leftBoundry,2- rightBoundry
    private float flt_Range;
    [SerializeField] private float flt_StraightRange;
    [SerializeField] private float flt_AnglRange;
    [SerializeField] private float flt_Left_Boundry;
    [SerializeField] private float flt_Right_Boundry;
    [SerializeField] private LayerMask interRectiveLayerForTakeOverEnemy;
    [SerializeField] private float flt_RangeOfTakeOverEnemy;
    private GameObject hit_Gameobject;
    [SerializeField] private LayerMask interactiveLayers;
    [SerializeField]
    private float leftDistance;
    [SerializeField]
    private float rightDistance;
    [SerializeField]
    private bool isCenterRaycast;
    [SerializeField]
    private bool isleftRayCast;
    [SerializeField]
    private bool isRightRaycast;
    [SerializeField]
    private bool isLeftRaycastBoundry;
    [SerializeField]
    private bool isRightRayCastBoundry;
    [SerializeField]
    private int input;
    [SerializeField]
    private int index;
    [SerializeField] private bool[] all_Bool;
    
   




    private void FixedUpdate()
    {
        //if (!PlayerManager.instance.GetPlayerStatus())
        //{
        //    return;
        //}

        CheckingAllRaycast();
        GetInput();     
        CheckingTurnOver();
    }

    #region GetAndSetProperites
    public int GetIndexOfRaycast()
    {
        return index;
    }
    public int GetInputOfEnemy()
    {
        return input;
    }
    #endregion

    private void CheckingTurnOver()
    {
        if (!isCenterRaycast && !isRightRaycast && !isleftRayCast)
        {

            for (int i = 0; i < 3; i++)
            {
                RaycastHit hit;

                if (Physics.Raycast(all_Transform[i].position, all_Transform[i].forward, out hit, flt_RangeOfTakeOverEnemy,
                   interRectiveLayerForTakeOverEnemy))
                {

                    if (!isLeftRaycastBoundry && isRightRayCastBoundry)
                    {

                        input = -1;
                        index = 0;
                    }
                    else if (isLeftRaycastBoundry && !isRightRayCastBoundry)
                    {
                        input = 1;
                        index = 0;
                    }
                    else
                    {
                        input = -1;
                        index = 0;
                    }

                }
            }
        }
    }

    #region RaycastCheckingMethod
    private void GetInput()
    {
        if (!isCenterRaycast && !isleftRayCast && !isRightRaycast)
        {
            input = 0;
        }
        else
        {
            if (isCenterRaycast)
            {
                if (isleftRayCast && !isRightRayCastBoundry && !isRightRaycast)
                {
                    Debug.Log("isleftRayCast && !isRightRayCastBoundry && !isRightRaycast");
                    input = 1;
                    index = 1;
                }
                else if (isRightRaycast && !isLeftRaycastBoundry && !isleftRayCast)
                {
                    Debug.Log("isRightRaycast && !isLeftRaycastBoundry && !isleftRayCast");
                    input = -1;
                    index = 1;
                }

                else if (isRightRayCastBoundry && isRightRaycast && !isLeftRaycastBoundry)
                {
                    Debug.Log("isRightRayCastBoundry && isRightRaycast && !isLeftRaycastBoundry");
                    input = -1;
                    index = 1;
                }
                else if (isLeftRaycastBoundry && isleftRayCast && !isRightRayCastBoundry)
                {
                    Debug.Log("isLeftRaycastBoundry && isleftRayCast && !isRightRayCastBoundry");
                    input = 1;
                    index = 1;
                }
                else
                {
                    Debug.Log("Distanc Check");
                   
                    if (leftDistance < rightDistance)
                    {
                        input = 1;
                        index = 1;
                    }
                    else
                    {
                        input = -1;
                        index = 1;
                    }

                }



            }
            if (isRightRaycast && !isleftRayCast && !isCenterRaycast)
            {
                if (isRightRayCastBoundry && !isLeftRaycastBoundry && !isleftRayCast)
                {
                    Debug.Log("isRightRayCastBoundry && !isLeftRaycastBoundry && !isleftRayCast +++ isright");
                    input = -1;
                    index = 0;
                }
                else if (!isRightRayCastBoundry && !isLeftRaycastBoundry)
                {
                    Debug.Log("!isRightRayCastBoundry && !isLeftRaycastBoundry ++ isright");
                    input = -1;
                    index = 1;
                }
                else
                {
                        Debug.Log("Distancw Check + Rightcast");
                   
                        if (leftDistance > rightDistance)
                        {
                            input = -1;
                            index = 2;
                        }
                        else
                        {
                            input = 1;
                            index = 2;
                        }

                    

                }
            }
            if (isleftRayCast && !isRightRaycast && !isCenterRaycast)
            {
                if (isLeftRaycastBoundry && !isRightRayCastBoundry && !isRightRaycast)
                {
                    Debug.Log("isLeftRaycastBoundry && !isRightRayCastBoundry && !isRightRaycast + isleft");
                    index = 1;
                    input = 0;
                }
                else if (!isLeftRaycastBoundry && !isRightRayCastBoundry)
                {
                    Debug.Log("!isLeftRaycastBoundry && !isRightRayCastBoundry + isleft");
                    index = 1;
                    input = 1;
                }


                else
                {
                    Debug.Log("Distancw Check + Leftcast");
                    
                        if (leftDistance > rightDistance)
                        {
                            input = -1;
                            index = 2;
                        }
                        else
                        {
                            input = 1;
                            index = 2;
                        }
              
                }
            }
        }

    }


    private void CheckingAllRaycast()
    {
        for (int i = 0; i < all_Transform.Length; i++)
        {
            flt_Range = flt_StraightRange;

            if (i >2)
            {
                flt_Range = flt_AnglRange;
            }

            Debug.DrawRay(all_Transform[i].position, all_Transform[i].forward, Color.blue);
            RaycastHit hit;
            if (Physics.Raycast(all_Transform[i].position, all_Transform[i].forward, out hit, flt_Range, interactiveLayers))
            {
                SetStatusOfBool(i, true);
                hit_Gameobject = hit.collider.gameObject;
                if (i>2 && i%2!=0)
                {
                    if (leftDistance ==0)
                    {
                        leftDistance = Vector3.Distance(transform.position, hit.collider.transform.position);
                    }else
                    {
                        float dist = Vector3.Distance(transform.position, hit.collider.transform.position);
                        if (dist>leftDistance)
                        {
                            leftDistance = dist;
                        }
                    }
                   
                }
                else if (i > 2 && i % 2 == 0)
                {
                    if (rightDistance == 0)
                    {
                        rightDistance = Vector3.Distance(transform.position, hit.collider.transform.position);
                    }
                    else
                    {
                        float dist = Vector3.Distance(transform.position, hit.collider.transform.position);
                        if (dist > rightDistance)
                        {
                            rightDistance = dist;
                        }
                    }
                }
            }
            else
            {
                SetStatusOfBool(i, false);

            }





        }
    }
    private void SetStatusOfBool(int i, bool value)
    {
        all_Bool[i] = value;
        if (all_Bool[0])
        {
            
            isCenterRaycast = true;
        }
       else if (all_Bool[1] )
        {
           
            isleftRayCast = true;
        }
       else  if (all_Bool[2])
        {
           
            isRightRaycast = true;
        }
       else if (all_Bool[3]|| all_Bool[5]|| all_Bool[7] || all_Bool[9])
        {

            isLeftRaycastBoundry = true;
        }
       else if (all_Bool[4]||all_Bool[6]||all_Bool[8] || all_Bool[10])
        {
            isRightRayCastBoundry = true;
        }
        else
        {
            isRightRaycast = false;
            isCenterRaycast = false;
            isleftRayCast = false;
            isRightRayCastBoundry = false;
            isLeftRaycastBoundry = false;
        }
       
            

       
      
    }
    #endregion
}
