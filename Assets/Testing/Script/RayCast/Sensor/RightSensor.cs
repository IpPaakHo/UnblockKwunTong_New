using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSensor : MonoBehaviour
{
    public bool active;
    [SerializeField] private float rayDistance;
    public bool isHit;
    public Transform hitCar;
    CarMovementController_Ver02 moveController;

    // Start is called before the first frame update
    void Start()
    {
        moveController = transform.parent.parent.gameObject.GetComponent<CarMovementController_Ver02>();
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            Ray ray = new Ray(transform.position, transform.right);
            RaycastHit hitTarget;
            if (Physics.Raycast(ray, out hitTarget, rayDistance))
            {
                Debug.DrawLine(ray.origin, hitTarget.point, Color.red);
                //Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.red);
                isHit = true;
                hitCar = hitTarget.transform;
                if (hitCar.GetComponent<CarMovementController_Ver02>().normalMode == true)
                {
                    hitCar.GetComponent<CarMovementController_Ver02>().normalMode = false;
                    hitCar.GetComponent<CarMovementController_Ver02>().stopMode = true;
                }
            }
            else
            {
                isHit = false;
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.green);
            }
        }
        
    }
}
