using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform[] all_TranformDotchRay;  // this array of Transform is Detect obstckle In Range
    [SerializeField] private Transform[] all_TransformPathFinder; // this array Of transfrm is pathfind;
    [SerializeField] private LayerMask interActiveLayer;    
    [SerializeField] private float flt_DotchRange;
    [SerializeField] private bool isSomthingBlocked;
    [SerializeField] private float flt_PathFinderRange;
     private int input;
     private int index;
     private bool[] all_Bool = new bool[3];

   // in this Ai Logic first Of All We Dotch All Tranform And Somthing is Detact So Pathfinder Raycast Touch And Get Best 
   // for for racing Avoid Obstackle
    


    private void FixedUpdate()
    {
        RayCastForDotch();  // detact obstckle in Range function
        PathFinder();     // best Path find Function
        //for (int i = 0; i < all_TransformPathFinder.Length; i++)
        //{
        //    Debug.DrawRay(all_TransformPathFinder[i].position, all_TransformPathFinder[i].forward * flt_PathFinderRange,
        //        Color.black, 0.1f);
        //}
    }
    private void RayCastForDotch()
    {
        // all detect ray cast produce and Check is somthing detact OR NOT
        for (int i = 0; i < all_TranformDotchRay.Length; i++)
        {
           // Debug.DrawRay(all_TranformDotchRay[i].position, Vector3.forward*flt_DotchRange,Color.red,0.1f);
            if (Physics.Raycast(all_TranformDotchRay[i].position,Vector3.forward,flt_DotchRange,interActiveLayer))
            {
                all_Bool[i] = true;
                Debug.Log("dotch");
            }
            else
            {
                all_Bool[i] = false;
                Debug.Log("nothing");
            }
           
        }

        if (all_Bool[0] || all_Bool[1] || all_Bool[2])
        {
            isSomthingBlocked = true;
        }
        else
        {
            isSomthingBlocked = false;
            input = 0;
        }
    }

    private void PathFinder()
    {
       // if dotchraycastfuction  detact somthing this function working
       // first find small path of left side if its blocked then check  small right Side Path,then both medium path and
       // max path 

        if (!isSomthingBlocked)
        {
            return;
        }
       
       

        if (!Physics.Raycast(all_TransformPathFinder[0].position,all_TransformPathFinder[0].
            forward,flt_PathFinderRange,interActiveLayer))
        {
            input = -1;
            index = 0;
            Debug.Log("Left - 1");
            
        }
        else if (!Physics.Raycast(all_TransformPathFinder[3].position, all_TransformPathFinder[3].
           forward, flt_PathFinderRange, interActiveLayer))
        {
            input = 1;
            index = 0;
            Debug.Log("Right - 1");

        }
        else if (!Physics.Raycast(all_TransformPathFinder[1].position, all_TransformPathFinder[1].
           forward, flt_PathFinderRange, interActiveLayer))
        {
            input = -1;
            index = 1;
            Debug.Log("Left - 2");
           
        }
        else if (!Physics.Raycast(all_TransformPathFinder[4].position, all_TransformPathFinder[4].
         forward, flt_PathFinderRange, interActiveLayer))
        {
            input = 1;
            index = 1;
            Debug.Log("Right - 2");

        }
        else if (!Physics.Raycast(all_TransformPathFinder[2].position, all_TransformPathFinder[2].
           forward, flt_PathFinderRange, interActiveLayer))
        {
            input = -1;
            index = 2;
            Debug.Log("Left - 3");
          
        }
        
      
        else if (!Physics.Raycast(all_TransformPathFinder[5].position, all_TransformPathFinder[5].
           forward, flt_PathFinderRange, interActiveLayer))
        {
            input = 1;
            index = 2;
            Debug.Log("Right - 3");
            
        }

    }

    // get index and input for Enemymovemnt  script
    public int GetIndexOfRaycast()
    {
        return index;
    }
    public int GetInputOfEnemy()
    {
        return input;
    }
}
