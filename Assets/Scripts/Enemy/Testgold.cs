using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testgold : MonoBehaviour
{
    [SerializeField] private int currentGold;
    [SerializeField] private int currentEXP;

    public void AddGold(int amount)
    {
        currentGold += amount;
        Debug.Log($"���� ���: {currentGold}");
    }

    public void AddEXP(int amount)
    {
        currentEXP += amount;
        Debug.Log($"���� ����ġ: {currentEXP}");
    }
}
