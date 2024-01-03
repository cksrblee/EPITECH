using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Subjects : MonoBehaviour
{
    [SerializeField]
    private Vector3 startPosi;
    [SerializeField]
    private Vector3 endPosi;

    private GameObject subjectObj; // for Test

    private float moveScale;
    private Vector3 direction;

    enum SubjectStatus
    {
        Moving,
        Stopping
    }

    private SubjectStatus status;

    public bool TestChangeDirection = false;

    public UnityEvent OnSubjectFinishConversation; // 신하가 뒤로 물러갈 떄 유니티 이벤트

    // Start is called before the first frame update
    void Start()
    {
        subjectObj = gameObject;
        subjectObj.transform.position = startPosi;

        moveScale = GameManager.moveScaleVariable;
        direction = Vector3.up;
        status = SubjectStatus.Moving;

    }

    // Update is called once per frame
    void Update()
    {
        //subjectObj 의 좌표를 startPosi 에서 endPosi로 Time.deltaTime으로 이동시킴
        if(status == SubjectStatus.Moving) subjectObj.transform.position += direction* Time.deltaTime * moveScale;
       
        if(status == SubjectStatus.Stopping)
        {
            if(TestChangeDirection)
            {
                status = SubjectStatus.Moving;
                ChangeDiretion();

            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (subjectObj != null)
        {
            Debug.Log(collision.gameObject.name);
            if (collision.gameObject.name == "FinishPoint")
            {
                //direction = Vector3.down;
                status = SubjectStatus.Stopping;
                /*
                while (true)
                {
                    subjectObj.transform.position = Vector3.Lerp(subjectObj.transform.position, endPosi,  moveScale * Time.deltaTime);
                    if (Mathf.Abs(subjectObj.transform.position.y - endPosi.y) < 0.02f)
                    {
                        status = SubjectStatus.Stopping;
                        break;
                    }

                }*/
            }
        }
        else
        {
            Debug.LogError("subejctObj is NONE");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (subjectObj != null)
        {
            Debug.Log(collision.gameObject.name);

        }
    }

    private void ChangeDiretion()
    {
        direction = Vector3.down;
    }


}
