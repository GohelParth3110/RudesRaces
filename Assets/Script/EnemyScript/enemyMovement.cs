using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [Header("Properties Of EnemyMotion")]
    [SerializeField] private float[] all_RotationAngle;
    [SerializeField] private float[] all_RotationalSpeed;
    [SerializeField] private float flt_RotationalSpeed;
    [SerializeField] private float flt_RotationAngle;
    [SerializeField] private  float flt_ForwordForce;
    [SerializeField] private float flt_CurrentForWordForce;
    [SerializeField] private float offset;
    [SerializeField] private bool isGetnput = true;
    [SerializeField] private bool isSetPosition = false;
    private bool isSetMaxAngle = false;
    public float flt_TargetAngle;
    public float flt_CurrentAngle;
    public int currentInput;
    public int currentIndex;
   [SerializeField] private bool isEnemyMove;
    private RayCastHandler rayCastHandler;
    private CharacterAnimation chracterAnimation;

    [Header("Data Of TriggerOfObstackle")]
    [SerializeField] private bool isTriggerObstacle;
    [SerializeField] private float flt_CurrentTimeTriggerObstacle;
    [SerializeField] private float flt_MaxTimeToTriggerObstacle;


    
    private void Start()
    {
      
        rayCastHandler = GetComponent<RayCastHandler>();
        flt_CurrentForWordForce = flt_ForwordForce;
      
    }
    private void Update()
    {
        if (!RaceManger.instance.GetRaceStatus())
        {
            return;
        }
        if (!isEnemyMove)
        {
            return;
        }
        GetInput();
        GetPosition();
        EnemyMotion();
        HandlingReduceSpeedofObstacle();
    }

    #region Properites
    public bool GetIsEnemyMove()
    {
        return isEnemyMove;
    }
    public void SetIsEnemyMove(bool value)
    {
        isEnemyMove = value;
    }
    public void SetIdleAnimation()
    {
        chracterAnimation.PlayIdleAniation();
    }
    #endregion

    #region Enemy Trigger Obstckle
    public void SetReduceSpeedWhenTriggerObstackle(float Speed ,float time)
    {
        flt_CurrentForWordForce = flt_ForwordForce - (0.01f * flt_ForwordForce * Speed);
        flt_CurrentTimeTriggerObstacle = 0;
        flt_MaxTimeToTriggerObstacle = time;
        isTriggerObstacle = true;
    }

    private void HandlingReduceSpeedofObstacle()
    {
        if (!isTriggerObstacle)
        {
            return;
        }
        flt_CurrentTimeTriggerObstacle += Time.deltaTime;
        if (flt_CurrentTimeTriggerObstacle > flt_MaxTimeToTriggerObstacle)
        {
            StopReduceSpeedWhenTriggerObstacle();
        }
    }

    private void StopReduceSpeedWhenTriggerObstacle()
    {
        flt_CurrentForWordForce = flt_ForwordForce;
        flt_CurrentTimeTriggerObstacle = 0;
        flt_MaxTimeToTriggerObstacle = 0;
        isTriggerObstacle = false;
    }

    #endregion


    #region AI Logic
    private void GetInput()
    {
        if (!isGetnput)
        {
            return;
        }
        if (rayCastHandler.GetInputOfEnemy() == 0 && isGetnput)
        {
            currentInput = 0;
        }
        else
        {
           
            isGetnput = false;
            currentInput = rayCastHandler.GetInputOfEnemy();
            currentIndex = rayCastHandler.GetIndexOfRaycast();
            flt_RotationalSpeed =  all_RotationalSpeed[currentIndex];
            flt_RotationAngle = all_RotationAngle[currentIndex];
            isSetMaxAngle = true;
            SetMaxAngle();
        }
    }

    private void GetPosition()
    {
        if (isGetnput)
        {
            return;
        }

        if (currentInput == 1 && flt_CurrentAngle > flt_RotationAngle - offset)
        {
            isSetPosition = true;
            currentInput = 0;
            isSetMaxAngle = true;
            SetMaxAngle(); 
        }

        if (currentInput == -1 && flt_CurrentAngle < -flt_RotationAngle + offset)
        {
            isSetPosition = true;
            currentInput = 0;
            isSetMaxAngle = true;
            SetMaxAngle();
        }

        if (isSetPosition && flt_CurrentAngle > 0 && flt_CurrentAngle < 5)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            flt_CurrentAngle = 0;
            flt_TargetAngle = 0;
            isGetnput = true;
            isSetMaxAngle = false;
            SetMaxAngle();
            isSetPosition = false; 
        }
        if (isSetPosition && flt_CurrentAngle < 0 && flt_CurrentAngle > -5)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            flt_CurrentAngle = 0;
            flt_TargetAngle = 0;
            isGetnput = true;
            isSetMaxAngle = false;
            SetMaxAngle();
            isSetPosition = false;
        }
    }
    
    private void SetMaxAngle()
    {
        if (isSetMaxAngle)
        {
            if (currentInput > 0)
            {
                flt_TargetAngle = flt_RotationAngle;
            }
            else if (currentInput < 0)
            {
                flt_TargetAngle = -flt_RotationAngle;
            }
            else
            {
                flt_TargetAngle = 0;
            }
            isSetMaxAngle = false;
        }
    }

    #endregion

    #region EnemyMotion
    private void EnemyMotion()
    {
        transform.Translate(transform.forward * flt_CurrentForWordForce * Time.deltaTime);

        flt_CurrentAngle = Mathf.Lerp(flt_CurrentAngle, flt_TargetAngle, flt_RotationalSpeed * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, flt_CurrentAngle, 0);
    }
    #endregion
}
