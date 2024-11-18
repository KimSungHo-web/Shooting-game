using UnityEngine;

public class InventoryItem
{
    public string itemName;
    public int width;
    public int height;
    public Sprite itemSprite;  // �������� �̹����� ������ ����

    public InventoryItem(string name, int width, int height, Sprite sprite)
    {
        itemName = name;
        this.width = width;
        this.height = height;
        itemSprite = sprite;
    }
}
