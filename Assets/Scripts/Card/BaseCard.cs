using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator animator;
    // Start is called before the first frame update
    public Image background;
    public Image cardIllustration;  // 
    protected string answer;
    protected string hint;
    protected string reaction;
    protected EffectAnswer[] effect;

    protected Status status;

    protected bool isFlipped = false;
    protected bool isComingUp = false;
    float time;
    protected bool isMouseOnCard = false;
    protected bool isCursorOn = false;
    protected enum Status
    {
        IDLE,
        HINT
    }

    private GameObject cursorIcon;
    private Camera mainCamera;
    public virtual void Build(Answer answer, Hint hint, Reaction reaction, Effect effect, Transform PlateTransform)  { }


    protected virtual void ChangeTxt()
    {
    }

    protected virtual void SelectImage() { }


    protected virtual void Start()
    {
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();

        mainCamera = Camera.main;

        // Ŀ�� �������� �����ϰ� ���� �� ��Ȱ��ȭ�մϴ�.
        cursorIcon = Instantiate(Resources.Load<GameObject>("Stamp"), this.gameObject.transform);
        cursorIcon.SetActive(false);

        var btn = gameObject.transform.GetChild(0).GetComponent<Button>();

        btn.onClick.AddListener(OnCardClicked);
    }
    protected virtual void Update()
    {
        time += Time.deltaTime;

        if (isMouseOnCard)
        {
            if (isCursorOn)
            {
                cursorIcon.SetActive(true);
                // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ�մϴ�.
                Vector3 cursorPos = Input.mousePosition;
                cursorPos.z = -2; // Z���� 0���� �����Ͽ� 2D ��鿡 �°� �����մϴ�.

                // Ŀ�� �������� ��ġ�� ������Ʈ�մϴ�.
                cursorIcon.transform.position = cursorPos;
            }
        }

        else
        {
            //Ŀ�� ����
            cursorIcon.SetActive(false);
        }
    }

    public virtual void OnFlipped() {}

    public virtual void OnFlipBack() {}
    public void OnPointerEnter(PointerEventData eventData)
    {
        print("ENTER");
        isMouseOnCard = true;
        isCursorOn = true;
        if (time > 0.5f && !isFlipped)
        {
            animator.SetTrigger("Flip");

            isFlipped = true;
            isComingUp = true; // ù���� ���� ����

            //���� ����
            status = Status.HINT;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("EXIT");
        isMouseOnCard = false;
        isCursorOn = false;
        if (time > 0.6f && isComingUp && isFlipped)
        {
            animator.SetTrigger("FlipBack");
            time = 0;
            isFlipped = false;

            status = Status.IDLE;
        }
    }

    public virtual void OnCardClicked()
    {
        Debug.Log("CARD CLICKED");
        var obj = Resources.Load<GameObject>("popup");

        var uitresform = GameObject.Find("InGameUI").transform;
        Instantiate(obj, uitresform);


    }
    
}
