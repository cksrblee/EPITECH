using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float moveScaleVariable = 1.0f;

    public UIController controller;

    private void Awake()
    {
        controller = gameObject.GetComponent<UIController>();
    }

    public UIController GetUIController()
    {
        return this.controller;
    }
}
