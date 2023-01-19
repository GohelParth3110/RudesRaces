using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadVerticalGenerate : MonoBehaviour
{
    [SerializeField] private GameObject sideRoad;
    [SerializeField] private float flt_VerticalOffset = 3.5f;
    [SerializeField] private float flt_HorizontalSpawnPostion;

    private void Start()
    {
        flt_HorizontalSpawnPostion = -(((RaceManger.instance.GetNoOfRacerPostionInGame() - 1)/2) * flt_VerticalOffset);
        SpawnHorizontalRoad();
    }

    private void SpawnHorizontalRoad()
    {
       

        for (int i = 0; i < RaceManger.instance.GetNoOfRacerPostionInGame(); i++)
        {
            Instantiate(sideRoad, new Vector3(flt_HorizontalSpawnPostion, transform.position.y, 
                transform.position.z), transform.rotation,transform);
            flt_HorizontalSpawnPostion += flt_VerticalOffset;
        }
    }
}
