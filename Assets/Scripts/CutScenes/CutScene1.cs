using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CutScene1 : BaseCutScene
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override IEnumerator CutSceneSequence()
    {
        dialogueBackground.SetActive(false);
        yield return new WaitForSeconds(2f); // Delay before first dialogue
        dialogueBackground.SetActive(true);

        // First Dialogue
        ShowDialogue("My name is David. A common novelist of modern times.");
        yield return new WaitUntil(() => finished_dialog); // Wait until first dialogue is finished
        finished_dialog = false;

        // Second Dialogue
        ShowDialogue("The company received a recommendation for performance.Damn it."); // Show second dialogue
        yield return new WaitUntil(() => finished_dialog);
        finished_dialog = false;

        // Third Dialogue
        ShowDialogue("I went to the library to find the same material."); // Show third dialogue
        yield return new WaitUntil(() => finished_dialog);
        finished_dialog = false;


        // Make it black
        dialogueBackground.SetActive(true);
        base.FadeToBlack();
        yield return new WaitForSeconds(0.2f);
        dialogueBackground.SetActive(false);

        current_CUTSCENE.SetActive(false);
        next_CUTSCENE.SetActive(true);
    }
}
