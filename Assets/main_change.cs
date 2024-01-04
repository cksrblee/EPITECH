using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class main_change : MonoBehaviour
{ 

    void Start()
    {
        // 버튼 클릭 시 이벤트를 추가합니다.
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(LoadNextScene);
        }
    }

    // 다음 씬으로 넘어가는 함수
    void LoadNextScene()
    {
        // 다음 씬으로 이동합니다.
        SceneManager.LoadScene(1);
    }
}
