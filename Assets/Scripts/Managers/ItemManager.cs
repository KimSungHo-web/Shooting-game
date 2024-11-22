using Defines;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    //������ �ε�
    //��δ� Assets/Resources/Prefabs/Example.fbx�̿�����
    //GameObject Prefab = ItemManager.Instance.Instantiate("�̸�");

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
            Debug.Log($"������ �ε� ���� : {path}");
            return null;
        }

        return Instantiate(prefab, parent);
    }
    //�ڵ� �����ʿ�
    public ItemSO GetResourceItemSO(string itemName)
    {
        return Load<ItemSO>($"ItemSOData/Resource/{itemName}");
    }
    public ItemSO GetConsumeItemSO(string itemName)
    {
        return Load<ItemSO>($"ItemSOData/Consume/{itemName}");
    }
    //
    //

    public ItemSO GetItemSO(ItemType itemType,Rarity rarity, string itemName)
    {
        string folderPath = GetFolderPathItemType(itemType,rarity);
        return Load<ItemSO>($"{folderPath}/{itemName}");
    }

    // �Һ������ ItemSO HealthPotion = ItemManager.Instance.GetItemSO(ItemType.Consumable, null, "ItemName");
    // �������� ItemSO EpicWeapon = ItemManager.Instance.GetItemSO(ItemType.Equipable, Rarity.Epic, "ItemName");
    private string GetFolderPathItemType(ItemType itemType, Rarity? rarity)
    {
        switch (itemType)
        {
            case ItemType.Consumable:
                return "ItemSOData/Consume";
            case ItemType.Equipable:
                return $"ItemSOData/Weapon/{GetRarityFolder(rarity.Value)}";
            case ItemType.Passive:
                return "ItemSOData/Passive";
            default:
                Debug.LogWarning($"�������� ���� ������ Ÿ��: {itemType}");
                return "ItemSOData";
        }

    }
    private string GetRarityFolder(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return "Common";
            case Rarity.Uncommon:
                return "Uncommon";
            case Rarity.Rare:
                return "Rare";
            case Rarity.Epic:
                return "Epic";
            case Rarity.Legendary:
                return "Legendery";
            default:
                Debug.LogWarning($"�������� ���� Defines.Rarity: {rarity}");
                return "Common"; 
        }
    }
}
