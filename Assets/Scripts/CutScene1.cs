using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutScene1 : MonoBehaviour
{
    public GameObject current_CUTSCENE;
    public GameObject next_CUTSCENE;

    public GameObject dialogueBackground;
    public TextMeshProUGUI dialogueText;

    public Image fadeImage;
    private bool finished_dialog = false;

    // @Brief : This is Load in CS230 Engine and the DOTween only uses the Start()
    void Start()
    {
        // Initialize
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
        fadeImage.gameObject.SetActive(true);
        ChangeTextColor(Color.black);

        // Updates
        StartCoroutine(CutSceneSequence());
    }

    // @Brief :It changes the color of text
    void ChangeTextColor(Color newColor)
    {
        dialogueText.color = newColor;
    }

    // @Brief : It is kind of Update in CS230
    IEnumerator CutSceneSequence()
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
        FadeToBlack();
        yield return new WaitForSeconds(2f);
        dialogueBackground.SetActive(false);

        current_CUTSCENE.SetActive(false);
        next_CUTSCENE.SetActive(true);
    }

    // @Brief : This fades to black using opacity.
    void FadeToBlack()
    {
        // Animate the alpha value to 1 over 2 seconds
        fadeImage.DOFade(1, 2f);
    }

    // @Brief :Types character one by one 
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // Wait time between characters
        }
    }

    // @Brief : It shows the dialogue panal and the text 
    void ShowDialogue(string text)
    {
        // Activate Text UI and text
        dialogueBackground.SetActive(true);
        dialogueText.gameObject.SetActive(true);

        // makes the opacity to 1
        dialogueText.color = new Color(dialogueText.color.r, dialogueText.color.g, dialogueText.color.b, 1);

        // Typing effect starts
        StartCoroutine(TypeSentence(text));

        // Deactivate after the effect
        DOVirtual.DelayedCall(text.Length * 0.05f + 3f, () => {
            dialogueText.DOFade(0, 0.5f).OnComplete(() => {
                dialogueBackground.SetActive(false);
                dialogueText.gameObject.SetActive(false);
                finished_dialog = true;
            });
        });
    }
}
