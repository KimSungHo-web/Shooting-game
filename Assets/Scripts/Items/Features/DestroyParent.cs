using Defines;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    private ItemSO itemData;

    protected virtual void Awake()
    {
        itemData = ItemManager.Instance.GetResourceItemSO(gameObject.name);
        //�����۸Ŵ������� SO���� ItemType �����͸� �޾ƿ��� �ż���

    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandleGoldAndExp();
            Destroy(transform.parent.gameObject);
        }
    }

    private void HandleGoldAndExp()
    {
        switch (gameObject.tag)
        {
            case "Gold":
                GameManager.Instance.AddGold(itemData.resourceValue);
                break;
            case "Exp":
                GameManager.Instance.AddEXP(itemData.resourceValue);
                break;
            case "Item":
                HandleItem();
                break;
            default:
                Debug.LogWarning($"������ �±� ����");
                break;
        }
    }

    private static void HandleItem()
    {
        //if(itemData == null)
        //switch (itemData.itemType)
        //{
        //    case ItemType.Consumable:
        //        //�÷��̾� ü��ȸ�� �ż��� ȣ��
        //        break;
        //    case ItemType.Equipable:
        //        //�÷��̾� ���� ������ ��ü
        //        //���� so�����Ϳ��� �޾Ƽ� ��ü
        //        break;
        //    case ItemType.Passive:
        //        //�κ��丮��
        //        break;
        //    default:
        //        Debug.Log($"������ ������ Ÿ�� ����");
        //        break;

        //}

    }


    //�ӽ�
    private int GetGoldValue()
    {
        return 10;
    }

    private int GetEXPValue()
    {
        return 5;
    }
    //
}
