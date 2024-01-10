using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThisWorldEventController : MonoBehaviour
{
    public static UnityEvent OnUserChooseCards = new UnityEvent();
    public static UnityEvent OnResultFinished = new UnityEvent();
    public static UnityEvent OnResultPanelOpened = new UnityEvent();
    public static UnityEvent OnKingDied = new UnityEvent();
    public static UnityEvent OnGameOver = new UnityEvent();
    public static UnityEvent OnChooseFailed = new UnityEvent();
    public static UnityEvent OnGaugeChanged = new UnityEvent();
    public static UnityEvent OnRoyalVariableChanged = new UnityEvent();
    public static UnityEvent OnFinanceVariableChanged = new UnityEvent();
    public static UnityEvent OnPropertyVariableChanged = new UnityEvent();
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
