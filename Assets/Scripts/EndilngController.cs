namespace echo17.EndlessBook.EndilngController
{
    using echo17.EndlessBook;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class EndilngController : MonoBehaviour
    {
        public EndlessBook book;


        public GameObject virtualCamera;

        public GameObject canvas;

        public float stateAnimationTime = 1f;
        public EndlessBook.PageTurnTimeTypeEnum turnTimeType = EndlessBook.PageTurnTimeTypeEnum.TotalTurnTime;
        public float turnTime = 1f;
        bool changeState = false;
        EndlessBook.StateEnum newState = EndlessBook.StateEnum.OpenMiddle;
        EndlessBook.StateEnum finishState = EndlessBook.StateEnum.ClosedBack;
        void Start()
        {

            changeState = true; newState = EndlessBook.StateEnum.ClosedBack;
            book.TurnToPage(
                book.LastPageNumber,
                turnTimeType,
                turnTime,
                openTime: stateAnimationTime,
                onCompleted: OnBookTurnToPageCompleted,
                onPageTurnStart: OnPageTurnStart,
                onPageTurnEnd: OnPageTurnEnd);


        }

        private void OnPageTurnEnd(Page page, int pageNumberFront, int pageNumberBack, int pageNumberFirstVisible, int pageNumberLastVisible, Page.TurnDirectionEnum turnDirection)
        {
        }

        private void OnPageTurnStart(Page page, int pageNumberFront, int pageNumberBack, int pageNumberFirstVisible, int pageNumberLastVisible, Page.TurnDirectionEnum turnDirection)
        {
        }

        private void OnBookTurnToPageCompleted(EndlessBook.StateEnum fromState, EndlessBook.StateEnum toState, int pageNumber)
        {
            book.SetState(newState, animationTime: stateAnimationTime, onCompleted: OnBookStateChanged);
            virtualCamera.SetActive(true);
            canvas.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnBookStateChanged(EndlessBook.StateEnum fromState, EndlessBook.StateEnum toState, int pageNumber)
        {
            //throw new NotImplementedException();
        }

        public void onrestartclicked()
        {
            SceneManager.LoadScene("Main");

        }


    }
}
