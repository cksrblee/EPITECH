using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ApproveCard : BaseCard
{
    Transform canvas;
    Transform text1;
    TextMeshProUGUI text;
    RectTransform rectTransform;
    ApproveCard ac;

    bool isFlipping = false;
    public override void Build(Answer answer, Hint hint, Reaction reaction, Effect effect, Transform plateTransform)
    {
        var obj = Instantiate(Resources.Load("Prefabs/Card", typeof(GameObject)) as GameObject, plateTransform);
        ac = obj.AddComponent<ApproveCard>();

        ac.answer = answer.agree;
        ac.hint = hint.agree;
        ac.reaction = reaction.agree;
        ac.effect = effect.agree;

        obj.name = "ApproveCard";


        canvas = obj.transform.GetChild(0).transform.GetChild(0);
        //var image = canvas.transform.GetChild(0);
        text1 = canvas.transform.GetChild(1);
        text = text1.gameObject.GetComponent<TextMeshProUGUI>();
        rectTransform = text1.gameObject.GetComponent<RectTransform>();
        text.text = ac.answer;
    }

    //������ �� 
    public override void OnFlipped()
    {
        print("FLIP");
        //rectTransform.Rotate(new Vector3(0, 180, 0));
        rectTransform.gameObject.transform.Rotate(new Vector3(0, 180, 0));

        text.text = ac.hint;
    }

    public override void OnFlipBack()
    {
        print("FLIPBACK");
        rectTransform.Rotate(new Vector3(0, -180, 0));

        text.text = ac.answer;
        Debug.Log("DC:ANSWER::" + ac.answer);
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

}
