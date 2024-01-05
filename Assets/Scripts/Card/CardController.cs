using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardController : MonoBehaviour
{

    public ApproveCard leftCard;
    public DisapproveCard rightCard;

    public Scenarios scenarios;

    public int curSceneIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Set Scenarios
        scenarios = GameObject.Find("GameManager").GetComponent<GameManager>().GetScenarios();

        //Set First Card 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeCards(Scenario scenario)
    {
        leftCard = new ApproveCard();
        rightCard = new DisapproveCard();

        leftCard.Build(scenario.answer, scenario.hint, scenario.reaction, scenario.effect);
        rightCard.Build(scenario.answer, scenario.hint, scenario.reaction, scenario.effect);
    }
    void OnMouseOverRightCard()
    {

    }

    void OnMouseOverLeftCard()
    {

    }

    //오른쪽 카드 선택
    void OnRightCardSelected()
    {

    }

    void OnLeftCardSelected()
    {

    }
}
