using UnityEngine;

public class Player : Stats
{
	[Header("Progression System")] public float xp;
	public float maxXp;
	public int level;
	public int skillPoints;

	//Uncomment that if when implementing online;
	//If it is the best option
	// [Header("Death")] public GameObject deathMenu;

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