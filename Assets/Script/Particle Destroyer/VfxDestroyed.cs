using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxDestroyed : MonoBehaviour
{
    [SerializeField] private float flt_Delay;
   
    void Start()
    {
        StartCoroutine(delaytime());
    }
    IEnumerator delaytime()
    {
        yield return new WaitForSeconds(flt_Delay);
        Destroy(gameObject);
    }
   
}
