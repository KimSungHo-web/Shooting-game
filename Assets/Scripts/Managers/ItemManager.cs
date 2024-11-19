using Defines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    //경로는 Assets/Resources/Prefabs/Example.fbx
    //프리팹 로드
    //GameObject Prefab = ItemManager.Instance.Instantiate("이름");

    private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();
    private Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>();
    private Dictionary<string, ScriptableObject> soDict = new Dictionary<string, ScriptableObject>();

    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(Sprite))
        {
            if (spriteDict.TryGetValue(path, out Sprite sprite))
                return sprite as T;

            Sprite sp = Resources.Load<Sprite>(path);
            spriteDict.Add(path, sp);
            return sp as T;
        }
        else if (typeof(T) == typeof(ScriptableObject))
        {
            if (soDict.TryGetValue(path, out ScriptableObject so))
                return so as T;

            ScriptableObject soData = Resources.Load<ScriptableObject>(path);
            soDict.Add(path, soData);
            return soData as T;
        }
        else if (typeof(T) == typeof(GameObject))
        {
            if (prefabDict.TryGetValue(path, out GameObject prefab))
                return prefab as T;

            GameObject loadedPrefab = Resources.Load<GameObject>(path);
            if (loadedPrefab != null)
            {
                prefabDict.Add(path, loadedPrefab);
            }
            return loadedPrefab as T;
        }

        return Resources.Load<T>(path);
    }
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        return Instantiate(prefab, parent);
    }


}
