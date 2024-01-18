using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    public static float moveScaleVariable = 1.0f;

    public UIController controller;
    Scenarios scenarios;

    static int royal = 50;
    static int finance = 50;
    static int property = 50; // index

    public static int resultPanelWaitTime = 4;
    public static int kingDeadUIPanelWaitTime = 5;
    public static int scenarioIndex = 0;
    public static float waitSecondsOfTestamentResultPanel = 8f;

    public static bool isGaugeValueChanged = false;
    public static int kingIndex = 1;

    public static float timerDuration = 15f;

    public bool TESTKingDeadTest = false;
    public bool TESTEnding = false;

    public static int endingScenarioIndex = 28;

    // Test Progress Bar
    public bool TESTProgress = false;
    public int TESTStage = 0;

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

    public static int Popularity
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
        scenarioIndex = 0;
        if (timerDuration>15)
            timerDuration = 15;

        Royal = 50;
        Popularity = 50;
        Finance = 50;
        /*
        if (GameObject.FindGameObjectWithTag("Stage") == null)
        {
            var stage = new GameObject();
            DontDestroyOnLoad(stage);
            stage.name = scenarioIndex.ToString();
            stage.tag = "Stage";

        }
        else
        {
            scenarioIndex = int.Parse(GameObject.FindGameObjectWithTag("Stage").name);
        }
        */
        controller = gameObject.GetComponent<UIController>();

        //load json String Assets/Data/scenario.json
        var jsonString = Resources.Load("scenario").ToString();

        scenarios = JsonConvert.DeserializeObject<Scenarios>(jsonString);

        Debug.Log("SCENARIOS LENGTH:" + scenarios.scenarios.Length);

        ThisWorldEventController.OnChooseFailed.AddListener(new UnityEngine.Events.UnityAction(OutOfTimePanelty));

        //Register Callbacks
        GameObject.Find("LeftUpper").GetComponent<BaseGaugeController>().ChangePercentages((float)Royal, (float)Popularity, (float)Finance);

        ThisWorldEventController.OnGameOver.AddListener(new UnityAction(CallEndingScene));
        ThisWorldEventController.OnTestament1Selected.AddListener(new UnityAction(OnTestament1));
        ThisWorldEventController.OnTestament2Selected.AddListener(new UnityAction(OnTestament2));

        if(TESTKingDeadTest)
        {
            scenarioIndex = 13;
        }

        if(TESTEnding)
        {
            scenarioIndex = 27;
        }

        if (TESTProgress)
        {
            scenarioIndex = TESTStage;
        }

    }

    private void Update()
    {
        if (isGaugeValueChanged)
        {
            isGaugeValueChanged = false;

            //Call Gauge Controller
            GameObject.Find("LeftUpper").GetComponent<BaseGaugeController>().ChangePercentages((float)Royal, (float)Popularity, (float)Finance);
        }

        if (scenarioIndex >= endingScenarioIndex )
        {
            if(GameObject.Find("LeftUpper").GetComponent<BaseGaugeController>().CheckAllZeroPoint())
            {
                ThisWorldEventController.OnGameOver?.Invoke();
            }
            else SceneManager.LoadScene("EndingSuccess");
        }
        /*
        if (GameObject.FindGameObjectWithTag("Stage") != null)
        {
            GameObject.FindGameObjectWithTag("Stage").name = scenarioIndex.ToString();
        }*/
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
        SceneManager.LoadScene("Ending");
    }

    public void OutOfTimePanelty()
    {
        Royal -= 10;
        Popularity -= 10;
        Finance -= 10;
        //�г�Ƽ �� ���� ����   
        GameObject.Find("LeftUpper").GetComponent<BaseGaugeController>().ChangePercentages((float)Royal, (float)Popularity, (float)Finance);
    }

    public void OnTestament1()
    {
        //Testament1: add time
        timerDuration += 5;

        Royal = 50;
        Popularity = 50;
        Finance = 50;
        //Testament2: set default gauge
        GameObject.Find("LeftUpper").GetComponent<BaseGaugeController>().ChangePercentages((float)Royal, (float)Popularity, (float)Finance);
    }

    public void OnTestament2()
    {
        Royal = 50;
        Popularity = 60;
        Finance = 50;
        //Testament2: set default gauge
        GameObject.Find("LeftUpper").GetComponent<BaseGaugeController>().ChangePercentages((float)Royal, (float)Popularity, (float)Finance);
    }
}
