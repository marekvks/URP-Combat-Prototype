using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerCombat playerCombat;

    /*#region Singleton

    // Reason why im using Singleton pattern is:
    // I only need one instance of this class

    private PlayerInputSystem()
    {
    }

    private static PlayerInputSystem instance;

    public static PlayerInputSystem GetInstance()
    {
        if (instance == null) instance = new PlayerInputSystem();
        return instance;
    }

    #endregion*/
}
