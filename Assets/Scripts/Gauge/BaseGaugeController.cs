using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseGaugeController : MonoBehaviour
{
    public Image fillImage; // This is the filled image.
    public float percentage = 0;
    private float gauge = 0.5f; // Gauge is 0 <= x <= 1
    private float epsilon = 0.01f; // Epsilon value for stopping interpolation
    bool isPointZero = false;
    protected virtual void AddPoints(float points) {
        SetGauge(percentage + points);
        
    }
    protected virtual void SubtractPoints(float points) {
        SetGauge(percentage - points);
        
    }

    protected bool CheckZeroPoint()
    {
        if (percentage <= 0)
        {
            percentage = 0.0f;
            return true;
        }
        if (percentage >= 99.9f)
        {
            percentage = 100.0f;
            return false;
        }
        else
        {
            return false;
        }
    }

    protected virtual void Update()
    {
        if (CheckZeroPoint())
        {
            //0 포인트일 경우
            // 포인트 더하는 함수 더하기
            GameManager.CallEndingScene();
        }

        else
        {
            // not implemented
        }


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

    public float GetGauge(float value) { return this.percentage; }
}
