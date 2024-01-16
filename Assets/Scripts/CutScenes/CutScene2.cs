using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutScene2 : BaseCutScene
{
    // @Brief : This is Load in CS230 Engine and the DOTween only uses the Start()
    protected override void Start()
    {
        base.Start();
    }


    protected override void Update()
    {
        base.Update();
    }
    // @Brief : It is kind of Update in CS230
    protected override IEnumerator CutSceneSequence()
    {
        yield return new WaitForSeconds(2f); // Delay before first dialogue

        // First Dialogue
        ShowDialogue("Have I ever come across a history book like this? It bears the exact same name as mine.");
        yield return new WaitUntil(() => finished_dialog); // Wait until first dialogue is finished
        finished_dialog = false;

        // Second Dialogue
        ShowDialogue("Well, I've been recommended to resign, but what really matters?"); // Show second dialogue
        yield return new WaitUntil(() => finished_dialog);
        finished_dialog = false;

        // Third Dialogue
        ShowDialogue("I might as well borrow this book to clear my head."); // Show third dialogue
        yield return new WaitUntil(() => finished_dialog);
        finished_dialog = false;

        // Make it black
        dialogueBackground.SetActive(true);
        FadeToBlack();
        yield return new WaitForSeconds(0.2f);
        dialogueBackground.SetActive(false);

        current_CUTSCENE.SetActive(false);
        next_CUTSCENE.SetActive(true);
    }
}
