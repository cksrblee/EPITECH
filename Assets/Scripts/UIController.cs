using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject MainUI;
    public GameObject CardUIPanel; // prefab
    public GameObject ClockUI; // prefab
    public GameObject UIParent;

    private GameObject cardUIPanelObj;
    private GameObject clockUIPanelObj;

    bool isKingDead = true;
    bool isTestamentSelected = false;
    int scenarioIndex
    {
        get => GameManager.scenarioIndex;

        set
        {
            GameManager.scenarioIndex = value;
        }
    }

    // Variables for Scene Number
    public GameObject SceneNumBG;
    public TextMeshProUGUI SceneNumText;
    public GameObject KingDeadUI;
    public GameObject TestamentResultUI;

    enum UIStatus
    {
        Normal,
        Dialogue
    }
    private void Awake()
    {
        MainUI.SetActive(false);

        InstantiateSelectPanel();  //첫 시작을 위한 코드
        UpdateSceneIndexText();

        //var openPanelAction = new UnityAction(InstantiateSelectPanel);
        // OnUserChose.AddListener(openPanelAction);
        var addSceneIndex = new UnityAction(AddSceneIndex);
        ThisWorldEventController.OnRestartGame.AddListener(addSceneIndex);
        ThisWorldEventController.OnChooseFailed.AddListener(addSceneIndex);
        ThisWorldEventController.OnResultFinished.AddListener(addSceneIndex);

        var openPanelAction = new UnityAction(InstantiateSelectPanel);
        ThisWorldEventController.OnResultFinished.AddListener(openPanelAction);

        ThisWorldEventController.OnRestartGame.AddListener(openPanelAction);

        var destroyPopUpPanelAction = new UnityAction(DestroyPopUpPanels);
        ThisWorldEventController.OnChooseFailed.AddListener(destroyPopUpPanelAction);
        ThisWorldEventController.OnResultPanelOpened.AddListener(destroyPopUpPanelAction);

        ThisWorldEventController.OnChooseFailed.AddListener(openPanelAction);

        var updateSceneIndexUI = new UnityAction(UpdateSceneIndexText);
        ThisWorldEventController.OnSceneIndexChanged.AddListener(updateSceneIndexUI);

        var onKingDead = new UnityAction(OnKingDead);
        ThisWorldEventController.OnKingDied.AddListener(onKingDead);
    }

    private void InstantiateSelectPanel()
    {
        StartCoroutine(InstantiateCardAndTimer());
    }
    IEnumerator InstantiateCardAndTimer()
    {
        //CardUI.SetActive(true);
        cardUIPanelObj = GameObject.Instantiate(CardUIPanel, UIParent.transform); //UI Controll에서는 카드 패널을 부를 뿐 카드를 만들지는 않음


        yield return new WaitForEndOfFrame();
        clockUIPanelObj = GameObject.Instantiate(ClockUI, UIParent.transform);

        //ClockUI.SetActive(true);

        var time = 0.0f;

        while (true)
        {
            if (cardUIPanelObj == null && clockUIPanelObj == null) break;
            time += Time.deltaTime;
            yield return null;
            //Debug.Log(time);
            if(time > GameManager.timerDuration)
            {
                DestroyPopUpPanels();

                if (GameManager.scenarioIndex % 14 != 13) // 배열이므로 -1, 이 함수가 끝나고 AddSceneIndex가 실행되므로 -1 -> 총 -2
                {
                    ThisWorldEventController.OnChooseFailed?.Invoke();
                }

                else
                {
                    GameObject.Find("GameManager").GetComponent<GameManager>().OutOfTimePanelty();
                    ThisWorldEventController.OnKingDied?.Invoke();
                }

                break;
            }
        }
        yield return new WaitForSeconds(0.5f);
    }

    public int GetScenarioIndex()
    {
        return scenarioIndex;
    }

    public void DestroyPopUpPanels()
    {
        Destroy(cardUIPanelObj);
        Destroy(clockUIPanelObj);
    }

    public void OnKingDead()
    {
        print("KING DEAD!!!");
        isKingDead = true;
        //open kingDead UI

        KingDeadUI.SetActive(true);
    }


    public void FinishKingDeadUI()
    {
        isKingDead = false;
        KingDeadUI.SetActive(false);
    }

    public void UpdateSceneIndexText()
    {
        SceneNumText.text = (scenarioIndex).ToString();
    }

    public void AddSceneIndex()
    {
        scenarioIndex++;
        ThisWorldEventController.OnSceneIndexChanged.Invoke();
    }

    public void OnTestament1Selected()
    {
        TestamentResultUI.SetActive(true);
        isTestamentSelected = true;
        FinishKingDeadUI();
        ThisWorldEventController.OnTestament1Selected.Invoke();
    }

    public void OnTestament2Selected()
    {
        TestamentResultUI.SetActive(true);
        isTestamentSelected = true;
        FinishKingDeadUI();
        ThisWorldEventController.OnTestament2Selected.Invoke();
    }

}
