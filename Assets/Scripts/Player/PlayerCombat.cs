using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int _maxComboCount = 4;
    private int _comboCount = 0;
    private readonly int _firstSlashTrigger = Animator.StringToHash("First Slash");
    private readonly int _comboSlashTrigger = Animator.StringToHash("Combo Slash");
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

    private void Update()
    {
        Debug.Log("End Attack: " + _endAttack);
    }


    public void OnInputAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (_isAttacking && _canEnterCombo)
        {
            _endAttack = false;
            _isInCombo = true;
            StartCoroutine(AttackCoroutine(_comboSlashTrigger));
        }
        else if (!_isAttacking)
        {
            // _endAttack = false;
            StartCoroutine(AttackCoroutine(_firstSlashTrigger));
        }

        else if (_endAttack)
            EndAttack();
    }

    IEnumerator AttackCoroutine(int hash)
    {
        _isAttacking = true;
        Attack(hash);
        float length = _animator.GetCurrentAnimatorClipInfo(0).Length;
        if (_comboCount == _maxComboCount)
        {
            yield return new WaitForSeconds(length * 0.5f);
            _comboCount = 0;
            yield break;
        }
        // Attack
        yield return new WaitForSeconds(length * 0.5f);
        _canEnterCombo = true;
        yield return new WaitForSeconds(length * 0.5f);
        if (!_isInCombo || _endAttack) EndAttack();
        _canEnterCombo = false;
        _endAttack = true;
    }

    private void Attack(int hash)
    {
        // Attack logic
        // Play animation
        _animator.SetTrigger(hash);
        _comboCount += 1;
    }

    private void EndAttack()
    {
        _isInCombo = false;
        _isAttacking = false;
    }
}
