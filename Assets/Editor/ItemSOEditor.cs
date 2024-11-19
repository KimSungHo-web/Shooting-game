using UnityEngine;
using UnityEditor;
using Defines;
using System.Collections.Generic;

[CustomEditor(typeof(ItemSO))]
public class ItemSOEditor : Editor
{
    private SerializedObject _serializedObject;
    private SerializedProperty _consumablesProperty;

    private void OnEnable()
    {
        _serializedObject = new SerializedObject(target);
        _consumablesProperty = _serializedObject.FindProperty("consumables");
    }

    public override void OnInspectorGUI()
    {
        _serializedObject.Update();

        ItemSO itemSO = (ItemSO)target;

        EditorGUILayout.LabelField("Item Settings", EditorStyles.boldLabel);

        itemSO.itemIcon = (Sprite)EditorGUILayout.ObjectField("Item Icon", itemSO.itemIcon, typeof(Sprite), false);
        itemSO.itemType = (ItemType)EditorGUILayout.EnumPopup("Item Type", itemSO.itemType);

        switch (itemSO.itemType)
        {
            case ItemType.Equipable:
                DrawEquipmentSection(itemSO);
                break;

            case ItemType.Consumable:
                DrawConsumableSection(itemSO);
                break;

            case ItemType.Resource:
                DrawResourceSection(itemSO);
                break;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(itemSO);
        }

        _serializedObject.ApplyModifiedProperties();
    }

    private void DrawEquipmentSection(ItemSO itemSO)
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("장비타입", EditorStyles.boldLabel);

        itemSO.SlotSize = EditorGUILayout.IntField("Slot Size", itemSO.SlotSize);
        EditorGUILayout.Space();
        itemSO.equipType = (EquipType)EditorGUILayout.EnumPopup("Equip Type", itemSO.equipType);
        itemSO.equipPrefab = (GameObject)EditorGUILayout.ObjectField("Equip Prefab", itemSO.equipPrefab, typeof(GameObject), false);
        itemSO.dropItemPrefab = (GameObject)EditorGUILayout.ObjectField("Drop Item Prefab", itemSO.dropItemPrefab, typeof(GameObject), false);
        itemSO.rarity = (Rarity)EditorGUILayout.EnumPopup("Rarity", itemSO.rarity);
        itemSO.LeftHand = EditorGUILayout.Toggle("Left Hand", itemSO.LeftHand);
        

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("무기정보 (speed는 -면 속도 빨라짐)", EditorStyles.boldLabel);
        itemSO.projectileDamage = EditorGUILayout.FloatField("Projectile Damage", itemSO.projectileDamage);
        itemSO.projectileSpeed = EditorGUILayout.FloatField("Projectile Speed", itemSO.projectileSpeed);
        itemSO.projectileScale = EditorGUILayout.FloatField("Projectile Scale", itemSO.projectileScale);

        EditorGUILayout.Space();
        itemSO.childPath = EditorGUILayout.TextField("Child Path", itemSO.childPath);
    }

    private void DrawConsumableSection(ItemSO itemSO)
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("소비타입", EditorStyles.boldLabel);

        itemSO.SlotSize = EditorGUILayout.IntField("Slot Size", itemSO.SlotSize);
        EditorGUILayout.Space();
        itemSO.isStackable = EditorGUILayout.Toggle("Is Stackable", itemSO.isStackable);
        itemSO.stackSize = EditorGUILayout.IntField("Stack Size", itemSO.stackSize);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_consumablesProperty, true);

        EditorGUILayout.Space();
        itemSO.childPath = EditorGUILayout.TextField("Child Path", itemSO.childPath);
    }

    private void DrawResourceSection(ItemSO itemSO)
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("리소스타입", EditorStyles.boldLabel);

        itemSO.resourceType = (ResourceType)EditorGUILayout.EnumPopup("Resource Type", itemSO.resourceType);
        itemSO.resourceValue = EditorGUILayout.IntField("Resource Value", itemSO.resourceValue);
        itemSO.ResourcePrefab = (GameObject)EditorGUILayout.ObjectField("Resource Prefab", itemSO.ResourcePrefab, typeof(GameObject), false);

        EditorGUILayout.Space();
        itemSO.childPath = EditorGUILayout.TextField("Child Path", itemSO.childPath);
    }
}
