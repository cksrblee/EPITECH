using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public Transform leftCardPlateTransform;
    public Transform rightCardPlateTransform;
    private ApproveCard leftCard;
    private DisapproveCard rightCard;
    private ResultCard resultCard;
    
    // Variables for Pop Up Quiz
    public GameObject quizPanel;
    public Button toggleButton;
    public TextMeshProUGUI QuestionText;

    // For Quiz Animation
    //public Animator quizPanelAnimator; // Reference to the Animator component
    //private bool isPanelOpen = false;


    public Scenarios scenarios;

    public int curScenarioIndex = 1;


    // Start is called before the first frame update
    void Start()
    {
        //Set Scenarios
        scenarios = GameObject.Find("GameManager").GetComponent<GameManager>().GetScenarios();

        //Set First Card 
        curScenarioIndex = GameManager.scenarioIndex;

        // Start with the quiz panel active for 3 seconds
        //StartCoroutine(ActivatePanelWithAnimation());
        quizPanel.SetActive(true);
        StartCoroutine(DeactivatePanelAfterTime(3.0f));

        // Add a listener to the button to call TogglePanel method when clicked
        toggleButton.onClick.AddListener(TogglePanel);

        MakeCards(scenarios.scenarios[curScenarioIndex]);

    }

    //IEnumerator ActivatePanelWithAnimation()
    //{
    //    quizPanel.SetActive(true);
    //    quizPanelAnimator.SetTrigger("Open"); // Trigger the animation to open the panel
    //    yield return new WaitForSeconds(3.0f);
    //    quizPanelAnimator.SetTrigger("Close"); // You'll need to create this trigger and animation clip
    //    yield return new WaitForSeconds(0.5f); // Adjust this time to the length of your close animation
    //    quizPanel.SetActive(false);
    //}

    //void TogglePanel()
    //{
    //    isPanelOpen = !isPanelOpen;
    //    if (isPanelOpen)
    //    {
    //        quizPanel.SetActive(true);
    //        quizPanelAnimator.SetTrigger("Open");
    //    }
    //    else
    //    {
    //        quizPanelAnimator.SetTrigger("Close");
    //        // You might want to deactivate the panel after the animation finishes
    //        StartCoroutine(DeactivateAfterAnimation());
    //    }
    //}

    //IEnumerator DeactivateAfterAnimation()
    //{
    //    // Wait for the animation to finish before deactivating the panel
    //    yield return new WaitForSeconds(0.5f); // Adjust this time to the length of your close animation
    //    quizPanel.SetActive(false);
    //}

    IEnumerator DeactivatePanelAfterTime(float time)
    {
        // Wait for the specified time
        QuestionText.text = scenarios.scenarios[curScenarioIndex].advise;
        yield return new WaitForSeconds(time);

        // Deactivate the panel
        quizPanel.SetActive(false);
    }

    void TogglePanel()
    {
        // Toggle the active state of the quiz panel
        quizPanel.SetActive(!quizPanel.activeSelf);
    }

    void MakeCards(Scenario scenario)
    {
        leftCard = new ApproveCard();
        rightCard = new DisapproveCard();

        leftCard.Build(scenario.answer, scenario.hint, scenario.reaction, scenario.effect, leftCardPlateTransform, scenario.king_id, scenario.event_id);
        rightCard.Build(scenario.answer, scenario.hint, scenario.reaction, scenario.effect, rightCardPlateTransform, scenario.king_id, scenario.event_id);

        // Dynamically construct the image file name
        string leftCardimage = $"{scenario.king_id}-{scenario.event_id}(agree)";
        string rightCardimage = $"{scenario.king_id}-{scenario.event_id}(disagree)";

        // Load the image
        LoadImage(leftCardimage, rightCardimage);
    }

    void LoadImage(string leftCardName, string rightCardName)
    {
        // Correct the path and load Sprites, not Images
        Sprite leftSprite = Resources.Load<Sprite>("Illustrate/David_1st/" + leftCardName);
        Sprite rightSprite = Resources.Load<Sprite>("Illustrate/David_1st/" + rightCardName);

        if (leftSprite != null && rightSprite != null)
        {
            var leftCard = GameObject.Find("ApproveCard").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
            var rightCard = GameObject.Find("DisapproveCard").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);

            // LeftCard and rightCard have Image components to assign the sprites to
            leftCard.GetComponent<Image>().sprite = leftSprite;
            rightCard.GetComponent<Image>().sprite = rightSprite;
        }
        else
        {
            Debug.LogError("Sprite not found: " + leftCardName + " or " + rightCardName);
        }
    }

}
