using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Resources;
using System.Runtime.Serialization;
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
    string finalCardName;
    bool isLoadImageFinished = false;
    bool isApplied = false;
    Sprite resultSprite;
    Transform final_card;

    public void Awake()
    {
        rectTransform = GameObject.Find("InGameUI").GetComponent<RectTransform>();

        ThisWorldEventController.OnResultPanelOpened.Invoke();
    }

    public void Start()
    {
        // Scale the card up to 1.2 times over a duration of time (e.g., 0.5 seconds)
        transform.DOScale(new Vector3(1.0f, 1.0f, 1f), 0.5f).SetEase(Ease.Linear);

        StartCoroutine(FinishResultPanel()); // �����⸦ ��ٸ��� �̺�Ʈ �ݹ��� ��û�ϴ� �Լ�
    }

    public void Build(string answer, string reaction, EffectAnswer[] effect)
    {
        this.answer = answer;

        this.react = reaction;
        this.eff = effect;

        var obj = gameObject;

        canvas = obj.transform.GetChild(1).transform.GetChild(0); 
        text1 = canvas.transform.GetChild(1);
        text = text1.gameObject.GetComponent<TextMeshProUGUI>();
        rectTransform = text1.gameObject.GetComponent<RectTransform>();
        text.text = this.react;
    }

    public void LoadImage(string king_id, string event_id, string agreeORnot)
    {
        // Correct the path and load Sprites, not Images
        finalCardName = $"{king_id}-{event_id}({agreeORnot})";
        resultSprite = Resources.Load<Sprite>("Illustrate/David_1st/" + finalCardName);
        
        isLoadImageFinished = true;
    }

    IEnumerator ApplySprite(Transform final_card, Sprite result)
    {
        final_card.gameObject.GetComponent<Image>().sprite = result;
        yield return new WaitForSeconds(0.1f);

        isApplied = true;
    }

    public void Update()
    {
        if(isLoadImageFinished && !isApplied)
        {
            final_card = gameObject.transform.GetChild(1).GetChild(0).GetChild(0);
            StartCoroutine(ApplySprite(final_card, resultSprite));
        }
    }

    IEnumerator FinishResultPanel()
    {
        yield return new WaitForSeconds(GameManager.resultPanelWaitTime);

        Destroy(this.gameObject);

        yield return new WaitForEndOfFrame();
        try
        {
            ThisWorldEventController.OnResultFinished.Invoke();
        }
        catch
        {
            Debug.LogError("ONRESULT FINISHED EVENT INVOKE ERROR");
        }
        

    }


    private void OnDestroy()
    {
        // ������ �Է�
        EffectAnswer[] effAnswers = this.eff;
        
        foreach (EffectAnswer effAnswer in effAnswers)
        {
            //print(effAnswer.property);
            //print(effAnswer.num);

            if(effAnswer.property == "royal" || effAnswer.property == "Royal")
            {
                GameManager.Royal += effAnswer.num;
            }

            else if (effAnswer.property == "Finance" || effAnswer.property == "finance")
            {
                GameManager.Finance += effAnswer.num;
            }

            else if (effAnswer.property == "Popularity" || effAnswer.property == "popularity")
            {
                GameManager.Popularity += effAnswer.num;
            }
        }
        
        if (GameManager.scenarioIndex % 15 != 13) // 배열이므로 -1, 이 함수가 끝나고 AddSceneIndex가 실행되므로 -1 -> 총 -2
        {
            ThisWorldEventController.OnResultFinished?.Invoke();
        }

        else
        {
            try
            {
                ThisWorldEventController.OnKingDied?.Invoke();
            }
            catch (Exception e)
            {
                print(e.ToString());
            }
        }
    }
}
