using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerCombat _playerCombat;

    [SerializeField] private PlayerController _playerController;
    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _playerInputActions.PlayerCombat.ComboSlash.performed += _playerCombat.PerformAttack;
        _playerInputActions.PlayerCombat.StealthKill.performed += _playerCombat.PerformStealthAttack;
        _playerInputActions.PlayerCombat.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.PlayerCombat.Disable();
    }
}
