using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    // hand = rotating obj
    public Transform hand;
    private float timerDuration = GameManager.timerDuration;
    private float timer; // Current Timer
    private float startpoint = 7.0f; // Current Timer

    // For Sound Effect
    public AudioSource tickSource; // Reference to the AudioSource component
    private float tickInterval; // Time between ticks
    private float nextTickTime; // Time when the next tick should occur

    private float timeScale = 1f;

    private bool isPlaying = false;

    void Start()
    {
        timer = timerDuration; // Set the timer time
        tickInterval = 1f; // Set the interval between ticks, adjust as needed
        nextTickTime = Time.time + tickInterval;
        StartCoroutine(StartTimerDelay(startpoint));
    }

    IEnumerator StartTimerDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isPlaying = true;
    }

    void Update()
    {
        if(isPlaying) 
        {
            //timeScale = GameManager.timerDuration;
            if (timer > 0)
            {
                timer -= Time.deltaTime; // timer goes down
                float anglePerSecond = 360f / 13f; // 36 degrees per second
                float angle = anglePerSecond * Time.deltaTime; // Calculate rotating angle for the current frame
                hand.Rotate(0, 0, -angle); // Rotate the hand
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
}
