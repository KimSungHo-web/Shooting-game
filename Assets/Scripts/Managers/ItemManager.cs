using Defines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    // ��������Ʈ �ε�
    // Sprite icon = ItemManager.Instance.Load<Sprite>("Icons/AK74");

    // ������ ������ �ε�
    //ItemSO itemData = ItemManager.Instance.Load<ItemSO>("SO_Items/AK74");

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

        return Resources.Load<T>(path);
    }
    public T GetSOData<T>(string path, SODataType dataType = SODataType.None, SOItemDataType itemType = SOItemDataType.None) where T : ScriptableObject
    {
        if (dataType == SODataType.Item && itemType != SOItemDataType.None)
        {
            return Load<T>($"SO_Datas/{dataType}/{itemType}/{path}");
        }
        else if (dataType != SODataType.None)
        {
            return Load<T>($"SO_Datas/{dataType}/{path}");
        }

        return Load<T>($"SO_Datas/{path}");
    }

    public T GetSOItemData<T>(string name, SOItemDataType itemType) where T : ScriptableObject
    {
        return GetSOData<T>(name, SODataType.Item, itemType);
    }




}
