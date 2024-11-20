using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Header("Scene Settings")]
    public string targetSceneName; // �̵��� �� �̸�

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ ��Ż�� ������ �� �� �̵�
        if (other.CompareTag("Player"))
        {
            ChangeScene();
        }
    }
}
