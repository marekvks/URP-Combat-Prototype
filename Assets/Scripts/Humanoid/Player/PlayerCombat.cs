using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UI;
using UnityEngine.VFX;

public class PlayerCombat : MonoBehaviour
{
    [Header("Animation Options")]
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationClip _lastComboAnimation;
    private readonly int _firstSlashTrigger = Animator.StringToHash("First Slash");
    private readonly int _comboSlashTrigger = Animator.StringToHash("Combo Slash");
    [Header("Attack/Combo Options")]
    [SerializeField] private int _maxComboCount = 4;
    [SerializeField] private float _stunDelay = 0.5f;
    [SerializeField] private float _durationBeforeEnteringNextPhase = 0.5f;
    [SerializeField] private float _attackDuration = 1.2f;
    private int _comboCount = 0;
    private bool _isAttacking = false;
    private bool _canEnterCombo = false;
    private bool _isInNextComboStage = false;
    private bool _isInCombo = false;

    [SerializeField] private Transform _spherecast;
    [SerializeField] private float _spherecastRadius = 20f;
    [SerializeField] private LayerMask _enemyLayer;

    [SerializeField] private float _damage = 400f;
    [SerializeField] private float _distance;

    [Header("Gizmos")]
    [SerializeField] private Color _color;
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
        // Play animation
        _animator.SetTrigger(hash);
        _comboCount++;
        // Attack system
        GameObject target = GetNearestTarget();
        if (target == null) return;
        Debug.Log(GetNearestTarget().name);
        IDamageable damageableTarget = target.GetComponent<IDamageable>();
        if (damageableTarget != null)
        {
            SetPositionAndRotations(target.transform);
            damageableTarget.TakeDamage(_damage);
        }
        // VFX
        // _swordSlash.Slash(_comboCount);
    }

    /// <summary>
    /// Makes player look at enemy, enemy look at player and sets player position near enemy
    /// </summary>
    /// <param name="target">enemy position</param>
    private void SetPositionAndRotations(Transform target)
    {
        transform.LookAt(target.position);
        target.LookAt(transform.position);
        Vector3 directionToPlayer = (transform.position - target.position).normalized;
        Debug.Log("Hi");
        transform.position = transform.position + directionToPlayer * _distance;
    }

    /// <summary>
    /// Gets all enemies within radius and calculates the closest one
    /// </summary>
    /// <returns>Closest enemy</returns>
    private GameObject GetNearestTarget()
    {
        Collider[] enemies = Physics.OverlapSphere(_spherecast.position, _spherecastRadius, _enemyLayer);
        float nearestDistance = _spherecastRadius;
        GameObject nearestObject = null;
        foreach (Collider enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestObject = enemy.gameObject;
            }
        }
        return nearestObject;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawSphere(_spherecast.position, _spherecastRadius);
    }
}