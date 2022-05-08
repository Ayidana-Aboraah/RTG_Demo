using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Stats")]
    public float hp, maxHp, speed = 1;
    public int durability;

    [Header("Shield")]
    public float shieldHp, maxShieldHp, storedDmg;
    public Timer shieldRecovery;
    public bool blocking;
    public int blockType;

    [Header("Multipliers")] public float hpMultiplier = 1f, xpMultiplier = 1f, defenseMultiplier = 1f, damageMultiplier = 1f;

    [HideInInspector] public bool stopped;

    internal virtual void Start()
    {
        hp = maxHp * hpMultiplier;
        shieldHp = maxShieldHp;
    }

    public virtual void TakeDamage(float damage)
    {
        //TODO: Use block type logic later

        var newDamage = damage - (durability * defenseMultiplier);

        if (damage < 0) newDamage = damage;

        if (blocking)
            switch (blockType)
            {
                case 2: // Love Train
                        // Create hitbox to attack all nearby
                    break;

                case 1: // Store damage
                    storedDmg += newDamage;
                    break;

                default:
                    if (!ShieldCheck(newDamage) && !shieldRecovery.isRunning) return;
                    break;
            }
        else
            hp -= newDamage;

        HealthCheck();
    }

    // if true -> shield is broken; else -> shield took the blow
    protected virtual bool ShieldCheck(float damage)
    {
        float resultDamage = shieldHp - damage;

        if (resultDamage <= 0)
        {
            ShieldBreak();
            return true;
        }

        shieldHp -= damage;
        return false;
    }

    public virtual void ShieldBreak()
    {
        shieldHp = 0;
        shieldRecovery.Start();
        Debug.Log("Oh No! My shield is broken.");
        //intialize Shield break state
    }

    public virtual void Die() => Destroy(gameObject);
    private void HealthCheck()
    {
        if (hp > maxHp) hp = maxHp;
        if (hp <= 0) Die();
    }
}
