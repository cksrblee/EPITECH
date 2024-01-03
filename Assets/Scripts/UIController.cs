using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public UnityEvent OnServantArrived; // 신하가 도착했을 떄
    public UnityEvent OnUserChose; //유저의 선택이 끝났을 때

    public GameObject MainUI;
    public Button choose1;
    public Button choose2;

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
    }


    private void OnDialoguePopUp()
    {
        //애니메이션 재생


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
