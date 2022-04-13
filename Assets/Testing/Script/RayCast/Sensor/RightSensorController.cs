using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSensorController : MonoBehaviour
{
    public bool isActive;
    RightSensor rightFrontSensor;
    RightSensor rightMiddleSensor;
    RightSensor rightBackSensor;
    public bool isRightSensorHit;
    // Start is called before the first frame update
    void Start()
    {
        rightFrontSensor = transform.GetChild(2).GetComponent<RightSensor>();
        rightMiddleSensor = transform.GetChild(3).GetComponent<RightSensor>();
        rightBackSensor = transform.GetChild(4).GetComponent<RightSensor>();
        isRightSensorHit = false;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(rightFrontSensor.isHit || rightMiddleSensor.isHit || rightBackSensor.isHit)
        {
            isRightSensorHit = true;
        }
        else
        {
            isRightSensorHit = false;
        }
    }
}
