using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : InteractableItem, IPassiveItemHandler
{
    public void ApplyPassiveEffect(GameObject target)
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

    public void RemovePassiveEffect(GameObject target)
    {
        throw new System.NotImplementedException();
    }
}
