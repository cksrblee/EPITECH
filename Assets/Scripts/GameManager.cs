using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static float moveScaleVariable = 1.0f;

    public UIController controller;
    Scenarios scenarios;

    static int royal = 50;
    static int finance = 50;
    static int property = 50; // index

    public static int resultPanelWaitTime = 4;
    public static int scenarioIndex = 0;

    public static bool isGaugeValueChanged = false;
    public static int kingIndex = 1;

    public static int Royal
    {
        get => royal;

        set
        {
            royal = value;
            isGaugeValueChanged = true;
            ThisWorldEventController.OnRoyalVariableChanged.Invoke();

        }
    }

    public static int Finance
    {
        get => finance;

        set
        {
            finance = value;
            isGaugeValueChanged = true;
            ThisWorldEventController.OnFinanceVariableChanged.Invoke();
        }
    }

    public static int Property
    {
        get => property;

        set
        {
            property = value;
            isGaugeValueChanged = true;
            ThisWorldEventController.OnPropertyVariableChanged.Invoke();
        }
    }


    private void Awake()
    {
        controller = gameObject.GetComponent<UIController>();

        //load json String Assets/Data/scenario.json
        var jsonString = Resources.Load("scenario").ToString();

        scenarios = JsonConvert.DeserializeObject<Scenarios>(jsonString);

        Debug.Log("SCENARIOS LENGTH:" + scenarios.scenarios.Length);

        ThisWorldEventController.OnChooseFailed.AddListener(new UnityEngine.Events.UnityAction(OutOfTimePanelty));

        //Register Callbacks
        //GameObject.Find("CenterUpper").GetComponent<BaseGaugeController>().ChangePercent();
    }

    private void Update()
    {
        if (isGaugeValueChanged)
        {
            isGaugeValueChanged = false;

            //Call Gauge Controller
        }

        if (scenarioIndex / 14 > 0 && scenarioIndex % 14 == 1) 
        {
            kingIndex += 1;
            ThisWorldEventController.OnKingDied.Invoke();
            print("KING DEAD" + kingIndex.ToString());
        }
    }


    public UIController GetUIController()
    {
        return this.controller;
    }

    public Scenarios GetScenarios()
    {
        return this.scenarios;
    }

    public static void CallEndingScene()
    {
        SceneManager.LoadScene(3);
    }


    public void OutOfTimePanelty()
    {
        //패널티 줄 내용 정리   
    }
    
}
