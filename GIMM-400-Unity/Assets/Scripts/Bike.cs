using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bike : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public Vector2 move;

    public void OnMove(InputValue inputValue)
    {
        move = inputValue.Get<Vector2>();
    }
    
    public void FixedUpdate()
    {
        var gamepad = Gamepad.current;
        float motor = maxMotorTorque * move.y;
        // float motor = maxMotorTorque * gamepad.leftStick.ReadValue().y;
        // float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        // float steering = gamepad.leftStick.ReadValue().x;
        float steering = maxSteeringAngle * move.x;
        
        print(move);
        
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
    }

    public void SlowDown(float amount)
    {
        // Slow down the car by amount
        SetBrakes(amount);
        // Wait for 1 second then set the brake torque to 0
        Invoke(nameof(ResetBrakes), 1f);
    }

    public void SetBrakes(float amount)
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            axleInfo.leftWheel.brakeTorque = amount;
            axleInfo.rightWheel.brakeTorque = amount;
        }
    }
    
    public void ResetBrakes()
    {
        SetBrakes(0);
    }
}

[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}
