using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    UnityEvent OnServantArrived; // ���ϰ� �������� ��

    public Button choose1;
    public Button choose2;

    enum UIStatus
    {
        Normal,
        Dialogue
    }
    private void Awake()
    {
        
    }

    private void OnDialoguePopUp()
    {
        //�ִϸ��̼� ���


    }
}
