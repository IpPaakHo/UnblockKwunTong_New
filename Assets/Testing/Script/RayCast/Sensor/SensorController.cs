using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorController : MonoBehaviour
{
    FrontSensor leftSenosor;
    FrontSensor rightSenosor;
    public bool isFrontSensorHit;
    public bool needTurning;
    public bool isTrafficLight;
    void Start()
    {
        rightSenosor = transform.GetChild(0).GetComponent<FrontSensor>();
        leftSenosor = transform.GetChild(1).GetComponent<FrontSensor>();
        needTurning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(leftSenosor.isHit || rightSenosor.isHit)
        {
            isFrontSensorHit = true;
        }
        else
        {
            isFrontSensorHit = false;
        }

        if(leftSenosor.isFrontCarParking || rightSenosor.isFrontCarParking)
        {
            needTurning = true;
        }
        else
        {
            needTurning = false;
        }

        if(leftSenosor.isTrafficLight || rightSenosor.isTrafficLight)
        {
            isTrafficLight = true;
        }
        else
        {
            isTrafficLight = false;
        }
    }
}
