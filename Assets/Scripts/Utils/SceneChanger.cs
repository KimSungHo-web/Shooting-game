using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Header("Scene Settings")]
    public string targetSceneName; // 이동할 씬 이름

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 포탈에 들어왔을 때 씬 이동
        if (other.CompareTag("Player"))
        {
            ChangeScene();
        }
    }
}
