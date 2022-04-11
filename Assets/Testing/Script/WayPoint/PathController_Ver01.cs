using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController_Ver01 : MonoBehaviour
{
    Path pathManager;

    public GameObject[] currentPath = null;
    public int currentPathIndex = 0;
    public int nextPathIndex = 0;
    public int secondPathIndex = 0;
    public int mainPathIndex = 0;
    public int nextMainPathIndex = 0;
    public int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        pathManager = transform.parent.parent.gameObject.GetComponent<Path>();
    }

    private void Update()
    {
        
    }

    public int MainIndexController(int mainIndex)
    {
        if(mainIndex == 10)
        {
            int ran = Random.Range(0, 3);
            return ran + 1;
        }
        else if(mainIndex == 12)
        {
            int ran = Random.Range(0, 2);
            return ran + 1;
        }
        else if (mainIndex == 23)
        {
            int ran = Random.Range(2, 4);
            return ran;
        }
        else if(mainIndex == 34)
        {
            int ran = Random.Range(3, 5);
            return ran;
        }
        else
        {
            return mainIndex;
        }

        
    }
    public void GetPath(int mainPathIndex, int currentPathIndex, int secondPathIndex)
    {
        this.currentPath = pathManager.GetPath(mainPathIndex, currentPathIndex);
        this.nextPathIndex = pathManager.GetNextPathID(mainPathIndex, currentPathIndex, secondPathIndex);
        this.secondPathIndex = pathManager.GetSecondPathIndex(mainPathIndex, currentPathIndex);
    }

    public void SetNextPath()
    {
        waypointIndex = 0;
        mainPathIndex = pathManager.GetNextMainPathIndex(mainPathIndex, currentPathIndex);
        mainPathIndex = MainIndexController(mainPathIndex);
        currentPathIndex = nextPathIndex;

        currentPath = pathManager.GetPath(mainPathIndex, currentPathIndex);
        nextPathIndex = pathManager.GetNextPathID(mainPathIndex, currentPathIndex, secondPathIndex);
        secondPathIndex = pathManager.GetSecondPathIndex(mainPathIndex, currentPathIndex);
    }
}
