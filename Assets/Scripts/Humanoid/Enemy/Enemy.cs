using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : Humanoid
{
    [SerializeField] private Rigidbody _rigidbody;

    private void Start()
    {
        Armor = MaxArmor;
        Health = MaxHealth;
    }

    public override void TakeDamage(float minDamage, float maxDamage, float pushBackForce)
    {
        float damage = CalculateDamage(minDamage, maxDamage);

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
        _animator.SetTrigger("Hit");
        PushBack(pushBackForce);

        if (IsDead()) Die();
    }

    public override float CalculateDamage(float minDamage, float maxDamage)
    {
        float damage = Random.Range(minDamage, maxDamage);
        return damage;
    }

    private void PushBack(float force)
    {
        _rigidbody.velocity = -transform.forward * force;
    }

    private bool IsDead()
    {
        bool isDead = false;
        if (Health <= 0) isDead = true;
        return isDead;
    }

    private void Die() => Debug.Log("dead"); //_animator.SetTrigger(_deathTriggerHash);
}
