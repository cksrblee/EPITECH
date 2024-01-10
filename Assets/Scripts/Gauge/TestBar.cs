using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestBar : MonoBehaviour
{
    public Image fillImage; // This is the filled image.
    public float percentage = 0;
    private float gauge = 1f; // Gauge is 0 <= x <= 1
    private float epsilon = 0.01f; // Epsilon value for stopping interpolation

    private void Update()
    {
        float targetGauge = percentage / 100f; // Convert the target percentage to a value between 0 and 1
        
        if (Mathf.Abs(targetGauge - gauge) < epsilon)
        {
            gauge = targetGauge; // If it's within epsilon, just set to the target value
        }
        else
        {
            gauge = Mathf.Lerp(gauge, targetGauge, Time.deltaTime * 3); // Otherwise, smoothly interpolate towards the target value
        }
        
        fillImage.fillAmount = gauge; // Update the fillAmount in the gauge
    }

    // Setting the gauge
    public void SetGauge(float value)
    {
        gauge = Mathf.Clamp01(value); // Set the value between 0 and 1
        fillImage.fillAmount = gauge; // Update the fillAbmout in the gauge
    }

    public float GetGauge(float value) { return  Mathf.Clamp01(value); }
}