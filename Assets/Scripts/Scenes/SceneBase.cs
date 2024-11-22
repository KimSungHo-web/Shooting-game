using UnityEngine;

public abstract class SceneBase : MonoBehaviour
{
    private void Start()
    {
        OnSceneLoad();
    }

    protected abstract void OnSceneLoad();

}
