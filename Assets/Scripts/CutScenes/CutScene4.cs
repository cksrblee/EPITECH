using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutScene4 : BaseCutScene
{

    // david,servant
    public TextMeshProUGUI DavidText;
    public TextMeshProUGUI servantText;


    // @Brief : This is Load in CS230 Engine and the DOTween only uses the Start()
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
        DavidText.gameObject.SetActive(false);
        servantText.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f); // Delay before first dialogue
        dialogueBackground.SetActive(true);



        // First Dialogue
        DavidText.gameObject.SetActive(true);
        ShowDialogue("????????!?!?!!?!?!!?!?!??!!?!!?!!?!!!?!?!");
        yield return new WaitUntil(() => finished_dialog); // Wait until first dialogue is finished
        finished_dialog = false;
        DavidText.gameObject.SetActive(false);

        // Second Dialogue
        DavidText.gameObject.SetActive(true);
        ShowDialogue("What is this...?"); // Show second dialogue
        yield return new WaitUntil(() => finished_dialog);
        finished_dialog = false;
        DavidText.gameObject.SetActive(false);

        // Third Dialogue
        servantText.gameObject.SetActive(true);
        ShowDialogue("Your Majesty, are you awake? It's time for an inspection."); // Show third dialogue
        yield return new WaitUntil(() => finished_dialog);
        finished_dialog = false;
        servantText.gameObject.SetActive(false);

        // Four Dialogue
        DavidText.gameObject.SetActive(true);
        ShowDialogue("Where are we?");
        yield return new WaitUntil(() => finished_dialog);
        finished_dialog = false;
        DavidText.gameObject.SetActive(false);

        // Five Dialogue
        servantText.gameObject.SetActive(true);
        ShowDialogue("What do you mean? This is the Kingdom of France, you are David I.");
        yield return new WaitUntil(() => finished_dialog);
        finished_dialog = false;
        servantText.gameObject.SetActive(false);

        // Six Dialogue
        DavidText.gameObject.SetActive(true);
        ShowDialogue("What?????!!!!!");
        yield return new WaitUntil(() => finished_dialog);
        finished_dialog = false;
        DavidText.gameObject.SetActive(false);


        // Make it black
        dialogueBackground.SetActive(true);
        FadeToBlack();
        yield return new WaitForSeconds(0.2f);
        dialogueBackground.SetActive(false);

        current_CUTSCENE.SetActive(false);
        //next_CUTSCENE.SetActive(true);
        GameObject.Find("BookMngr").GetComponent<ReadingBookMngr>().loadnextscene();
    }

}
