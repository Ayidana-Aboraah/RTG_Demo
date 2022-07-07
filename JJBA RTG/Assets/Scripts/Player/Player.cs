using UnityEngine;

public sealed class Player : Stats
{
	[Header("Progression System")] 
	public float xp, maxXp;
	public int level, skillPoints;
	// [Header("Death")] public GameObject deathMenu; //reimplement for online if nesseccary
	public Healthbar healthbar, xp_bar, shieldbar; // TODO: implement the shieldbar

	internal override void Start() {
		base.Start();
		healthbar.SetMaxHealth((int) maxHp);
	}

	public void AddXp(float newXp)
	{
		xp += xpMultiplier * newXp;

		if (xp < maxXp || level > 99) return;

		skillPoints++;
		level++;
		maxXp += 10; //Increase Number is arbitrary // We may cap this, or just not increase to begin with
		float newExperience = xp - newXp;
		if (newExperience < 0) xp -= newExperience;

		xp_bar.slider.value = xp;
		xp_bar.slider.maxValue = maxXp;
	}

	public override void TakeDamage(float damage){
		base.TakeDamage(damage);
		healthbar.SetHealth((int) hp);
	}

	public override void Die() => FindObjectOfType<DeathMenu>().Evaluate();

	public void Respawn()
	{
		hp = maxHp;
		shieldHp = maxShieldHp;
		shieldRecovery.ResetTimer();
	}
}
