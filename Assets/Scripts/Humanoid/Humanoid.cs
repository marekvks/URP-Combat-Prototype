using UnityEngine;

public class Humanoid : MonoBehaviour, IDamageable
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected int _hitTriggerHash = Animator.StringToHash("Hit");
    [SerializeField] protected int _deathTriggerHash = Animator.StringToHash("Die");
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

    public virtual void TakeDamage(float minDamage, float maxDamage, float pushBackForce)
    {
        Debug.Log("I cant take damage");
    }

    public virtual float CalculateDamage(float minDamage, float maxDamage)
    {
        return 0;
    }
}
