using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardAnimationController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator animator;
    bool isFlipped = false;
    bool isComingUp = false;
    float time;
    public void OnPointerEnter(PointerEventData eventData)
    {
        print("ENTER");
        if (time > 0.5f && !isFlipped)
        {
            animator.SetTrigger("Flip");

            isFlipped = true;
            isComingUp = true; // ù���� ���� ����

            //���� ����
        }

        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("EXIT");
        if (time > 0.6f && isComingUp && isFlipped)
        {
            animator.SetTrigger("FlipBack");
            time = 0;
            isFlipped= false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }
}
