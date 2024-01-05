using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public UnityEvent OnServantArrived; // ���ϰ� �������� ��
    public UnityEvent OnUserChose; //������ ������ ������ ��

    public GameObject MainUI;
    public GameObject CardUI;
    public GameObject ClockUI;
    public Button choose1;
    public Button choose2;
    public GameObject UIParent;

    int scenarioIndex = 0;
    enum UIStatus
    {
        Normal,
        Dialogue
    }
    private void Awake()
    {
        MainUI.SetActive(false);
        choose1.onClick.AddListener(OnPlatyerSelectYes);
        choose2.onClick.AddListener(OnPlayerSelectNo);

        StartCoroutine(InstantiateCardAndTimer());
        //InvokeRepeating("TikRotationAnim", 30, 10);
    }


    private void OnDialoguePopUp()
    {
        //�ִϸ��̼� ���


    }
    IEnumerator InstantiateCardAndTimer()
    {
        //ClockUI.SetActive(true);
        //CardUI.SetActive(true);
        var temp1=GameObject.Instantiate(CardUI, UIParent.transform);
        yield return new WaitForEndOfFrame();
        var temp = GameObject.Instantiate(ClockUI, UIParent.transform);


        var time = 0.0f;

        while (true)
        {
            time += Time.deltaTime;
            yield return null;
            Debug.Log(time);
            if(time > 10)
            {

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
    private void OnPlatyerSelectYes()
    {
        OnUserChose.Invoke();
    }

    private void OnPlayerSelectNo()
    {
        OnUserChose.Invoke();
    }

}
