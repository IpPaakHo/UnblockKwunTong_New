using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontSensor : MonoBehaviour
{
    [SerializeField] private float rayDistance;
    public bool isHit;

    public bool isFrontCarParking;
    public bool isTrafficLight;
    public Transform hitCar;

    CarMovementController_Ver02 moveController;

    void Start()
    {
        moveController = transform.parent.parent.gameObject.GetComponent<CarMovementController_Ver02>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitTarget;

        if (Physics.Raycast(ray, out hitTarget, rayDistance))
        {
            Debug.DrawLine(ray.origin, hitTarget.point, Color.red);
            //Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.red);
            isHit = true;
            hitCar = hitTarget.transform;
            isFrontCarParking = hitCar.GetComponent<CarMovementController_Ver02>().parkingMode;
            if(hitCar.tag == "TrafficLightBarrier")
            {
                isTrafficLight = true;
            }
            /*if (hitCar.GetComponent<CarMovementController_Ver02>().parkingMode == true)
            {
                isFrontCarParking = true;
            }
            if (hitCar.GetComponent<CarMovementController_Ver02>().normalMode == true)
            {
                timer = Time.time;
                if(Time.time >= timer)
                {
                    moveController.currentSpeed /= 2;
                    timer = Time.time + 1f;
                }
            }*/
        }
        else
        {
            isHit = false;
            isFrontCarParking = false;
            isTrafficLight = false;
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.green);
        }
    }
}
