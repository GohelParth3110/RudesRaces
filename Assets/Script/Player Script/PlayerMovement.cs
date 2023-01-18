using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Properites")]
    [SerializeField] private float flt_ForwordForce;
    [SerializeField] private float flt_CurrentForwordForce;
    [SerializeField] private float flt_RotatingAngle;
    [SerializeField] private float flt_RoatatingSpeed;
    private CharacterAnimation chracterAnimation;
    private float flt_TargetAngle;
    private float flt_CurrentAngle;
    [SerializeField]private bool isPlayerMove ;

    [Header("ReduceSpeed When Trigger Obstcle")]
    [SerializeField] private float flt_MaxTimeToReduceSpeed;
    [SerializeField] private float flt_CurrentTimeReduceSpeed;
    [SerializeField] private bool isPlayerTriggerWithObstacle;

    
   
    void Start()
    {
       
        flt_CurrentForwordForce = flt_ForwordForce;
       
    }
  
    void Update()
    {
        if (!RaceManger.instance.GetRaceStatus())
        {
            return;
        }
        if (!isPlayerMove)
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
    public void SetIdleAnimation()
    {
        chracterAnimation.PlayIdleAniation();
    }
    #endregion
    #region Player Touch in Obstackle
    public void SetPlayerMovementWhenPlayerTouchObstacle(float reduceSpeed, float maxTime)
    {
        flt_CurrentForwordForce = flt_ForwordForce - (0.01f * flt_ForwordForce * reduceSpeed);
        flt_CurrentTimeReduceSpeed = 0;
        flt_MaxTimeToReduceSpeed = maxTime;
        isPlayerTriggerWithObstacle = true;
    }

    private void HandlingTriggerObstacle()
    {
        if (!isPlayerTriggerWithObstacle)
        {
            return;
        }
        flt_CurrentTimeReduceSpeed += Time.deltaTime;
        if (flt_CurrentTimeReduceSpeed>flt_MaxTimeToReduceSpeed)
        {
            ResetReduceSpeed();
        }
    }
    private void ResetReduceSpeed()
    {
        flt_CurrentTimeReduceSpeed = flt_ForwordForce;
        flt_CurrentForwordForce = flt_ForwordForce;
        flt_CurrentTimeReduceSpeed = 0;
        isPlayerTriggerWithObstacle = false;
    }
    #endregion

    #region PlayerMovement
    private void GetInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0)
        {
            flt_TargetAngle = flt_RotatingAngle;
        }
        else if (horizontal < 0)
        {
            flt_TargetAngle = -flt_RotatingAngle;
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
        
        transform.Translate(transform.forward * flt_CurrentForwordForce*Time.deltaTime);

    }
    #endregion

}
