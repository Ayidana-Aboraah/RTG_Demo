using UnityEngine;

public sealed class Player : Stats
{
	[Header("Progression System")] 
	public float xp, maxXp;
	public int level, skillPoints;
	// [Header("Death")] public GameObject deathMenu; //reimplement for online if nesseccary
	public PlayerHUD HUD;
    internal Animator ani; // TODO: Put this in base during a refactor

	internal override void Start() {
		base.Start();
        ani = GetComponentInChildren<Animator>();
		HUD.SetMaxHealth((int)maxHp);
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
	}

	public override void TakeDamage(float damage){
		base.TakeDamage(damage);
        ani.SetTrigger("Hurt"); // TODO: Place this in side the base TakeDamage at the appropriate place\
		HUD.SetHealth((int) hp);
	}

	public override void Die() => FindObjectOfType<DeathMenu>().Evaluate();

	public void Respawn()
	{
		hp = maxHp;
		shieldHp = maxShieldHp;
		shieldRecovery.ResetTimer();
		HUD.SetMaxHealth((int) maxHp);
	}
}
