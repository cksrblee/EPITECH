using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ApproveCard : BaseCard
{
    Transform canvas;
    Transform text1;
    TextMeshProUGUI text;
    RectTransform rectTransform;
    ApproveCard ac;
    Transform image;
    Image cardImage;
    Image cardBackgroundImage;

    Sprite cardSprite;
    Sprite characterImg;

    Sprite cardPanelFront;
    Sprite cardPanelBack;

    bool isFlipping = false;
    public override void Build(Answer answer, Hint hint, Reaction reaction, Effect effect, Transform plateTransform, string king_id, string event_id)
    {
        ac = gameObject.GetComponent<ApproveCard>();
        ac.answer = answer.agree;
        ac.hint = hint.agree;
        ac.reaction = reaction.agree;
        ac.effect = effect.agree; 
        ac.king_id = king_id;
        ac.event_id = event_id;

        gameObject.name = "ApproveCard";


        canvas = gameObject.transform.GetChild(0).transform.GetChild(0);

        cardImage = canvas.transform.GetChild(0).GetComponent<Image>();
        cardBackgroundImage = gameObject.transform.GetChild(0).GetComponent<Image>();

        text1 = canvas.transform.GetChild(1);
        text = text1.gameObject.GetComponent<TextMeshProUGUI>();
        rectTransform = text1.gameObject.GetComponent<RectTransform>();
        text.text = ac.answer;

        // Load and set the image
        string cardSpritePath = "Illustrate/David_1st/" + $"{king_id}-{event_id}(agree)"; // Make sure to use the correct naming scheme here
        cardSprite = Resources.Load<Sprite>(cardSpritePath);
        string characterSpritePath = "Illustrate/Characters/1stKnight";
        try
        {
            characterImg = Resources.Load<Sprite>(characterSpritePath);
        }
        catch
        {
            Debug.LogError("ERROR: CHARACTER IMG LOAD ERROR");
        }
        base.LoadCardPanelBackground(ref cardPanelFront, ref cardPanelBack);
    }

    //������ �� 
    public override void OnFlipped()
    {
        //rectTransform.Rotate(new Vector3(0, 180, 0));
        //rectTransform.gameObject.transform.Rotate(new Vector3(0, 180, 0));
        try
        {
            text.text = ac.hint;
            cardBackgroundImage.sprite = cardPanelBack;
            cardImage.sprite = characterImg;
        }
        catch {
            Debug.LogError("APPROVE: FLIPPING ERROR");
        }
    }

    public override void OnFlipBack()
    {
        //rectTransform.Rotate(new Vector3(0, -180, 0));
        try
        {

            text.text = ac.answer;
            //Debug.Log("DC:ANSWER::" + ac.answer);

            //set illustraion
            cardBackgroundImage.sprite = cardPanelFront;
            cardImage.sprite = cardSprite;
        }

        catch
        {
            Debug.LogError("APPROVE: FLIP BACK ERROR");  
        }
    }
    private IEnumerator WaitAndFlip()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.1f);

        OnFlipped();
        isFlipping = false;

        yield return new WaitForSeconds(2);
    }

    private IEnumerator WaitAndFlipBack()
    {
        //yield return new WaitForEndOfFrame();
        //yield return new WaitForSeconds(0.5f);

        OnFlipBack();
        isFlipping = false;

        yield return new WaitForSeconds(2);
    }
    public void FlipWrapper()
    {
        StartCoroutine(WaitAndFlip());
    }

    public void FlipBackWrapper()
    {
        StartCoroutine(WaitAndFlipBack());
    }
    protected override void Start()
    {
        base.Start();
    }


    protected override void Update()
    {
        base.Update();

        if (ac == null)
        {
            ac = gameObject.GetComponent<ApproveCard>();
        }

        if (text == null)
        {
            canvas = gameObject.transform.GetChild(0).GetChild(0);
            text = canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        if (cardImage == null)
        {
            cardImage = canvas.transform.GetChild(0).GetComponent<Image>();
        }


        if (rectTransform == null)
        {
            rectTransform = gameObject.transform.GetChild(0).GetComponent<RectTransform>();
        }

        if (isMouseOnCard)
        {
            if (isCursorOn)
            {
                if (!isFlipping)
                {
                    isFlipping = true;
                    FlipWrapper();
                }

            }

        }

        else
        {
            if (!isFlipping)
            {
                isFlipping = true;
                FlipBackWrapper();
            }
        }
    }

    public override void OnCardClicked()
    {
        GameObject final_card = GameObject.FindGameObjectWithTag("ResultCard");
        if (final_card != null)
        {
            Destroy(final_card); // Destroy the card
        }
        //Debug.Log("CARD CLICKED");
        // Load the popup prefab
        GameObject resultPrefab = Resources.Load<GameObject>("Prefabs/ResultCard");
        if (resultPrefab == null)
        {
            Debug.LogError("Popup prefab not found in Resources!");
            return;
        }

        // Instantiate the popup as a child of the InGameUI canvas
        Transform uiTransform = GameObject.Find("InGameUI").transform;
        GameObject popupInstance = Instantiate(resultPrefab, uiTransform);

        // Get the ResultCard component and call its Build method
        var resultCardComponent = popupInstance.AddComponent<ResultCard>();
        if (resultCardComponent != null)
        {
            resultCardComponent.Build(this.answer, this.reaction, this.effect);
            resultCardComponent.LoadImage(this.king_id, this.event_id, "agree");
        }
        else
        {
            Debug.LogError("ResultCard which is ApproveCard component not found on the popup prefab!");
        }
    }

    protected override void LoadCardPanelBackground(ref Sprite cardFront, ref Sprite cardBack)
    {
        base.LoadCardPanelBackground(ref cardFront, ref cardBack);
    }
}
