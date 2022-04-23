using UnityEngine;

[RequireComponent(typeof(StandAttribute))]
public class Enemy : Stats
{
    public Transform target;
    public float xp;

    public override void Die()
    {
        if (target != null) target.GetComponent<Player>().AddXp(xp); // If this enemy stores an item, probably give it to the player
        Destroy(gameObject);
    }
}
