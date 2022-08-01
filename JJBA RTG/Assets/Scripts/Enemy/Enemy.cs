using UnityEngine;

[RequireComponent(typeof(StandAttribute))]
public class Enemy : Stats
{
    public byte boss; //First bit is for if boss; Rest are for which boss.
    public float xp;
    internal Animator ani; // TODO: Put this in base during a refactor
    public PlayerHUD boss_bar;

   internal override void Start()
    {
        base.Start();
        ani = GetComponent<Animator>();
        boss_bar.SetMaxHealth((int) maxHp);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        ani.SetTrigger("Hurt");
        boss_bar.SetHealth((int) hp);
    }

    public override void Die()
    {
        var target = GetComponent<EnemyAI>().target;
        if (target != null) target.GetComponent<Player>().AddXp(xp); // If this enemy stores an item, probably give it to the player
        PrototypeProgression.Completed(boss); //Mostly just for the Demo
        Destroy(gameObject);
    }
}
