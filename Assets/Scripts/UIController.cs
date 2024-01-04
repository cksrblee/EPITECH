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
        //�ִϸ��̼� ���


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
