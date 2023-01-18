using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastHandler : MonoBehaviour
{
    [SerializeField] private  bool isCenterRaycast;
    [SerializeField] private bool isleftRayCast;
    [SerializeField] private bool isRightRaycast;
    [SerializeField] private bool isLeftRaycastBoundry;
    [SerializeField] private bool isRightRayCastBoundry;
    [SerializeField] private int input;
    [SerializeField] private int index;
    [SerializeField] private  Transform[] all_Transform;           // 0-center ,1 - left ,2 - right,1- leftBoundry,2- rightBoundry
    [SerializeField] private float flt_Range;
    [SerializeField] private float flt_StraightRange;
    [SerializeField] private float flt_AnglRange;
  
    [SerializeField] private float  flt_Left_Boundry;
    [SerializeField] private float flt_Right_Boundry;
    [SerializeField] private GameObject hit_Gameobject;
    [SerializeField] private LayerMask interactiveLayers;
    [SerializeField] private float leftDistance;
    [SerializeField] private float rightDistance;
    [SerializeField] private LayerMask interRectiveLayerForTakeOverEnemy;
    [SerializeField] private float flt_RangeOfTakeOverEnemy;
  


    private void FixedUpdate()
    {
        //if (!PlayerManager.instance.GetPlayerStatus())
        //{
        //    return;
        //}
        GetInput();
        CheckingAllRaycast();
        CheckingTurnOver();
    }

    #region GetAndSetProperites
    public int GetIndexOfRaycast()
    {
        return index;
    }
    public  int GetInputOfEnemy()
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

                if (Physics.Raycast(all_Transform[i].position,all_Transform[i].forward,out hit,flt_RangeOfTakeOverEnemy,
                   interRectiveLayerForTakeOverEnemy))
                {
                   
                    if (!isLeftRaycastBoundry && isRightRayCastBoundry)
                    {
                        Debug.Log(transform.name + "SidecutEnemy");
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
                    input = 1;
                    index = 1;
                }
                else if (isRightRaycast && !isLeftRaycastBoundry && !isleftRayCast)
                {
                    input = -1;
                    index = 1;
                }

                else if (isRightRayCastBoundry && isRightRaycast && !isLeftRaycastBoundry)
                {
                    Debug.Log("Center and Right Boundry Somthig left boundry space");
                    input = -1;
                    index = 1;
                }
                else if (isLeftRaycastBoundry && isleftRayCast && !isRightRayCastBoundry)
                {
                    Debug.Log("Center and Left Boundry Somthig Right Boundry Space");
                    input = 1;
                    index = 1;
                }
                else
                {
                    if (hit_Gameobject != null)
                    {
                        if (hit_Gameobject.CompareTag("Obstacles"))
                        {
                            Debug.Log("Distance Chcek");
                            leftDistance = Mathf.Abs
                            (hit_Gameobject.transform.position.x - flt_Left_Boundry);
                            rightDistance = Mathf.Abs
                                (hit_Gameobject.transform.position.x - flt_Right_Boundry);
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
            if (isRightRaycast && !isleftRayCast && !isCenterRaycast)
            {
                if (isRightRayCastBoundry  && !isLeftRaycastBoundry&& !isleftRayCast)
                {
                    input = -1;
                    index = 0;
                }
                else if (!isRightRayCastBoundry && !isLeftRaycastBoundry)
                {
                    input = -1;
                    index = 1;
                }
                else
                {
                    if (hit_Gameobject != null)
                    {
                        float leftDistnce = Mathf.Abs
                       (hit_Gameobject.transform.position.x - flt_Left_Boundry);
                        float rightDistnce = Mathf.Abs
                             (hit_Gameobject.transform.position.x - flt_Right_Boundry);
                        if (leftDistnce > rightDistnce)
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
            if (isleftRayCast && !isRightRaycast && !isCenterRaycast)
            {
                if (isLeftRaycastBoundry &&  !isRightRayCastBoundry && !isRightRaycast)
                {
                    Debug.Log("LeftcAst And Boundry Somthing Rightbiundry false");
                    index = 1;
                    input = 0;
                }
                else if (!isLeftRaycastBoundry && !isRightRayCastBoundry)
                {
                    index = 1;
                    input = 1;
                }
              

                else
                {
                    if (hit_Gameobject != null)
                    {
                        Debug.Log("LefXCastBoiundry SOMTHING dISTNACScHECK");
                        float leftDistnce = Mathf.Abs
                        (hit_Gameobject.transform.position.x - flt_Left_Boundry);
                        float rightDistnce = Mathf.Abs
                             (hit_Gameobject.transform.position.x - flt_Right_Boundry);
                        if (leftDistnce > rightDistnce)
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
        
    }

   
    private void CheckingAllRaycast()
    {
        for (int i = 0; i < all_Transform.Length; i++)
        {
            flt_Range = flt_StraightRange;

            if (i>3)
            {
                flt_Range = flt_AnglRange;
            }
            Debug.DrawRay(all_Transform[i].position, all_Transform[i].forward, Color.blue);
            RaycastHit hit;
            if (Physics.Raycast(all_Transform[i].position, all_Transform[i].forward,out hit, flt_Range, interactiveLayers))
            {
                SetStatusOfBool(i, true);
                hit_Gameobject = hit.collider.gameObject;
            }
            else
            {
                SetStatusOfBool(i, false);
             
            }

           
           


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
        else if (i == 3)
        {
            isLeftRaycastBoundry = value;

        }
        else if (i == 4)
        {
            isRightRayCastBoundry = value;

        }
    }
    #endregion
}
