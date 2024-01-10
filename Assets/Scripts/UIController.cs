using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    int scenarioIndex = GameManager.scenarioIndex;

    // Variables for Scene Number
    public GameObject SceneNumBG;
    public TextMeshProUGUI SceneNumText;

    enum UIStatus
    {
        Normal,
        Dialogue
    }
    private void Awake()
    {
        MainUI.SetActive(false);

        InstantiateSelectPanel(); 
        //var openPanelAction = new UnityAction(InstantiateSelectPanel);
        // OnUserChose.AddListener(openPanelAction);
        var openPanelAction = new UnityAction(InstantiateSelectPanel);
        ThisWorldEventController.OnResultFinished.AddListener(openPanelAction);

        var destroyPopUpPanelAction = new UnityAction(DestroyPopUpPanels);
        ThisWorldEventController.OnChooseFailed.AddListener(destroyPopUpPanelAction);
        ThisWorldEventController.OnResultPanelOpened.AddListener(destroyPopUpPanelAction);
    }

    private void InstantiateSelectPanel()
    {
        StartCoroutine(InstantiateCardAndTimer());
    }
    IEnumerator InstantiateCardAndTimer()
    {
        //CardUI.SetActive(true);
        cardUIPanelObj = GameObject.Instantiate(CardUIPanel, UIParent.transform); //UI Controll에서는 카드 패널을 부를 뿐 카드를 만들지는 않음

        SceneNumText.text = (GetScenarioIndex() + 1).ToString();

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
            if(time > 10)
            {
                Debug.Log("TIME TO SWTICH");
                //오브젝트 삭제

                DestroyPopUpPanels();
                //실패

                ThisWorldEventController.OnChooseFailed.Invoke();

                break;
            }
        }

        //시나리오 인덱스 상승
        scenarioIndex++;
        GameManager.scenarioIndex = this.scenarioIndex;

        print(scenarioIndex);
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

}
