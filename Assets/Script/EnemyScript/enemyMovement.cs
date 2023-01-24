using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [Header("Properties Of EnemyMotion")]
    [SerializeField] private float flt_RateOfReduceSpeed;    // when raceOver time
    [SerializeField] private float[] all_RotationAngle;          // angleRotateWith index whene Ai Logic Time
    [SerializeField] private float[] all_RotationalSpeed;        // rotationspeed WithIndeex When Ai LogicTime
    private float flt_RotationalSpeed;         // currentRotation Speed
    private float flt_RotationAngle;       // currentRotation Angle
    [SerializeField] private  float flt_MovementSpeed;          // ForwordMovementSpeed
    private float flt_CurrentMovementSpeed;  // CurrentMovementSpeed
    [SerializeField] private float offset;                      // this offset Angle is When TargetAngle And CurrnetAngle sametime
    private bool shouldTakeRaycastInput = true;             // this bool get Data Raycast input Get Or Not
    private bool hasReachedTargetRotation = false;        // geting Data Of Raycast Set Angle Posiotion or not 
    private bool isChangingDirection = false;                         // this bool Set TargetAngle
    private float flt_TargetAngle;                               
    private float flt_CurrentAngle;
    private  int currentInput;
    private  int currentIndex;
    private bool isFinishedRace;
   
    
    [Header("Components")]
    private Rigidbody enemyRb;
    private RayCastHandler rayCastHandler;

    [Header("Data Of Trigger of Something")]
    private bool isHit;
    private float flt_CurrentSlowTime;
    private float flt_MaxSlowTime; 


    
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
        if (isFinishedRace)
        {
            ReduceSpeed();
            EnemyMotion();
        }
        else
        {
            GetInput();
            GetPosition();
            EnemyMotion();
            HandlingReduceSpeedofObstacle();
        }

    }


    public void ResetEnemyMoveMent()
    {
        isHit = false;
        flt_CurrentMovementSpeed = flt_MovementSpeed;
        isFinishedRace = false;
        flt_CurrentSlowTime = 0;
        flt_MaxSlowTime = 0;
    }
    public void SetEnemyStatus(bool isEnemyFinishRace)
    {
        isFinishedRace = isEnemyFinishRace;
    }
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

        isHit = true;
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

        isHit = true;
    }

    private void HandlingReduceSpeedofObstacle()
    {
        if (!isHit)
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
        isHit = false;
    }

    #endregion


    #region AI Logic
    private void GetInput()
    {
        if (!shouldTakeRaycastInput)
        {
            return;
        }
        if (rayCastHandler.GetInputOfEnemy() == 0 && shouldTakeRaycastInput)
        {
            currentInput = 0;
        }
        else
        {
           
            shouldTakeRaycastInput = false;
            currentInput = rayCastHandler.GetInputOfEnemy();
            currentIndex = rayCastHandler.GetIndexOfRaycast();
            flt_RotationalSpeed =  all_RotationalSpeed[currentIndex];
            flt_RotationAngle = all_RotationAngle[currentIndex];
            isChangingDirection = true;
            SetMaxAngle();
        }
    }

    private void GetPosition()
    {
        if (shouldTakeRaycastInput)
        {
            return;
        }

        if (currentInput == 1 && flt_CurrentAngle > flt_RotationAngle - offset)
        {
            hasReachedTargetRotation = true;
            currentInput = 0;
            isChangingDirection = true;
            SetMaxAngle(); 
        }

        if (currentInput == -1 && flt_CurrentAngle < -flt_RotationAngle + offset)
        {
            hasReachedTargetRotation = true;
            currentInput = 0;
            isChangingDirection = true;
            SetMaxAngle();
        }

        if (hasReachedTargetRotation && flt_CurrentAngle > 0 && flt_CurrentAngle < 5)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            flt_CurrentAngle = 0;
            flt_TargetAngle = 0;
            shouldTakeRaycastInput = true;
            isChangingDirection = false;
            SetMaxAngle();
            hasReachedTargetRotation = false; 
        }
        if (hasReachedTargetRotation && flt_CurrentAngle < 0 && flt_CurrentAngle > -5)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            flt_CurrentAngle = 0;
            flt_TargetAngle = 0;
            shouldTakeRaycastInput = true;
            isChangingDirection = false;
            SetMaxAngle();
            hasReachedTargetRotation = false;
        }
    }
    
    private void SetMaxAngle()
    {
        if (isChangingDirection)
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
            isChangingDirection = false;
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

        Debug.Log("ReduceSpeed");
        flt_CurrentMovementSpeed = Mathf.Lerp(flt_CurrentMovementSpeed, 0, flt_RateOfReduceSpeed * Time.deltaTime);
        enemyRb.velocity = new Vector3(0, 0, 0);
        enemyRb.angularVelocity = new Vector3(0, 0, 0);
     

    }
    #endregion
}
