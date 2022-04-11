using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementController_Ver02 : MonoBehaviour
{
    public float currentSpeed;
    public float accSpeed;
    private const float MAXSPEED = 15f; 

    public bool stopMode;
    public bool normalMode;
    public bool turningMode;
    public bool parkingMode;

    [SerializeField] private Vector3 turningPoint;
    private float distanceBetweenTwoPoint;

    [SerializeField] private float timer;

    SensorController frontSensor;
    PathController_Ver01 pathController;
    Path pathManager;

    //public GameObject rightSensor;

    private bool setTurningPoint = false;

    private void Start()
    {
        //Debug.Log("Start Start");
        InitializedSpeedController();
        timer = 0f;
        //frontSensor = GetComponentInChildren<Raycast_Ver2>();
        //frontSensor = GetComponentInChildren<FrontSensor>();
        //frontSensor = transform.GetChild(0).GetChild(0).GetComponent<FrontSensor>();
        frontSensor = GetComponentInChildren<SensorController>();
        pathController = transform.GetComponent<PathController_Ver01>();
        pathManager = transform.parent.parent.gameObject.GetComponent<Path>();
        //Debug.Log("Start Finish");
    }

    private void Update()
    {
        //SpeedLimitedController();
        //ModeController();
    }

    public void InitializedSpeedController()
    {
        //normalMode = true;
        //stopMode = false;
        //turningMode = false;
    }

    public void ModeController()
    {
        if (normalMode)
        {
            normalModeController();
        }
        else if (stopMode)
        {
            StopModeController();
        }
        else if (turningMode)
        {
            TurningModeConreoller();
        }
        else if (parkingMode)
        {
            ParkingMode();
        }
        else
        {
            //normalModeController();
        }

        if (!frontSensor.isFrontSensorHit)
        {
            normalMode = true;
            stopMode = false;
            turningMode = false;
        }
        else if (frontSensor.isFrontSensorHit && !frontSensor.needTurning)
        {
            stopMode = true;
            normalMode = false;
            turningMode = false;
        }
        else if(frontSensor.isFrontSensorHit && frontSensor.needTurning)
        {
            stopMode = false;
            normalMode = false;
            turningMode = true;
        }
        else if(frontSensor.isFrontSensorHit && frontSensor.isTrafficLight)
        {
            stopMode = true;
            normalMode = false;
            turningMode = false;
        }
        
    }

    public void normalModeController()
    {
        accSpeed = 0.4f;
        if (Time.time >= timer && currentSpeed >= 0 && currentSpeed < MAXSPEED) {
            //accSpeed = 0.4f;
            currentSpeed += accSpeed;
            timer = Time.time + 0.1f;
        }
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    public void StopModeController()
    {
        currentSpeed = 0;
        accSpeed = 0;
    }

    public void TurningModeConreoller()
    {
        //Debug.Log("Turning01"); 
        //currentSpeed = 5;
        //accSpeed = 0.2f;
        if (setTurningPoint == false)
        {
            //turningPoint = new Vector3(transform.position.x + 10, 0, transform.position.z+2);
            pathController.mainPathIndex++;
            pathController.waypointIndex++;
            pathController.currentPath = pathManager.GetPath(pathController.mainPathIndex, pathController.currentPathIndex);
            turningPoint = new Vector3(
                (pathController.currentPath[pathController.waypointIndex].transform.position.x + pathController.currentPath[pathController.waypointIndex-1].transform.position.x) / 2,
                pathController.currentPath[pathController.waypointIndex].transform.position.y,
                (pathController.currentPath[pathController.waypointIndex].transform.position.z + pathController.currentPath[pathController.waypointIndex-1].transform.position.z) / 2 
            );
            setTurningPoint = true;
        }
        transform.LookAt(turningPoint);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        distanceBetweenTwoPoint = Vector3.Distance(transform.position, turningPoint);
        if (distanceBetweenTwoPoint < 3f)
        {
            turningMode = false;
            normalMode = true;
            setTurningPoint = false;
        }
    }

    public void ParkingMode()
    {
        currentSpeed = 0;
        accSpeed = 0;
    }

    public void SpeedLimitedController()
    {
        if(currentSpeed >= MAXSPEED)
        {
            currentSpeed = MAXSPEED;
        }
        if(currentSpeed < 0)
        {
            currentSpeed = 0;
        }
    }


    
}
