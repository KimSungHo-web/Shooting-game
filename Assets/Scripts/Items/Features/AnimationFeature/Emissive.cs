using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emissive : MonoBehaviour
{
    [SerializeField] private Material emissiveMaterial;
    [SerializeField] private Renderer objectToChange;
    
    void Start()
    {
        emissiveMaterial = objectToChange.GetComponent<Renderer>().material;
    }

    public void TurnEmissionOff()
    {
        emissiveMaterial.DisableKeyword("_EMISSION");
    }

    public void TurnEmissionOn()
    {
        emissiveMaterial.EnableKeyword("_EMISSION");
    }
    
   
}
