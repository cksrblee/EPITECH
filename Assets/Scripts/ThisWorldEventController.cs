using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThisWorldEventController : MonoBehaviour
{
    public static UnityEvent OnUserChooseCards = new UnityEvent();
    public static UnityEvent OnResultFinished = new UnityEvent();
    public static UnityEvent OnResultPanelOpened = new UnityEvent();
    public static UnityEvent OnKingDied = new UnityEvent(); // 왕이 죽었을 경우 시나리오 인덱스 15, 30 떄 실행
    public static UnityEvent OnRestartGame = new UnityEvent();
    public static UnityEvent OnGameOver = new UnityEvent();
    public static UnityEvent OnChooseFailed = new UnityEvent();
    public static UnityEvent OnGaugeChanged = new UnityEvent();
    public static UnityEvent OnSceneIndexChanged = new UnityEvent();
    public static UnityEvent OnRoyalVariableChanged = new UnityEvent();
    public static UnityEvent OnFinanceVariableChanged = new UnityEvent();
    public static UnityEvent OnPropertyVariableChanged = new UnityEvent();
    

}
