using UnityEngine;

public sealed class Player : Stats
{
	[Header("Progression System")] 
	public float xp, maxXp;
	public int level, skillPoints;
	// [Header("Death")] public GameObject deathMenu; //reimplement for online if nesseccary

	public void AddXp(float newXp)
	{
		xp += xpMultiplier * newXp;

		if (xp < maxXp || level > 99) return;

		skillPoints++;
		level++;
		//add abitrary increase to Xp
		maxXp += 10;

		float newExperience = xp - newXp;
		if (newExperience < 0) xp -= newExperience;
	}

	public override void Die()
	{
		FindObjectOfType<DeathMenu>().Evaluate();
	}

	public void Respawn()
	{
		hp = maxHp;
		shieldHp = maxShieldHp;
		shieldRecovery.ResetTimer();
	}
}