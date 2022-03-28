using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    public Vector3 rotatePoint;
    private float nextWaypointDistance;

    // Start is called before the first frame update
    void Start()
    {
        rotatePoint = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z +5 );
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(rotatePoint);
        transform.Translate(Vector3.forward * Time.deltaTime);
        nextWaypointDistance = Vector3.Distance(transform.position, rotatePoint);
        if (nextWaypointDistance < 2f)
        {
            Debug.Log("success!");
            
        }
        //transform.Rotate(0.0f, 10.0f, 0.0f, Space.World);
    }
}
