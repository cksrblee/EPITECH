using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class DisapproveCard : BaseCard
{
    Transform canvas;
    Transform text1;
    TextMeshProUGUI text;
    RectTransform rectTransform;
    DisapproveCard dc;
    Image cardImage;
    Image cardBackgroundImage;
    Sprite cardSprite;
    Sprite characterImg;
    bool isFlipping = false;

    Sprite cardPanelFront;
    Sprite cardPanelBack;

    string cardSpritePath;
    string characterSpritePath;
    public override void Build(Answer answer, Hint hint, Reaction reaction, Effect effect, Transform plateTransform, string king_id, string event_id)
    {
        dc = gameObject.GetComponent<DisapproveCard>();
        dc.answer = answer.disagree;
        dc.hint = hint.disagree;
        dc.reaction = reaction.disagree;
        dc.effect = effect.disagree;
        dc.king_id = king_id;
        dc.event_id = event_id;

        gameObject.name = "DisapproveCard";

        canvas = gameObject.transform.GetChild(0).transform.GetChild(0);

        cardImage = canvas.transform.GetChild(0).GetComponent<Image>();
        cardBackgroundImage = gameObject.transform.GetChild(0).GetComponent<Image>();
        text1 = canvas.transform.GetChild(1);
        text = text1.gameObject.GetComponent<TextMeshProUGUI>();
        rectTransform = text1.gameObject.GetComponent<RectTransform>();
        text.text = dc.answer;

        // Load and set the image
        cardSpritePath = "Illustrate/David_1st/" + $"{king_id}-{event_id}(disagree)"; // Make sure to use the correct naming scheme here
        cardSprite = Resources.Load<Sprite>(cardSpritePath);

        characterSpritePath = "Illustrate/Characters/2";
        characterImg = Resources.Load<Sprite>(characterSpritePath);
        if (cardSprite != null)
        {
            cardImage.sprite = cardSprite;
        }
        else
        {
            Debug.LogError("Sprite not found at path: " + cardSpritePath);
        }

        if (characterImg == null)
        {
            Debug.LogError("ERROR: LOAD CHARACTER IMG");
        }
        base.LoadCardPanelBackground(ref cardPanelFront, ref cardPanelBack);
    }

    protected override void Start()
    {
        base.Start();
    }

    //������ �� 
    public override void OnFlipped()
    {
        //rectTransform.Rotate(new Vector3(0, 180, 0));
        //rectTransform.gameObject.transform.Rotate(new Vector3(0, 180, 0));
        try
        {

            text.text = dc.hint;

            //print(gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.name);
            cardBackgroundImage.sprite = cardPanelBack;
            cardImage.sprite = characterImg;
        }

        catch
        {
            Debug.LogError("DISAPPROVE: FLIPPING ERROR");
        }
    }

    public override void OnFlipBack()
    {
        //print("FLIPBACK");
        //rectTransform.Rotate(new Vector3(0, -180, 0));
        try
        {
            text.text = dc.answer;

            cardBackgroundImage.sprite = cardPanelFront;
            cardImage.sprite = cardSprite;

        }
        catch
        {
            Debug.LogError("DISAPPROVE: BACKFLIPPING ERROR");
        }
        //Debug.Log("DC:ANSWER::" + dc.answer);
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

    protected override void Update()
    {
        base.Update();

        if (dc == null)
        {
            dc = gameObject.GetComponent<DisapproveCard>();
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
            resultCardComponent.LoadImage(this.king_id, this.event_id, "disagree");
        }
        else
        {
            Debug.LogError("ResultCard which is DisapproveCard component not found on the popup prefab!");
        }
    }

    protected override void LoadCardPanelBackground(ref Sprite cardFront, ref Sprite cardBack)
    {
        base.LoadCardPanelBackground(ref cardFront, ref cardBack);
    }
}
