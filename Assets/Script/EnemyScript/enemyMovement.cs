using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [Header("Properties Of EnemyMotion")]
    [SerializeField] private float flt_RateOfReduceSpeed;
    [SerializeField] private float[] all_RotationAngle;          // angleRotateWith index whene Ai Logic Time
    [SerializeField] private float[] all_RotationalSpeed;        // rotationspeed WithIndeex When Ai LogicTime
    [SerializeField] private float flt_RotationalSpeed;         // currentRotation Speed
    [SerializeField] private float flt_RotationAngle;       // currentRotation Angle
    [SerializeField] private  float flt_MovementSpeed;          // ForwordMovementSpeed
    [SerializeField] private float flt_CurrentMovementSpeed;  // CurrentMovementSpeed
    [SerializeField] private float offset;                      // this offset Angle is When TargetAngle And CurrnetAngle sametime
    [SerializeField] private bool isGetnput = true;             // this bool get Data Raycast input Get Or Not
    [SerializeField] private bool isSetPosition = false;        // geting Data Of Raycast Set Angle Posiotion or not 
    private bool isSetMaxAngle = false;                         // this bool Set TargetAngle
    [SerializeField] private float flt_TargetAngle;                               
    [SerializeField] float flt_CurrentAngle;
    [SerializeField] int currentInput;
    [SerializeField] int currentIndex;
    [SerializeField] private bool isEnemyMove;
    private RayCastHandler rayCastHandler;
    private Rigidbody enemyRb;

    [Header("Data Of Trigger of Something")]
    [SerializeField] private bool isSlowTime;
    [SerializeField] private float flt_CurrentSlowTime;
    [SerializeField] private float flt_MaxSlowTime; 


    
    private void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        rayCastHandler = GetComponent<RayCastHandler>();
        flt_CurrentMovementSpeed = flt_MovementSpeed;
      
    }
    private void Update()
    {
        if (!RaceManger.instance.GetRaceStatus())
        {
            return;
        }
        ReduceSpeed();
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
   
    #endregion

    #region Enemy Trigger Obstckle
    public void SetReduceSpeedWhenTriggerObstackle(float Speed ,float time)
    {
       
        float updatedMovementSpeed = flt_MovementSpeed - (0.01f * flt_MovementSpeed * Speed);
        
      
        
        if(updatedMovementSpeed < flt_CurrentMovementSpeed)
        {
            flt_CurrentMovementSpeed = updatedMovementSpeed;
        }

        float timeLeft = flt_MaxSlowTime - flt_CurrentSlowTime;
        if (timeLeft < time)
        {
            flt_MaxSlowTime = time;
            flt_CurrentSlowTime = 0;
        }

        isSlowTime = true;
    }
    public void SetBulletTrigger(float reducedSpeedPercentage, float slowTime)
    {
        float updatedMovementSpeed = flt_MovementSpeed - (0.01f * flt_MovementSpeed * slowTime);

        if (updatedMovementSpeed < flt_CurrentMovementSpeed)
        {
            flt_CurrentMovementSpeed = updatedMovementSpeed;
        }

        float timeLeft = flt_MaxSlowTime - flt_CurrentSlowTime;
        if (timeLeft < slowTime)
        {
            flt_MaxSlowTime = slowTime;
            flt_CurrentSlowTime = 0;
        }

        isSlowTime = true;
    }

    private void HandlingReduceSpeedofObstacle()
    {
        if (!isSlowTime)
        {
            return;
        }
        flt_CurrentSlowTime += Time.deltaTime;
        if (flt_CurrentSlowTime > flt_MaxSlowTime)
        {
            StopReduceSpeedWhenTriggerObstacle();
        }
    }

    private void StopReduceSpeedWhenTriggerObstacle()
    {
        flt_CurrentMovementSpeed = flt_MovementSpeed;
        flt_CurrentSlowTime = 0;
        flt_MaxSlowTime = 0;
        isSlowTime = false;
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
            enemyRb.velocity = new Vector3(0, 0, 0);
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
        transform.Translate(transform.forward * flt_CurrentMovementSpeed * Time.deltaTime);

        flt_CurrentAngle = Mathf.Lerp(flt_CurrentAngle, flt_TargetAngle, flt_RotationalSpeed * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, flt_CurrentAngle, 0);
    }

    private void ReduceSpeed()
    {
        if (isEnemyMove)
        {
            return;
        }

        flt_CurrentMovementSpeed = Mathf.Lerp(flt_CurrentMovementSpeed, 0, flt_RateOfReduceSpeed * Time.deltaTime);
        enemyRb.velocity = new Vector3(0, 0, 0);
        enemyRb.angularVelocity = new Vector3(0, 0, 0);
     

    }
    #endregion
}
