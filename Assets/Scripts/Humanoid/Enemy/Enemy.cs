using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : Humanoid
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int deathTriggerHash = Animator.StringToHash("Die");

    private void Start()
    {
        Armor = MaxArmor;
        Health = MaxHealth;
    }

    public override void TakeDamage(float damage)
    {
        if (damage <= Armor)
        {
            Armor -= damage;
        }
        else
        {
            float damageToHealth = Armor - damage;
            Armor = 0;
            Health += damageToHealth;
        }
    
        Debug.Log("Ouch");

        if (IsDead()) Die();
    }

    private bool IsDead()
    {
        bool isDead = false;
        if (Health <= 0) isDead = true;
        return isDead;
    }

    private void Die() => _animator.SetTrigger(deathTriggerHash);
}
