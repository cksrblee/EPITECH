using System.Collections;
using System.Collections.Generic;
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

    public Scenarios scenarios;

    public int curScenarioIndex = 1;


    // Start is called before the first frame update
    void Start()
    {
        //Set Scenarios
        scenarios = GameObject.Find("GameManager").GetComponent<GameManager>().GetScenarios();

        //Set First Card 
        curScenarioIndex = GameManager.scenarioIndex;

        MakeCards(scenarios.scenarios[curScenarioIndex]);

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
