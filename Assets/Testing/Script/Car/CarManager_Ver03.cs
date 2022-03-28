using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager_Ver03 : MonoBehaviour
{
    private float nextWaypointDistance;
    private float currentWaypointDis;

    //FrontSensor frontSensor;
    Path pathManager;
    PathController_Ver01 pathController;
    CarMovementController_Ver02 moveController;
    SpawnCarController_Ver01 spawnController;

    void Start()
    {
        //Set layer mask to "Car"
        gameObject.layer = 7;
        gameObject.tag = "Car";
        //frontSensor = transform.GetChild(0).GetChild(0).GetComponent<FrontSensor>();
        pathManager = transform.parent.parent.gameObject.GetComponent<Path>();
        pathController = GetComponent<PathController_Ver01>();
        moveController = GetComponent<CarMovementController_Ver02>();
        spawnController = GetComponentInParent<SpawnCarController_Ver01>();

        //Set current path
        pathController.mainPathIndex = spawnController.mainPathIndex;
        pathController.currentPathIndex = spawnController.pathIndex;
        //pathController.mainPathIndex = 1;
        //pathController.currentPathIndex = 11;
        //transform.LookAt(currentPath[wayPointIndex].transform.position);

        SetPath();
        //pathController.GetPath();
    }

    // Update is called once per frame
    void Update()
    {
        //DestroyController();
        moveController.SpeedLimitedController();
        moveController.ModeController();
        nextWaypointDistance = Vector3.Distance(transform.position, pathController.currentPath[pathController.waypointIndex].transform.position);
        if (nextWaypointDistance < 2f)
        {
            IncreaseIndex();
        }
        
        //speedController.RandomStopEvent(pathController.mainPathIndex, pathController.currentPathIndex, pathController.waypointIndex);
    }

    void IncreaseIndex()
    {
        pathController.waypointIndex++;
        if (pathController.waypointIndex >= pathController.currentPath.Length)
        {
            pathController.SetNextPath();
        }

        transform.LookAt(pathController.currentPath[pathController.waypointIndex].transform.position);
    }

    private void DestroyController()
    {
        if (pathController.currentPathIndex == 9 || transform.position.y >= 1 || transform.position.y < 0)
        {
            pathController.nextPathIndex = 0;
            spawnController.count--;
            Destroy(gameObject);
        }


    }

    private void SetPath()
    {
        pathController.currentPath = pathManager.GetPath(pathController.mainPathIndex, pathController.currentPathIndex);
        pathController.nextPathIndex = pathManager.GetNextPathID(pathController.mainPathIndex, pathController.currentPathIndex, pathController.secondPathIndex);
        pathController.secondPathIndex = pathManager.GetSecondPathIndex(pathController.mainPathIndex, pathController.currentPathIndex);
    }

}
