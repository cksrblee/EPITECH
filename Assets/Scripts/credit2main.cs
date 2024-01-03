using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class credit2main : MonoBehaviour
{
    // This is for the next Scene name in UI
    public string nextSceneName;

    void Start()
    {
        nextSceneName = "Intro";
        // Get component and if button presses load next scene
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(LoadNextScene);
        }
    }

    // This function is for loading next scene
    void LoadNextScene()
    {
        // Load the Next Scene
        SceneManager.LoadScene(nextSceneName);
    }
}
