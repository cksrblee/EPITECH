using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public Transform leftCardPlateTransform;
    public Transform rightCardPlateTransform;
    private ApproveCard leftCard;
    private DisapproveCard rightCard;
    private ResultCard resultCard;
    
    // Variables for Pop Up Quiz
    public GameObject quizPanel;
    public Button toggleButton;
    public TextMeshProUGUI QuestionText;

    // For Quiz Animation
    //public Animator quizPanelAnimator; // Reference to the Animator component
    //private bool isPanelOpen = false;

    // For Sound Effect
    public AudioClip card_sound;
    public AudioClip quizSound;
    private AudioSource audio_source;


    public Scenarios scenarios;

    public int curScenarioIndex = 1;


    // Start is called before the first frame update
    void Start()
    {
        //Set Scenarios
        scenarios = GameObject.Find("GameManager").GetComponent<GameManager>().GetScenarios();

        // Initialize the AudioSource
        audio_source = GetComponent<AudioSource>();

        //Set First Card 
        curScenarioIndex = GameManager.scenarioIndex;
        if(curScenarioIndex >= GameManager.endingScenarioIndex)
        {
            return;
        }
        // Start with the quiz panel active for 5 seconds
        //StartCoroutine(ActivatePanelWithAnimation());
        quizPanel.SetActive(true);
        StartCoroutine(DeactivatePanelAfterTime(5.0f));

        // Add a listener to the button to call TogglePanel method when clicked
        toggleButton.onClick.AddListener(TogglePanel);

        // Make Cards after 5seconds
        StartCoroutine(DelayedMakeCards());
    }

    IEnumerator DelayedMakeCards()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(4f);

        // Play the sound effect
        audio_source.PlayOneShot(card_sound);

        // Call MakeCards method
        MakeCards(scenarios.scenarios[curScenarioIndex]);
    }

    IEnumerator DeactivatePanelAfterTime(float time)
    {
        // Play the quiz sound effect
        audio_source.PlayOneShot(quizSound);

        // Wait for the specified time
        QuestionText.text = scenarios.scenarios[curScenarioIndex].advise;
        yield return new WaitForSeconds(time);

        // Deactivate the panel
        quizPanel.SetActive(false);
    }

    void TogglePanel()
    {
        // Toggle the active state of the quiz panel
        quizPanel.SetActive(!quizPanel.activeSelf);
    }

    void MakeCards(Scenario scenario)
    {
        //leftCard = new ApproveCard();
        //rightCard = new DisapproveCard();


        var rightCard = Instantiate(Resources.Load("Prefabs/Card", typeof(GameObject)) as GameObject, rightCardPlateTransform);
        var leftCard = Instantiate(Resources.Load("Prefabs/Card", typeof(GameObject)) as GameObject, leftCardPlateTransform);
        var ac = leftCard.AddComponent<ApproveCard>();
        var dc = rightCard.AddComponent<DisapproveCard>();

        ac.Build(scenario.answer, scenario.hint, scenario.reaction, scenario.effect, leftCardPlateTransform, scenario.king_id, scenario.event_id);
        dc.Build(scenario.answer, scenario.hint, scenario.reaction, scenario.effect, rightCardPlateTransform, scenario.king_id, scenario.event_id);
    }
}
