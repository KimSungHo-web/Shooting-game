using System;
using UnityEngine;

public class ThirdPersonRecoil : MonoBehaviour
{
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
    
    
}

