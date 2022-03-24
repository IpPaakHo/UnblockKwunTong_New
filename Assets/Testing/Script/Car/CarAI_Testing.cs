using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarAI_Testing : MonoBehaviour
{
    [SerializeField] private List<Vector3> path = null;
    [SerializeField] private float arriveDistance = 0.3f;
    [SerializeField] private float lastPointArriveDistance = 0.1f;
    [SerializeField] private float turningAngleOffset = 5f;
    [SerializeField] private Vector3 currentTargetPosition;

    private int index = 0;

    private bool stop;

    public bool Stop
    {
        get { return stop; }
        set { stop = value; }
    }
    [field: SerializeField]
    public UnityEvent<Vector2> OnDrive { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if(path == null || path.Count == 0)
        {
            Stop = true;
        } else
        {
            currentTargetPosition = path[index];
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfArrived();
    }

    public void SetPath(List<Vector3> path)
    {
        if(path.Count == 0)
        {
            Destroy(gameObject);
            return;
        }
        this.path = path;
        index = 0;
        currentTargetPosition = this.path[index];

        Vector3 relativePoint = transform.InverseTransformDirection(this.path[index++]);

        float angle = Mathf.Atan2(relativePoint.x, relativePoint.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, angle, 0);
        Stop = false;
    }

    private void CheckIfArrived()
    {
        if(stop == false)
        {
            var distanceToCheck = arriveDistance;
            if(index == path.Count - 1)
            {
                distanceToCheck = lastPointArriveDistance;
            }
            if(Vector3.Distance(currentTargetPosition, transform.position) < distanceToCheck)
            {
                SetNextTargetIndex();
            }
        }
    }

    private void Drive()
    {
        if (Stop)
        {
            OnDrive?.Invoke(Vector2.zero);
        }
        else
        {
            Vector3 relativePoint = transform.InverseTransformPoint(currentTargetPosition);
            float angle = Mathf.Atan2(relativePoint.x, relativePoint.z) * Mathf.Rad2Deg;
            var rotateCar = 0;
            if(angle > turningAngleOffset)
            {
                rotateCar = 1;
            }else if(angle < -turningAngleOffset)
            {
                rotateCar = -1;
            }
            OnDrive?.Invoke(new Vector2(rotateCar, 1));
        }
    }

    private void SetNextTargetIndex()
    {
        index++;
        if(index >= path.Count)
        {
            Stop = true;
            Destroy(gameObject);
        }
        else
        {
            currentTargetPosition = path[index];
        }
    }
}
