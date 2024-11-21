using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class TitleScene : SceneBase
{
    private void Init()
    {
        AudioManager.Instance.Init();
    }
    protected override void OnSceneLoad()
    {
        Init();
    }

}
