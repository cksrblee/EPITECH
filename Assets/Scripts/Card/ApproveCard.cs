using Newtonsoft.Json;
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

    bool isFlipping = false;
    public override void Build(Answer answer, Hint hint, Reaction reaction, Effect effect, Transform plateTransform, string king_id, string event_id)
    {
        var obj = Instantiate(Resources.Load("Prefabs/Card", typeof(GameObject)) as GameObject, plateTransform);
        ac = obj.AddComponent<ApproveCard>();

        ac.answer = answer.agree;
        ac.hint = hint.agree;
        ac.reaction = reaction.agree;
        ac.effect = effect.agree; 
        ac.king_id = king_id;
        ac.event_id = event_id;

        obj.name = "ApproveCard";


        canvas = obj.transform.GetChild(0).transform.GetChild(0);

        cardImage = canvas.transform.GetChild(0).GetComponent<Image>();
        text1 = canvas.transform.GetChild(1);
        text = text1.gameObject.GetComponent<TextMeshProUGUI>();
        rectTransform = text1.gameObject.GetComponent<RectTransform>();
        text.text = ac.answer;

        // Load and set the image
        string cardSpritePath = "Illustrate/David_1st/" + $"{king_id}-{event_id}(agree)"; // Make sure to use the correct naming scheme here
        Sprite cardSprite = Resources.Load<Sprite>(cardSpritePath);
        if (cardSprite != null)
        {
            cardImage.sprite = cardSprite;
        }
        else
        {
            Debug.LogError("Sprite not found at path: " + cardSpritePath);
        }
    }

    //������ �� 
    public override void OnFlipped()
    {
        //rectTransform.Rotate(new Vector3(0, 180, 0));
        rectTransform.gameObject.transform.Rotate(new Vector3(0, 180, 0));

        text.text = ac.hint;
    }

    public override void OnFlipBack()
    {
        rectTransform.Rotate(new Vector3(0, -180, 0));

        text.text = ac.answer;
        //Debug.Log("DC:ANSWER::" + ac.answer);
    }
    private IEnumerator WaitAndFlip()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.5f);

        OnFlipped();
        isFlipping = false;
    }

    private IEnumerator WaitAndFlipBack()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.5f);

        OnFlipBack();
        isFlipping = false;
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

        if (ac == null)
        {
            ac = gameObject.GetComponent<ApproveCard>();
        }

        if (text == null)
        {
            canvas = gameObject.transform.GetChild(0).GetChild(0);
            text = canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
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
        Debug.Log("CARD CLICKED");
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
}
