using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class main_change : MonoBehaviour
{
    // ���� ���� �̸��� ���⿡ �Է��ϼ���.
    public string nextSceneName;

    void Start()
    {
        nextSceneName = "Main";
        // ��ư Ŭ�� �� �̺�Ʈ�� �߰��մϴ�.
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(LoadNextScene);
        }
    }

    // ���� ������ �Ѿ�� �Լ�
    void LoadNextScene()
    {
        // ���� ������ �̵��մϴ�.
        SceneManager.LoadScene(nextSceneName);
    }
}
