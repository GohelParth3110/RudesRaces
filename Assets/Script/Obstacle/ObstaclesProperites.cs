using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesProperites : MonoBehaviour
{
    [Header("Particle Effect")]
    [SerializeField] private GameObject particle_ObstackleVfx;
    [Header("Data")]
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

    public void playVfx()
    {
        Instantiate(particle_ObstackleVfx, transform.position, transform.rotation);
    }


}
