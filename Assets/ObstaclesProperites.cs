using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesProperites : MonoBehaviour
{
    [SerializeField] private float flt_MaxTimeToReduceSpeed;
    [SerializeField] private float flt_damage;
    [SerializeField] private float ReduceSpeed;

    public float GetMaxTimeToReduceSpeed()
    {
        return flt_MaxTimeToReduceSpeed;
    }
    public float GetDamage()
    {
        return flt_damage;
    }
    public float GetReduceSpeed()
    {
        return ReduceSpeed;
    }


}
