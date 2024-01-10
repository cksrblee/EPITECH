using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseGaugeController : MonoBehaviour
{
    // For the Full Image;
    public Image royalFillImage;
    public Image popularityFillImage;
    public Image financeFillImage;

    // Percentage value
    public float royalPercentage = GameManager.Royal;
    public float popularityPercentage = GameManager.Popularity;
    public float financePercentage = GameManager.Finance;

    // Gauge is 0 <= x <= 1
    private float royalGauge = 0.5f;
    private float popularityGauge = 0.5f;
    private float financeGauge = 0.5f;

    // Epsilon value for stopping interpolation
    private float epsilon = 0.01f; 

    bool isPointZero = false;

    private void UpdateGauge(ref float gauge, float percentage, Image fillImage)
    {
        float targetGauge = percentage / 100f;
        if (Mathf.Abs(targetGauge - gauge) < epsilon)
        {
            gauge = targetGauge;
        }
        else
        {
            gauge = Mathf.Lerp(gauge, targetGauge, Time.deltaTime * 3);
        }
        fillImage.fillAmount = gauge;
    }

    private void Update()
    {
        if (CheckAllZeroPoint())
        {
            //0 ����Ʈ�� ���
            // ����Ʈ ���ϴ� �Լ� ���ϱ�
            ThisWorldEventController.OnGameOver.Invoke();
            //GameManager.CallEndingScene();
        }

        UpdateGauge(ref royalGauge, royalPercentage, royalFillImage);
        UpdateGauge(ref popularityGauge, popularityPercentage, popularityFillImage);
        UpdateGauge(ref financeGauge, financePercentage, financeFillImage);
    }

    public void SetGauge(string gaugeType, float value)
    {
        switch (gaugeType)
        {
            case "royal":
                royalGauge = Mathf.Clamp01(value);
                break;
            case "popularity":
                popularityGauge = Mathf.Clamp01(value);
                break;
            case "finance":
                financeGauge = Mathf.Clamp01(value);
                break;
        }
    }

    public float GetPercentage(string gaugeType)
    {
        switch (gaugeType)
        {
            case "royal":
                return royalPercentage;
            case "popularity":
                return popularityPercentage;
            case "finance":
                return financePercentage;
            default:
                return 0;
        }
    }

    public void ChangePercentages(float royal_val, float popularity_val, float finance_val)
    {
        royalPercentage = royal_val;
        popularityPercentage = popularity_val;
        financePercentage = finance_val; 
    }

    protected bool CheckZeroPoint(float name_percentage)
    {
        if (name_percentage <= 0.1)
        {
            name_percentage = 0.0f;
            return true;
        }
        if (name_percentage >= 99.9f)
        {
            name_percentage = 100.0f;
            return false;
        }
        else
        {
            return false;
        }
    }

    protected bool CheckAllZeroPoint()
    {
        if(CheckZeroPoint(royalPercentage) || 
           CheckZeroPoint(popularityPercentage) || 
           CheckZeroPoint(financePercentage))
        {
            return true;
        }
        else 
        { 
            return false; 
        }
    }
}