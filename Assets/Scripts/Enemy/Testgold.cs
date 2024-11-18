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
        Debug.Log($"현재 골드: {currentGold}");
    }

    public void AddEXP(int amount)
    {
        currentEXP += amount;
        Debug.Log($"현재 경험치: {currentEXP}");
    }
}
