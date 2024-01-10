using echo17.EndlessBook;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadingBookMngr : MonoBehaviour
{
    protected EndlessBook book;
    public float stateAnimationTime = 1f; 
    public EndlessBook.PageTurnTimeTypeEnum turnTimeType = EndlessBook.PageTurnTimeTypeEnum.TotalTurnTime;
    public float turnTime = 1f;

    public GameObject InitialCamera;
    public GameObject zoomCamera;

    public GameObject storyUI;

    bool isZoomFinished = false;
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

        if (Input.GetMouseButtonDown(0) && isZoomFinished)
        {
            storyUI.SetActive(true);
        }

    }

    private void OnBookStateChanged(EndlessBook.StateEnum fromState, EndlessBook.StateEnum toState, int pageNumber)
    {
        //throw new NotImplementedException();
    }

    //

    public void OnStartBtnClicked()
    {
        //카메라 호출
        StartCoroutine(CameraMoving(2.1f));
    }


    IEnumerator CameraMoving(float seconds)
    {
        zoomCamera.SetActive(true);

        yield return new WaitForEndOfFrame();

        isZoomFinished = true;
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void loadnextscene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
