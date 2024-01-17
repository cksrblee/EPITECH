using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 0 ~ 13 -> 1st King
 14 -> Uwon
 27 -> 2nd King
 */

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
        int currentScenarioIndex = GameManager.scenarioIndex;

        float minProgress = 0.21f; // Initial progress when currentScenarioIndex is 1
        float maxProgress = 0.93f;  // Maximum progress
        float maxScenarios = 14.0f;  // Total number of scenarios

        // Calculate progress within the current king's range
        float progress;
        if (currentScenarioIndex < 14)
        {
            progress = (float)currentScenarioIndex / (maxScenarios - 1);
        }
        else if(currentScenarioIndex == 15)
        {
            progress = minProgress;
            fullProgressBar.fillAmount = minProgress;
        }
        else
        {
            // Reset the index for the second king's set of scenarios
            int indexForSecondKing = currentScenarioIndex - 14;
            progress = (float)indexForSecondKing / (maxScenarios - 1);
        }

        // Set target progress based on the calculated progress
        targetProgress = minProgress + progress * (maxProgress - minProgress);

        print("Progress : " + targetProgress);
        // Update the image based on progress level
        UpdateProgressImage(targetProgress);
    }

    // 0.93 is FULL / 0.21 is Empty / Midpoint is 0.57
    private void UpdateProgressImage(float progress)
    {
        int currentScenarioIndex = GameManager.scenarioIndex; // Get the current scenario index

        // Check the current king and set images accordingly
        if (currentScenarioIndex < 14)
        {
            // First king's image progression
            if (progress >= 0.75f)
                progressKingImage.sprite = KingImages[3];
            else if (progress >= 0.57f)
                progressKingImage.sprite = KingImages[2];
            else if (progress >= 0.39f)
                progressKingImage.sprite = KingImages[1];
            else
                progressKingImage.sprite = KingImages[0];
        }
        else
        {
            // Second king's image progression
            if (progress >= 0.75f)
                progressKingImage.sprite = KingImages[7];
            else if (progress >= 0.57f)
                progressKingImage.sprite = KingImages[6];
            else if (progress >= 0.39f)
                progressKingImage.sprite = KingImages[5];
            else
                progressKingImage.sprite = KingImages[4];
        }
    }

}