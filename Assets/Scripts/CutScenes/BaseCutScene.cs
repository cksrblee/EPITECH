using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseCutScene : MonoBehaviour, IPointerClickHandler
{
    public GameObject current_CUTSCENE;
    public GameObject next_CUTSCENE;

    public GameObject dialogueBackground;
    public TextMeshProUGUI dialogueText;

    protected bool finished_dialog = false;

    public AudioSource typingSoundEffect; // Assign this in the inspector

    public Image fadeImage;
    protected bool isClicked = false;
    // Start is called before the first frame update
    protected virtual void Start()
    {

        // Initialize
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
        fadeImage.gameObject.SetActive(true);
        ChangeTextColor(Color.black);

        // Updates
        StartCoroutine(CutSceneSequence());
    }

    protected virtual void Update()
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isClicked = true;
    }

    protected void ChangeTextColor(Color newColor)
    {
        dialogueText.color = newColor;
    }

    // @Brief : It is kind of Update in CS230
    protected virtual IEnumerator CutSceneSequence()
    { 
        yield return null;
    }

    protected void FadeToBlack()
    {
        // Animate the alpha value to 1 over 2 seconds
        fadeImage.DOFade(1, 2f);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // Wait time between characters
            if (isClicked == true)
            {
                dialogueText.text = sentence;
                isClicked = false;
                break;
            }
        }
    }

    // @Brief : It shows the dialogue panal and the text 
    public void ShowDialogue(string text)
    {
        // Activate Text UI and text
        dialogueBackground.SetActive(true);
        dialogueText.gameObject.SetActive(true);

        // makes the opacity to 1
        dialogueText.color = new Color(dialogueText.color.r, dialogueText.color.g, dialogueText.color.b, 1);

        // Typing effect starts
        //typingSoundEffect.Play(); // Play the typing sound effect
        StartCoroutine(TypeSentence(text));


        // Deactivate after the effect
        DOVirtual.DelayedCall(text.Length * 0.05f + 1.5f, () => {
            dialogueText.DOFade(0, 0.5f).OnComplete(() => {
                //typingSoundEffect.Stop(); // Stop the typing sound effect if we're finishing
                dialogueBackground.SetActive(false);
                dialogueText.gameObject.SetActive(false);
                finished_dialog = true;

            });
        });

    }


}
