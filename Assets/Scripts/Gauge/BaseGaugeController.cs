using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseGaugeController : MonoBehaviour
{
    // For the Full Image
    public Image royalFillImage;
    public Image popularityFillImage;
    public Image financeFillImage;

    // For the yellow Image
    public Image royalYellowImage;
    public Image popularityYellowImage;
    public Image financeYellowImage;

    // For the red Image
    public Image royalRedImage;
    public Image popularityRedImage;
    public Image financeRedImage;

    public GameObject[] gauges;

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

    private void UpdateGauge(ref float gauge, float percentage,
                             Image normalImage, Image yellowImage, Image redImage)
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

        normalImage.fillAmount = gauge;
        yellowImage.fillAmount = gauge;
        redImage.fillAmount = gauge;

        // Control visibility of images based on thresholds
        normalImage.enabled = percentage > 30;
        yellowImage.enabled = percentage <= 30 && percentage > 10;
        redImage.enabled = percentage <= 10;
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

        royalPercentage = GameManager.Royal;
        popularityPercentage = GameManager.Popularity;
        financePercentage = GameManager.Finance;

        print(royalPercentage);
        print(popularityPercentage);
        print(financePercentage);

        UpdateGauge(ref royalGauge, royalPercentage, royalFillImage, royalYellowImage, royalRedImage);
        UpdateGauge(ref popularityGauge, popularityPercentage, popularityFillImage, popularityYellowImage, popularityRedImage);
        UpdateGauge(ref financeGauge, financePercentage, financeFillImage, financeYellowImage, financeRedImage);
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


    //Check whether any of the percentages is on zero
    public bool CheckAllZeroPoint()
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

    public void MakeAllImageInvisible()
    {
        try
        {
            foreach(var i in gauges)
            {
                i.SetActive(false);
            }

        }

        catch (Exception e)
        {
            print(e.ToString());
        }
}


    public void MakeAllImageVisible()
    {
        try
        {
            foreach (var i in gauges)
            {
                i.SetActive(true);
            }

            //이미지 fillamount 업데이트 하는 함수 설정해야할 듯

        }

        catch (Exception e)
        {
            print(e.ToString());
        }
    }
}
