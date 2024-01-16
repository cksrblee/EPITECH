using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutScene3 : BaseCutScene
{
    public GameObject bus;
    public GameObject dialogueCharName;
    public GameObject main_character;

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
        ShowDialogue("The weather looks gloomy... I better head home quickly.");
        yield return new WaitUntil(() => finished_dialog); // Wait until first dialogue is finished

        // Animate Bus
        main_character.SetActive(false);
        bus.transform.DOScale(new Vector3(8.0f, 8.0f, 8.0f), 1.5f);
        yield return new WaitForSeconds(1.5f); // Wait for bus animation
        finished_dialog = false;

        // Second Dialogue
        main_character.SetActive(true);
        ShowDialogue("Oh..? Uh-uh-uh???"); // Show second dialogue
        yield return new WaitUntil(() => finished_dialog);
        main_character.SetActive(false);

        // Make it black
        FadeToBlack();
        yield return new WaitForSeconds(0.2f);

        current_CUTSCENE.SetActive(false);
        next_CUTSCENE.SetActive(true);
    }

}