using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMngr : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        print("THIS IS INDEX : " + currentSceneIndex);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
