using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutScene3 : MonoBehaviour
{
    public GameObject bus;
    public GameObject dialogueBackground;
    public GameObject dialogueCharName;
    public GameObject main_character;
    public GameObject current_CUTSCENE;
    public GameObject next_CUTSCENE;

    public Image fadeImage;

    public TextMeshProUGUI dialogueText;
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

    // @Brief : It is kind of Update in CS230
    IEnumerator CutSceneSequence()
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
        yield return new WaitForSeconds(2f);

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

    // @Brief :It changes the color of text
    void ChangeTextColor(Color newColor)
    {
        dialogueText.color = newColor;
    }

    // @Brief : It shows the dialogue panal and the text 
    void ShowDialogue(string text)
    {
        // Activate Text UI and text
        dialogueBackground.SetActive(true);
        dialogueCharName.SetActive(true);
        dialogueText.gameObject.SetActive(true);

        // makes the opacity to 1
        dialogueText.color = new Color(dialogueText.color.r, dialogueText.color.g, dialogueText.color.b, 1);

        // Typing effect starts
        StartCoroutine(TypeSentence(text));

        // Deactivate after the effect
        DOVirtual.DelayedCall(text.Length * 0.05f + 1f, () => {
            dialogueText.DOFade(0, 0.5f).OnComplete(() => {
                dialogueBackground.SetActive(false);
                dialogueCharName.SetActive(false);
                dialogueText.gameObject.SetActive(false);
                finished_dialog = true;
            });
        });
    }
}