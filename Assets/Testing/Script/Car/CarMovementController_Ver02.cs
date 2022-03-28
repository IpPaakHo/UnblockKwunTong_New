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
    public bool slowdownMode;
    public bool parkingMode;

    [SerializeField] private Vector3 turningPoint;
    private float distanceBetweenTwoPoint;

    [SerializeField] private float timer;

    FrontSensor frontSensor;
    PathController_Ver01 pathController;
    Path pathManager;

    private bool setTurningPoint = false;

    private void Start()
    {
        //currentSpeed = 0f;
        //accSpeed = 0.4f;
        InitializedSpeedController();
        timer = 0f;
        //frontSensor = GetComponentInChildren<Raycast_Ver2>();
        //frontSensor = GetComponentInChildren<FrontSensor>();
        frontSensor = transform.GetChild(0).GetChild(0).GetComponent<FrontSensor>();
        pathController = transform.GetComponent<PathController_Ver01>();
        pathManager = transform.parent.parent.gameObject.GetComponent<Path>();
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
    }

    public void normalModeController()
    {
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
        Debug.Log("Turning01"); 
        currentSpeed = 5;
        accSpeed = 0.2f;
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
        Debug.Log("Turning02");
        transform.LookAt(turningPoint);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        distanceBetweenTwoPoint = Vector3.Distance(transform.position, turningPoint);
        Debug.Log("Turning03");
        if (distanceBetweenTwoPoint < 2f)
        {
            Debug.Log("Turning04");
            turningMode = false;
            normalMode = true;
            setTurningPoint = false;
            Debug.Log("Turning05");
        }
    }

    public void ParkingMode()
    {
        currentSpeed = 0;
        accSpeed = 0;
    }

    public void SlowdownModeController()
    {
        if (Time.time >= timer && currentSpeed < MAXSPEED)
        {
            currentSpeed -= accSpeed;
            timer = Time.time + 0.1f;
            //Debug.Log("Timer updated");
        }
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
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

    public void HitByOther()
    {
        Debug.Log("Raycast Success!!");
        normalMode = false;
        stopMode = true;
    }

    
}
