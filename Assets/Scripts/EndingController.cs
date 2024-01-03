using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{
    // Needed
    // 1. Go to Main
    // 2. Print the Final King
    // 3. Scrolling Credit

    public Button goToMain;
    public Text finalKingText; // To Print the Final King
    public GameManager gameManager;
    public GameObject creditScroller; // Object for the Credit Scroll

    void Start()
    {
        //gameManager = gameObject.GetComponent<GameManager>();
        //goToMain = GetComponent<Button>();
        gameManager = FindObjectOfType<GameManager>();
        goToMain.onClick.AddListener(GoToMain); // 버튼에 이벤트 리스너 추가
        DisplayFinalKing();
        StartCreditScroll();
    }

    void Update()
    {
        // Go to Main
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameManager.SetFinalKingName(null);
            GoToMain();
        }
    }

    void GoToMain()
    {
        // SetNextStage("MainScene")
        SceneManager.LoadScene("Intro");
    }

    void DisplayFinalKing()
    {
        finalKingText.text = gameManager.GetFinalKingName();
    }

    void StartCreditScroll()
    {
        // 크레딧 스크롤 시작
        creditScroller.SetActive(true);
    }
}
