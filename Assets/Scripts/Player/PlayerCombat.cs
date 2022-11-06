using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float _length = 1f;
    [SerializeField] private Animator _animator;
    private readonly int _firstSlashTrigger = Animator.StringToHash("First Slash");
    private readonly int _comboSlashTrigger = Animator.StringToHash("Slash");
    private bool _isAttacking = false;
    private bool _canEnterCombo = false;
    private bool _isInCombo = false;
    private bool _endAttack = false;

    [HideInInspector]
    public bool IsAttacking
    {
        get { return _isAttacking; }
    }

    [HideInInspector]
    public bool IsInCombo
    {
        get { return _isInCombo; }
    }

    [HideInInspector]
    public bool CanEnterCombo
    {
        get { return _canEnterCombo; }
    }

    //

    public void OnInputAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (_isAttacking && _canEnterCombo)
        {
            _isInCombo = true;
            _endAttack = false;
            StartCoroutine(AttackCoroutine(_comboSlashTrigger));
        }
        else if (!_isAttacking)
            StartCoroutine(AttackCoroutine(_firstSlashTrigger));
        else if (_endAttack)
            EndAttack();
    }

    IEnumerator AttackCoroutine(int hash)
    {
        _isAttacking = true;
        // Attack
        Attack(hash);
        yield return new WaitForSeconds(_length * 0.5f);
        _canEnterCombo = true;
        yield return new WaitForSeconds(_length * 0.5f);
        if (!_isInCombo || _endAttack)
            EndAttack();
        _canEnterCombo = false;
        _endAttack = true;
    }

/*
    IEnumerator ChainAttackCoroutine()
    {
        yield return null;
    }
*/

    private void Attack(int hash)
    {
        // Attack logic
        // Play animation
        _animator.SetTrigger(hash);
    }

    private void EndAttack()
    {
        _isInCombo = false;
        _isAttacking = false;
    }
}
