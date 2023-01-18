using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    [SerializeField] private int ammo;
   
    public int  GetAmmo()
    {
        return ammo;
    }
    public void Addammo(int ammoValue)
    {
        ammo += ammoValue;
    }
    public void SubtarctAmmoValue(int ammoValue)
    {
        ammo -= ammoValue;
    }
}
