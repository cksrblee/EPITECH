using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour
{
    public Transform leftCardPlateTransform;
    public Transform rightCardPlateTransform;
    private ApproveCard leftCard;
    private DisapproveCard rightCard;

    public Scenarios scenarios;

    public int curScenarioIndex = 1;


    // Start is called before the first frame update
    void Start()
    {
        //Set Scenarios
        scenarios = GameObject.Find("GameManager").GetComponent<GameManager>().GetScenarios();

        //Set First Card 
        curScenarioIndex = GameObject.Find("GameManager").GetComponent<UIController>().GetScenarioIndex();

        MakeCards(scenarios.scenarios[curScenarioIndex]);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeCards(Scenario scenario)
    {
        leftCard = new ApproveCard();
        rightCard = new DisapproveCard();

        leftCard.Build(scenario.answer, scenario.hint, scenario.reaction, scenario.effect, leftCardPlateTransform);
        rightCard.Build(scenario.answer, scenario.hint, scenario.reaction, scenario.effect, rightCardPlateTransform);
    }

}
