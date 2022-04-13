using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    Text text;
    public float BlinkFadeInTime = 0.5f;
    public float BlinkStayTime = 0.8f;
    public float BlinkFadeOutTime = 0.7f;
    private float timeChecker = 0;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        color = text.color;
    }

    // Update is called once per frame
    void Update()
    {
        timeChecker += Time.deltaTime;
        if(timeChecker < BlinkFadeInTime)
        {
            text.color = new Color(color.r, color.g, color.b, timeChecker / BlinkFadeInTime);   //adding the alpha
        }
        else if(timeChecker < BlinkFadeInTime + BlinkStayTime)
        {
            text.color = new Color(color.r, color.g, color.b, 1);   //stay full alpha
        }
        else if(timeChecker < BlinkFadeInTime + BlinkStayTime + BlinkFadeOutTime)
        {
            text.color = new Color(color.r, color.g, color.b, 1- (timeChecker - (BlinkFadeInTime + BlinkStayTime))/BlinkFadeOutTime);  //reduse alpha
        }
        else
        {
            timeChecker = 0;
        }
    }
}
