using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : InteractableItem, IEquipable, IInteractable
{
    public void Equip(GameObject character)
    {
        throw new System.NotImplementedException();
    }
    public void Unequip(GameObject character)
    {
        throw new System.NotImplementedException();
    }

    public void Flash()
    {
        throw new System.NotImplementedException();
    }

    public string GetInteractPrompt()
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract(Transform target)
    {
        throw new System.NotImplementedException();
    }


}
