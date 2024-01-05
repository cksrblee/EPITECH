using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseCard : MonoBehaviour
{
    // Start is called before the first frame update
    public Image background;
    public Image cardIllustration;  // 
    protected string answer;
    protected string hint;
    protected string reaction;
    protected string effect;

    TextMeshProUGUI text;
    protected Status status;
    protected enum Status
    {
        IDLE,
        HINT
    }
    public virtual void Build(Answer answer, Hint hint, Reaction reaction, Effect effect)  { }

    protected virtual void TurnCard()
    {
        //Turn This Card
    }
    
    protected virtual void ChangeTxt()
    {
        //Change Text
        if (this.status == Status.IDLE)
        {
            text.text = answer;
        }

        else if (this.status == Status.HINT)
        {
            text.text = hint;
        }

    }

    public void OnMouseOverCard()
    {
        //ī�� ������Ʈ ���� ���콺�� ����� ��
        //TurnCard: ī�� ����
        //ChangeTxt: �ؽ�Ʈ ����
    }

    public void OnMouseLeft()
    {

    }
}
