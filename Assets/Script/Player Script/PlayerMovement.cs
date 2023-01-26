using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Componenets")]
    private Rigidbody playerRb;

    [Header("Player Properites")]
    [SerializeField] private float flt_MovementSpeed;   // base movement speed
    [SerializeField] private float flt_CurrentMovementSpeed;  // current movement speed
    [SerializeField] private float flt_RotatingAngle;  // RotatingAngle
    [SerializeField] private float flt_RotatingSpeed; //  RotationSpeed
   [SerializeField] private bool isPlayerFinishedRace;
    [SerializeField] private float playerRbMode;
    private float boundry;

    [Header("Game Data")]
    [SerializeField] private float flt_RateOfReduceSpeed;
    [SerializeField] private float flt_TargetAngle;
    [SerializeField] private float flt_CurrentAngle;
   

    [Header("ReduceSpeed When Trigger Something")]
    [SerializeField] private float flt_MaxSlowTime;
    [SerializeField] private float flt_CurrentSlowTime;
    [SerializeField] private bool isHit;

    
   
    void Start()
    {
       
        playerRb = GetComponent<Rigidbody>();
        flt_CurrentMovementSpeed = flt_MovementSpeed;
        boundry = RaceManger.instance.GetCurrentLevel().GetBoundryPostion();
    }
  
    void Update()
    {
        if (!RaceManger.instance.GetRaceStatus())
        {
            return;
        }
        if (isPlayerFinishedRace)
        {
            ReduceSpeed();
           
        }
        else if (!isPlayerFinishedRace)
        {
            GetInput();
           
            HandlingTriggerObstacle();
        }
      
    }
    private void FixedUpdate()
    {
        if (!RaceManger.instance.GetRaceStatus())
        {
            return;
        }
        PlayerMotion(); //we also check playerRb Move Rotation Also
    }
    #region PlayerProperites


    public void SetPlayerRaceStatus(bool  isplayerfinishRace)
    {
        isPlayerFinishedRace = isplayerfinishRace;
    }

    public void ResetPlayerMoveMent()
    {
        isHit = false;
        flt_CurrentMovementSpeed = flt_MovementSpeed;
        isPlayerFinishedRace = false;
        flt_CurrentSlowTime = 0;
        flt_MaxSlowTime = 0;
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

        isHit = true;
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

        isHit = true;
    }

    private void HandlingTriggerObstacle()
    {
        if (!isHit)
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
        isHit = false;
    }
    #endregion

    #region PlayerMovement
    private void GetInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0)
        {
            flt_TargetAngle = flt_RotatingAngle;
            //playerRb.velocity = new Vector3(0, 0, 0);
        }
        else if (horizontal < 0)
        {
            flt_TargetAngle = -flt_RotatingAngle;
           // playerRb.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            flt_TargetAngle = 0;
        }
    }
   
    private void PlayerMotion()
    {
       
       
        flt_CurrentAngle = Mathf.Lerp(flt_CurrentAngle, flt_TargetAngle, flt_RotatingSpeed * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, flt_CurrentAngle, 0);
       
       playerRb.velocity = transform.forward * flt_CurrentMovementSpeed;
       
    }
    public void ReduceSpeed()
    {
       
        flt_CurrentMovementSpeed = Mathf.Lerp(flt_CurrentMovementSpeed, 0, flt_RateOfReduceSpeed * Time.deltaTime);
       
        playerRb.velocity = new Vector3(0 , 0, 0);
        playerRb.angularVelocity = new Vector3(0, 0, 0);
    }

   
    #endregion

}
