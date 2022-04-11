using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    [SerializeField] private float rayDistance;
    public bool isHit;
    public Transform hitCar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HitBehavior()
    {
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.forward);
        RaycastHit hitTarget;

        if (Physics.Raycast(ray, out hitTarget, rayDistance))
        {
            Debug.DrawLine(ray.origin, hitTarget.point, Color.red);
            isHit = true;
            hitCar = hitTarget.transform;
            hitCar.GetComponent<CarMovementController_Ver02>().normalMode = false;
            hitCar.GetComponent<CarMovementController_Ver02>().stopMode = true;
        }
        else
        {
            isHit = false;
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.green);
        }
    }

}
