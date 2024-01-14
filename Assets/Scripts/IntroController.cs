using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(LoadNextScene());   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
