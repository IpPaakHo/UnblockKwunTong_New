using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyKeySwitchScene : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            Debug.Log("A key press");
            SceneManager.LoadScene("RealisticKT");
        }
    }
}
