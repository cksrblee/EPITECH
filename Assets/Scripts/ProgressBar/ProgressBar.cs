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

    // FOR King's Images
    public Sprite[] KingImages;
    private Image progressKingImage;
    public Image KingImageOBJ;

    void Start()
    {
        // Get the GameManager instance
        gameManager = FindObjectOfType<GameManager>();
        targetProgress = fullProgressBar.fillAmount;
        progressKingImage = KingImageOBJ.GetComponent<Image>();
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

        // Update the image based on progress level
        UpdateProgressImage(targetProgress);
    }

    private void UpdateProgressImage(float currentProgress)
    {
        if (currentProgress >= 0.8f)
            progressKingImage.sprite = KingImages[3]; // 80%
        else if (currentProgress >= 0.6f)
            progressKingImage.sprite = KingImages[2]; // 60%
        else if (currentProgress >= 0.4f)
            progressKingImage.sprite = KingImages[1]; // 40%
        else if (currentProgress >= 0.2f)
            progressKingImage.sprite = KingImages[0]; // 20%
    }
}