using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontSensor : MonoBehaviour
{
    [SerializeField] private float rayDistance;
    public bool isHit;
    public Transform hitCar;
    private float timer;

    CarMovementController_Ver02 moveController;

    void Start()
    {
        moveController = transform.parent.parent.gameObject.GetComponent<CarMovementController_Ver02>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        //Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.forward);
        RaycastHit hitTarget;

        if (Physics.Raycast(ray, out hitTarget, rayDistance))
        //if (Physics.Raycast(ray, rayDistance))
        {
            Debug.DrawLine(ray.origin, hitTarget.point, Color.red);
            //Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.red);
            isHit = true;
            hitCar = hitTarget.transform;
            if(hitCar.GetComponent<CarMovementController_Ver02>().parkingMode == true)
            {
                moveController.turningMode = true;
                moveController.normalMode = false;
                moveController.stopMode = false;
            }
            if (hitCar.GetComponent<CarMovementController_Ver02>().normalMode == true)
            {
                timer = Time.time;
                if(Time.time >= timer)
                {
                    moveController.currentSpeed /= 2;
                    timer = Time.time + 1f;
                }
                
            }

        }
        else
        {
            isHit = false;
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.green);
        }
    }
}
