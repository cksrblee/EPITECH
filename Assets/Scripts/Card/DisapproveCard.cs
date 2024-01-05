using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapproveCard : BaseCard
{
    public override void Build(Answer answer, Hint hint, Reaction reaction, Effect effect)
    {
        this.answer = answer.disagree;
        this.hint = hint.disagree;
        this.reaction = reaction.disagree;
        this.effect = effect.disagree;
    }

    

}
