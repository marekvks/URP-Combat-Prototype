using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UI;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationClip _lastComboAnimation;
    [SerializeField] private int _maxComboCount = 4;
    [SerializeField] private float _stunDelay = 0.5f;
    [SerializeField] private float _durationBeforeEnteringNextPhase = 0.5f;
    [SerializeField] private float _attackDuration = 1.2f;
    private int _comboCount = 0;
    private readonly int _firstSlashTrigger = Animator.StringToHash("First Slash");
    private readonly int _comboSlashTrigger = Animator.StringToHash("Combo Slash");
    private bool _isAttacking = false;
    private bool _canEnterCombo = false;
    private bool _isInNextComboStage = false;
    private bool _isInCombo = false;
    // private bool _endAttack = false;

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

    // Attatched to Unity's input system
    public void OnInputAttack(InputAction.CallbackContext context)
    {
        // Checking if button is pressed, not realeased
        if (!context.performed) return;
        // If player is attacking and is in stage when he can enter another combo attack
        if (_isAttacking && _canEnterCombo && !_isInNextComboStage)
        {
            _isInCombo = true;
            _canEnterCombo = false;
            _isInNextComboStage = true;
            StartCoroutine(AttackCoroutine(_comboSlashTrigger));
        }
        // If player wasnt already attacking, start attack
        else if (!_isAttacking)
        {
            StartCoroutine(AttackCoroutine(_firstSlashTrigger));
        }
    }

    IEnumerator AttackCoroutine(int hash)
    {
        _isAttacking = true;
        // Attack system
        Attack(hash);
        // AnimatorClipInfo[] clipInfo = _animator.GetCurrentAnimatorClipInfo(0);
        // If im on the last animation in combo
        float beforeEnteringComboDuration = _attackDuration * _durationBeforeEnteringNextPhase;
        float enteringComboDuration = _attackDuration - beforeEnteringComboDuration;
        if (_comboCount == _maxComboCount)
        {
            // wait till the animation finishes
            yield return new WaitForSeconds(_lastComboAnimation.length);
            // end the attack and coroutine
            EndAttack();
            yield break;
        }
        yield return new WaitForSeconds(beforeEnteringComboDuration);
        // this has to be changed after some period of time because if i would run it on the top, it would overwrite previous coroutine so the statemement if (_isInNextComboStage == false)
        // would play resulting running EndAttack() function which i dont want to run
        _isInNextComboStage = false;
        _canEnterCombo = true;
        yield return new WaitForSeconds(enteringComboDuration - 0.1f);
        // if i didnt slash in time when _canEnterCobmo was true, EndAttack() function will run
        if (!_isInNextComboStage) EndAttack();
    }

    /// <summary>
    /// Function used for playing slash animations and locking on closest enemy (not yet)
    /// </summary>
    private void Attack(int hash)
    {
        // Attack system
        // Play animation
        _animator.SetTrigger(hash);
        _comboCount++;
    }

    /// <summary>
    /// Function used to end an attacking phase
    /// </summary>
    private void EndAttack()
    {
        _comboCount = 0;
        _canEnterCombo = false;        
        _isInNextComboStage = false;
        _isInCombo = false;
        _isAttacking = false;
    }
}
