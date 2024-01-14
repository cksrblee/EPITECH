using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    // hand = rotating obj
    public Transform hand;
    private float timerDuration = 15f; // Timer goes for 10 seconds
    private float timer; // Current Timer

    void Start()
    {
        timer = timerDuration; // Set the timer time
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; // timer goes down
            float angle = (360 / timerDuration) * Time.deltaTime; // Calculate rotating angle
            hand.Rotate(0, 0, -angle); // turn by second
        }
    }
}
