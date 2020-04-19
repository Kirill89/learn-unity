using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    const float DEGREES_PER_HOUR = 30f;
    const float DEGREES_PER_MINUTE = 6f;
    const float DEGREES_PER_SECOND = 6f;

    public bool continuous = false;
    public Transform hours;
    public Transform minutes;
    public Transform seconds;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (continuous)
        {
            TimeSpan time = DateTime.Now.TimeOfDay;
            hours.localRotation = Quaternion.Euler(0f, (float)time.TotalHours * DEGREES_PER_HOUR, 0f);
            minutes.localRotation = Quaternion.Euler(0f, (float)time.TotalMinutes * DEGREES_PER_MINUTE, 0f);
            seconds.localRotation = Quaternion.Euler(0f, (float)time.TotalSeconds * DEGREES_PER_SECOND, 0f);
        }
        else
        {
            DateTime time = DateTime.Now;
            hours.localRotation = Quaternion.Euler(0f, time.Hour * DEGREES_PER_HOUR, 0f);
            minutes.localRotation = Quaternion.Euler(0f, time.Minute * DEGREES_PER_MINUTE, 0f);
            seconds.localRotation = Quaternion.Euler(0f, time.Second * DEGREES_PER_SECOND, 0f);
        }
    }
}
