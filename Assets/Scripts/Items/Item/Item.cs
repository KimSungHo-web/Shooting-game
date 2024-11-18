using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite itemIcon;
    public int width = 1;
    public int height = 1;

    public Item(string name, Sprite icon, int w, int h)
    {
        itemName = name;
        itemIcon = icon;
        width = w;
        height = h;
    }
}
