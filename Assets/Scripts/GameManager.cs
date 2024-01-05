using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        controller = gameObject.GetComponent<UIController>();

        //load json String Assets/Data/scenario.json
        var jsonString = Resources.Load("scenario").ToString();
        print(jsonString);

        scenarios = JsonConvert.DeserializeObject<Scenarios>(jsonString);

        foreach (Scenario scenario in scenarios.scenarios)
        {
            Debug.Log(scenario.king_id);
            print(scenario.king_name);
            // Further processing here
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
}
