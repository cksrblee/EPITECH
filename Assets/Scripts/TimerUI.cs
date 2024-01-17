using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    // hand = rotating obj
    public Transform hand;
    private float timerDuration = GameManager.timerDuration;
    private float timer; // Current Timer

    // For Sound Effect
    public AudioSource tickSource; // Reference to the AudioSource component
    private float tickInterval; // Time between ticks
    private float nextTickTime; // Time when the next tick should occur

    private float timeScale = 1f;

    void Start()
    {
        timer = timerDuration; // Set the timer time
        tickInterval = 1f; // Set the interval between ticks, adjust as needed
        nextTickTime = Time.time + tickInterval;
    }

    void Update()
    {
        //timeScale = GameManager.timerDuration;
        if (timer > 0)
        {
            timer -= Time.deltaTime; // timer goes down
            float angle = (360 / timerDuration) * Time.deltaTime; // Calculate rotating angle
            hand.Rotate(0, 0, -angle); // turn by second
        }

        // Increase ticking speed when there are 5 seconds or less remaining timer
        if (timer <= 5f && tickInterval != 0.5f) // Adjust this value to change the faster ticking rate
        {
            tickInterval = 0.5f; // Faster ticking interval
            nextTickTime = Time.time; // Reset next tick time for immediate effect
        }

        // Check if it's time to play the next tick
        if (Time.time >= nextTickTime)
        {
            tickSource.Play(); // Play the ticking sound
            nextTickTime += tickInterval; // Set the time for the next tick
        }
    }
}
