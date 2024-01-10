using System.Collections;
using System.Collections.Generic;
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

    public static int royal = 50;
    public static int finance = 50;
    public static int property = 50; // index

    public static int resultPanelWaitTime = 4;
    public static int scenarioIndex = 0;



    private void Awake()
    {
        controller = gameObject.GetComponent<UIController>();

        //load json String Assets/Data/scenario.json
        var jsonString = Resources.Load("scenario").ToString();

        scenarios = JsonConvert.DeserializeObject<Scenarios>(jsonString);

        Debug.Log("SCENARIOS LENGTH:" + scenarios.scenarios.Length);
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
}
