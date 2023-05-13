using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    private float oldSeconds;
    private Text timerText;
    //public float countUp = 0.0f;
    private float timeSeconds = 0.0f;
    public float timeMinute = 0.0f;
    private float timeHour = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        oldSeconds = 0.0f;
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSeconds += Time.deltaTime;
        if(timeSeconds >= 60.0f)
        {
            timeMinute++;
            timeSeconds = timeSeconds % 60;
        }
        if(timeMinute >= 60.0f)
        {
            timeHour++;
            timeMinute = timeMinute % 60;
        }
        if(timeSeconds != oldSeconds)
        {
            timerText.text = (timeHour).ToString("0") +":" + (timeMinute).ToString("00") + ":" + (timeSeconds).ToString("00");
        }
        oldSeconds = timeSeconds;
    }
}
