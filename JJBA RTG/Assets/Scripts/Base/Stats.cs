using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Stats")]
    public float hp, maxHp;
    public int durability;

    [Header("Shield")]
    public float shieldHp, maxShieldHp, storedDmg;
    public Timer shieldRecovery;
    public bool blocking;
    public int blockType;

    [Header("Multipliers")]
    public float hpMultiplier = 1f, xpMultiplier = 1f, defenseMultiplier = 1f, damageMultiplier = 1f;

    [HideInInspector] public bool stopped;

    internal virtual void Start()
    {
        hp = maxHp * hpMultiplier;
        shieldHp = maxShieldHp;
    }

    public virtual void TakeDamage(float damage)
    {
        var newDamage = damage - (durability * defenseMultiplier);

        if (damage < 0) newDamage = damage; //Turns into heal

        if (blocking && shieldHp > 0)
            switch (blockType)
            {
                case 0:
                    if (shieldHp - newDamage <= 0) // if true -> shield is broken; else -> shield took the blow
                        ShieldBreak();
                    else
                        shieldHp -= newDamage;
                    break;

                case 1: // Store damage
                    storedDmg += newDamage;
                    break;

                case 2: // Love Train
                    Hitbox newHit = new Hitbox(); // Create hitbox to attack all nearby
                    newHit.range = 5f;
                    newHit.damage = damage;
                    newHit.Atk();
                    break;
            }
        else
            hp -= newDamage;

        HealthCheck();
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
        if (hp <= 0f) Die();
    }
}
