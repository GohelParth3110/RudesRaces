using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Properites")]
    [SerializeField] private float flt_RateOfReduceSpeed;
    [SerializeField] private float flt_MovementSpeed;   // forwordMoveMentSpeed
    [SerializeField] private float flt_CurrentMovementSpeed;  // currentForWordSpeed
    [SerializeField] private float flt_RotatingAngle;  // RotatingAngle
    [SerializeField] private float flt_RoatatingSpeed; //  RotationSpeed
    private Rigidbody playerRb;
    private float flt_TargetAngle;
    private float flt_CurrentAngle;
    [SerializeField]private bool isPlayerMove ;

    [Header("ReduceSpeed When Trigger Something")]
    [SerializeField] private float flt_MaxSlowTime;
    [SerializeField] private float flt_CurrentSlowTime;
    [SerializeField] private bool isSlowTime;

    
   
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        flt_CurrentMovementSpeed = flt_MovementSpeed;
       
    }
  
    void Update()
    {
        if (!RaceManger.instance.GetRaceStatus())
        {
            return;
        }
       
        GetInput();
        PlayerMotion(); //we also check playerRb Move Rotation Also
        HandlingTriggerObstacle();
    }
    #region PlayerProperites

    public bool GetIsPlayerMove()
    {
        return isPlayerMove;
    }
    public void SetIsPLayerMove(bool Value)
    {
        isPlayerMove = Value;
    }
   
    #endregion


    #region Player Touch in Obstackle
    public void SetPlayerMovementWhenPlayerTouchObstacle(float reduceSpeed, float maxTime)
    {
        float updatedMovementSpeed = flt_MovementSpeed - (0.01f * flt_MovementSpeed * reduceSpeed);

        if (updatedMovementSpeed < flt_CurrentMovementSpeed)
        {
            flt_CurrentMovementSpeed = updatedMovementSpeed;
        }

        float timeLeft = flt_MaxSlowTime - flt_CurrentSlowTime;
        if (timeLeft < maxTime)
        {
            flt_MaxSlowTime = maxTime;
            flt_CurrentSlowTime = 0;
        }

        isSlowTime = true;
    }
    public void SetplayerSpeedReduceBulletTouch(float reduceSpeed, float maxTime)
    {
        float updatedMovementSpeed = flt_MovementSpeed - (0.01f * flt_MovementSpeed * reduceSpeed);

        if (updatedMovementSpeed < flt_CurrentMovementSpeed)
        {
            flt_CurrentMovementSpeed = updatedMovementSpeed;
        }

        float timeLeft = flt_MaxSlowTime - flt_CurrentSlowTime;
        if (timeLeft < maxTime)
        {
            flt_MaxSlowTime = maxTime;
            flt_CurrentSlowTime = 0;
        }

        isSlowTime = true;
    }

    private void HandlingTriggerObstacle()
    {
        if (!isSlowTime)
        {
            return;
        }
        flt_CurrentSlowTime += Time.deltaTime;
        if (flt_CurrentSlowTime > flt_MaxSlowTime)
        {
            ResetReduceSpeed();
        }
    }
    private void ResetReduceSpeed()
    {
        flt_MaxSlowTime = 0;
        flt_CurrentMovementSpeed = flt_MovementSpeed;
        flt_CurrentSlowTime = 0;
        isSlowTime = false;
    }
    #endregion

    #region PlayerMovement
    private void GetInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0)
        {
            flt_TargetAngle = flt_RotatingAngle;
            playerRb.velocity = new Vector3(0, 0, 0);
        }
        else if (horizontal < 0)
        {
            flt_TargetAngle = -flt_RotatingAngle;
            playerRb.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            flt_TargetAngle = 0;
        }
    }
   
    private void PlayerMotion()
    {
       
       
        flt_CurrentAngle = Mathf.Lerp(flt_CurrentAngle, flt_TargetAngle, flt_RoatatingSpeed * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, flt_CurrentAngle, 0 );
        
        transform.Translate(transform.forward * flt_CurrentMovementSpeed * Time.deltaTime);

    }
    private void ReduceSpeed()
    {
        if (isPlayerMove)
        {
            return;
        }

        flt_CurrentMovementSpeed = Mathf.Lerp(flt_CurrentMovementSpeed, 0, flt_RateOfReduceSpeed * Time.deltaTime);

        playerRb.velocity = new Vector3(0 , 0, 0);
        playerRb.angularVelocity = new Vector3(0, 0, 0);
    }
    #endregion

}
