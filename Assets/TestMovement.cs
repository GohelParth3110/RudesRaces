using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    float flt_CurrentAngle = 0f;
    float flt_TargetAngle = 0f;
    [SerializeField] private float flt_RotatingAngle;  // RotatingAngle
    [SerializeField] private float flt_RotatingSpeed;  // RotatingAngle
    [SerializeField] private float flt_CurrentMovementSpeed;  // RotatingAngle

    // Update is called once per frame
    void Update()
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

        //flt_CurrentAngle = Mathf.Lerp(flt_CurrentAngle, flt_TargetAngle, flt_RotatingSpeed * Time.deltaTime);
        //transform.localEulerAngles = new Vector3(0, flt_CurrentAngle, 0);


        transform.Translate(transform.forward * flt_CurrentMovementSpeed * Time.deltaTime, Space.Self);

    }
}
