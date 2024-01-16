using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image fullProgressBar; // Assign in Inspector
    private GameManager gameManager;
    private float targetProgress; // Target progress value
    private float fillSpeed = 2.0f; // Speed of the fill animation

    void Start()
    {
        // Get the GameManager instance
        gameManager = FindObjectOfType<GameManager>();
        targetProgress = fullProgressBar.fillAmount;
    }

    void Update()
    {
        UpdateProgressBar();

        // Lerp the fillAmount
        if (fullProgressBar.fillAmount < targetProgress)
        {
            fullProgressBar.fillAmount = Mathf.Lerp(fullProgressBar.fillAmount, targetProgress, fillSpeed * Time.deltaTime);
        }
    }

    private void UpdateProgressBar()
    {
        int currentScenarioIndex = GameManager.scenarioIndex + 1;
        float minProgress = 0.21f; // Initial progress when currentScenarioIndex is 1
        float maxProgress = 0.92f;  // Maximum progress
        float maxScenarios = 28f;  // Total number of scenarios

        // Scale and shift the progress range
        float progress = ((float)currentScenarioIndex - 1) / (maxScenarios - 1);
        targetProgress = minProgress + progress * (maxProgress - minProgress);
    }
}

