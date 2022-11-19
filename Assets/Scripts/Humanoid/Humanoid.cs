using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Humanoid : MonoBehaviour, IDamageable
{
    [SerializeField] protected float MaxArmor = 100f;
    [SerializeField] protected float MaxHealth = 100f;
    protected float CurrentArmor;
    protected float CurrentHealth;

    public float Armor
    {
        get { return CurrentArmor; }
        set { CurrentArmor = Mathf.Clamp(value, 0f, MaxArmor); }
    }

    public float Health
    {
        get { return CurrentHealth; }
        set { CurrentHealth = Mathf.Clamp(value, 0f, MaxHealth); }
    }

    public virtual void TakeDamage(float damage)
    {
        Debug.Log("I cant take damage");
    }
}
