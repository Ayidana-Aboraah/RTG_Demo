using UnityEngine;

[RequireComponent(typeof(StandAttribute))]
public class Enemy : Stats
{
    public byte boss; //First bit is for if boss; Rest are for which boss.
    public float xp;

    public override void Die()
    {
        var target = GetComponent<EnemyCombat>().target;
        if (target != null) target.GetComponent<Player>().AddXp(xp); // If this enemy stores an item, probably give it to the player
        PrototypeProgression.Completed(boss); //Mostly just for the Demo
        Destroy(gameObject);
    }
}
