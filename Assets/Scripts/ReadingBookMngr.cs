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

    // For Background Music
    public AudioSource ButtonSource; // Reference to the AudioSource component
    public AudioSource bgmAudioSource; // Reference to the AudioSource component

    bool isZoomFinished = false;
    // Start is called before the first frame update
    void Start()
    { 
        book = GameObject.Find("Book").GetComponent<EndlessBook>();

        StartCoroutine(CameraMoving(2.1f));

        // Start playing BGM
        bgmAudioSource.Play();
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
            // BOOK BGM STOP HERE
            bgmAudioSource.Stop();
            ButtonSource.Play();
            StartCoroutine(WaitForSeconds(1.0f));
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
        //?????? ????
        StartCoroutine(CameraMoving(2.1f));
    }


    IEnumerator CameraMoving(float seconds)
    {

        yield return new WaitForSeconds(0.2f);
        Debug.Log("sdslnklw");
        zoomCamera.SetActive(true);

        yield return new WaitForEndOfFrame();

        isZoomFinished = true;
    }

    public void LoadMainScene()
    {

        SceneManager.LoadScene("Main");
    }

    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void loadnextscene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        print("THIS IS INDEX : " + currentSceneIndex);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
