using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public UnityEvent OnServantArrived; // ���ϰ� �������� ��
    public UnityEvent OnUserChose; //������ ������ ������ ��

    public GameObject MainUI;
    public GameObject CardUIPanel;
    public GameObject ClockUI;
    public Button choose1;
    public Button choose2;
    public GameObject UIParent;
    int scenarioIndex = 0;

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
        choose1.onClick.AddListener(OnPlayerSelectYes);
        choose2.onClick.AddListener(OnPlayerSelectNo);

        InstantiateSelectPanel(); // 수정
        //InvokeRepeating("TikRotationAnim", 30, 10);
    }

    private void InstantiateSelectPanel()
    {
        StartCoroutine(InstantiateCardAndTimer());
    }
    IEnumerator InstantiateCardAndTimer()
    {
        //CardUI.SetActive(true);

        SceneNumText.text = (GetScenarioIndex() + 1).ToString();

        var temp1=GameObject.Instantiate(CardUIPanel, UIParent.transform); //UI Controll에서는 카드 패널을 부를 뿐 카드를 만들지는 않음
        yield return new WaitForEndOfFrame();
        var temp = GameObject.Instantiate(ClockUI, UIParent.transform);

        //ClockUI.SetActive(true);

        var time = 0.0f;

        while (true)
        {
            time += Time.deltaTime;
            yield return null;
            //Debug.Log(time);
            if(time > 10)
            {
                Debug.Log("TIME TO SWTICH");
                //오브젝트 삭제

                Destroy(temp);
                Destroy(temp1);
                //실패

                //CardController 에 이벤트 보내기

                break;
            }
        }

        scenarioIndex++;
        print(scenarioIndex);
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(InstantiateCardAndTimer());
    }

    private void OnPlayerSelectYes()
    {
        OnUserChose.Invoke();
    }

    private void OnPlayerSelectNo()
    {
        OnUserChose.Invoke();
    }

    public int GetScenarioIndex()
    {
        return scenarioIndex;
    }
}
