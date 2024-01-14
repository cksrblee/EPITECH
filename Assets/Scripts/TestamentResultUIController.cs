using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestamentResultUIController : MonoBehaviour
{

    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Awake()
    {
        //text = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string txt)
    {
        text.text = txt;
    }

    private void Start()
    {
        StartCoroutine(WaitAndLoadNextScenario());
    }

    public IEnumerator WaitAndLoadNextScenario()
    {
        Debug.Log("WAIT AND START NEXT SCENARIO");
        yield return new WaitForSeconds(GameManager.waitSecondsOfTestamentResultPanel);
        //GameObject.Find("GameManager").GetComponent<UIController>().FinishKingDeadUI();
        //ThisWorldEventController.
        ThisWorldEventController.OnRestartGame?.Invoke();

        Destroy(gameObject);
    }
}
