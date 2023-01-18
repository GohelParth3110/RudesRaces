using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using Assets.HeroEditor4D.Common.Scripts.Enums;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator_Character;
    [SerializeField] private Character4D Character;


    public void PlaySlashAnamiation()
    {
        animator_Character.SetBool("Slash2H", true);
    }
    public void PlayRunAnmation()
    {
        animator_Character.SetInteger("State", 3);
       
    }
    public void PlayDieAnmation()
    {
        animator_Character.SetInteger("State", 9);
    }
    public void PlayFireAmmoAnimation()
    {
        if (Character.AnimationManager.IsAction) return;

        animator_Character.SetInteger("WeaponType", 6);
        Character.AnimationManager.Fire();
    }
    public void PlayIdleAniation()
    {
        animator_Character.SetInteger("State", 0);
    }


}

