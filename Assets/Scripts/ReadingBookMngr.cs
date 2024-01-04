using echo17.EndlessBook;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputManagerEntry;

public class ReadingBookMngr : MonoBehaviour
{
    protected EndlessBook book;
    public float stateAnimationTime = 1f; 
    public EndlessBook.PageTurnTimeTypeEnum turnTimeType = EndlessBook.PageTurnTimeTypeEnum.TotalTurnTime;
    public float turnTime = 1f;

    public GameObject zoomCamera;
    public GameObject lastCam;

    public GameObject[] puff;

    int newPageNumber = 0;
    // Start is called before the first frame update
    void Start()
    { 
        book = GameObject.Find("Book").GetComponent<EndlessBook>();
        
    }

    // Update is called once per frame
    void Update()
    {
        EndlessBook.StateEnum newState = EndlessBook.StateEnum.OpenMiddle;
        bool changeState = false;
        if (changeState)
        {
            book.SetState(newState, animationTime: stateAnimationTime, onCompleted: OnBookStateChanged);
        }

        bool turnToPage = false;

        if (Input.GetMouseButtonDown(0)) { 
            turnToPage = true; newPageNumber += 2;
            Debug.Log(turnToPage);
            print(newPageNumber);
        }

        if (newPageNumber > 5)
        {
            changeState = true; newState = EndlessBook.StateEnum.ClosedBack;
            //book.TurnBackward(turnTime,
            //       onCompleted: OnBookTurnToPageCompleted,
            //       onPageTurnStart: OnPageTurnStart,
            //      onPageTurnEnd: OnPageTurnEnd);

            turnToPage = false;
            book.SetState(newState, animationTime: stateAnimationTime, onCompleted: OnBookStateChanged);
            
            lastCam.SetActive(true);
            zoomCamera.SetActive(false);

            TurnOnPuff(3);
        }

        //페이지 4개 가정

        if (turnToPage)
        {
            book.TurnToPage(newPageNumber, turnTimeType, turnTime,
                openTime: stateAnimationTime,
                onCompleted: OnBookTurnToPageCompleted,
                onPageTurnStart: OnPageTurnStart,
                onPageTurnEnd: OnPageTurnEnd
                );
        }
        
    }

    private void OnBookStateChanged(EndlessBook.StateEnum fromState, EndlessBook.StateEnum toState, int pageNumber)
    {
        //throw new NotImplementedException();
    }

    private void OnPageTurnEnd(echo17.EndlessBook.Page page, int pageNumberFront, int pageNumberBack, int pageNumberFirstVisible, int pageNumberLastVisible, echo17.EndlessBook.Page.TurnDirectionEnum turnDirection)
    {
        //throw new NotImplementedException();
    }

    private void OnPageTurnStart(echo17.EndlessBook.Page page, int pageNumberFront, int pageNumberBack, int pageNumberFirstVisible, int pageNumberLastVisible, echo17.EndlessBook.Page.TurnDirectionEnum turnDirection)
    {
        //throw new NotImplementedException();
    }

    private void OnBookTurnToPageCompleted(EndlessBook.StateEnum fromState, EndlessBook.StateEnum toState, int pageNumber)
    {
        //throw new NotImplementedException();
    }

    //

    public void OnStartBtnClicked()
    {
        //카메라 호출
        StartCoroutine(ZoomTheBookInSecs(2.3f));
    }


    IEnumerator ZoomTheBookInSecs(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        zoomCamera.SetActive(true);
    }

    private void TurnOnPuff(int ind)
    {
        StartCoroutine(TurnOnPuffAfterWaiting(3, 3));
    }

    IEnumerator TurnOnPuffAfterWaiting(float seconds, int ind)
    {
        yield return new WaitForSeconds(seconds);

        puff[ind - 1].SetActive(true);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }
}
