using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResultCard : MonoBehaviour
{
    Transform canvas;
    Transform text1;
    TextMeshProUGUI text;
    RectTransform rectTransform;
    ResultCard popup_rc;
    string react;
    EffectAnswer[] eff;
    string answer;

    public void Awake()
    {
        rectTransform = GameObject.Find("InGameUI").GetComponent<RectTransform>();
    }

    public void Build(string answer, string reaction, EffectAnswer[] effect)
    {
        this.answer = answer;

        this.react = reaction;
        this.eff = effect;

        var obj = gameObject;

        canvas = obj.transform.GetChild(0).transform.GetChild(0); 
        text1 = canvas.transform.GetChild(1);
        text = text1.gameObject.GetComponent<TextMeshProUGUI>();
        rectTransform = text1.gameObject.GetComponent<RectTransform>();
        text.text = this.react;
    }

    public void LoadImage(string king_id, string event_id, string answer)
    {
        // Correct the path and load Sprites, not Images
        Sprite resultSprite = Resources.Load<Sprite>("Illustrate/David_1st/" + king_id);

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