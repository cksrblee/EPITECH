using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproveCard : BaseCard
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Build(Answer answer, Hint hint, Reaction reaction, Effect effect)
    {
        this.answer = answer.agree;
        this.hint = hint.agree;
        this.reaction = reaction.agree;
        this.effect = effect.agree;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
