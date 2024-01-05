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
        //카드 오브젝트 위에 마우스가 닿았을 때
        //TurnCard: 카드 돌림
        //ChangeTxt: 텍스트 변경
    }

    public void OnMouseLeft()
    {

    }
}
