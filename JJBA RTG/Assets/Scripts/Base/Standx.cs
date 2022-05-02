using UnityEngine;

public abstract class Standx : MonoBehaviour
{
	[Header("Cooldown")]
	public float spAtkCooldown;
	public float ultCooldown;
	public float[] ACooldowns = new float[3];

	internal Transform parent;
	internal Stats stats;
	
	#region Stand On
	public abstract void Atk();
	public abstract void SpAtk();
	public abstract void Strong();
	public abstract void Heavy();
	public abstract void A1();
	public abstract void A2();
	public abstract void A3();
	public abstract void Ult();
	#endregion

	public virtual void ApplyAttributes(){}
	
	#region Maintenence
	
	///<summary>
	///Sets the cooldowns for the player combat to the cooldowns for the stand
	///</summary>
	public virtual void SetCooldowns(PlayerCombat combat)
	{
		//Set each cooldown for the combat timers/cooldowns
		// combat.atkTimer.maxTime = atkCooldown;
		combat.spAtkTimer.maxTime = spAtkCooldown;
		// combat.strongTimer.maxTime = strongCooldown;
		// combat.heavyTimer.maxTime = heavyCooldown;
		combat.ultTimer.maxTime = ultCooldown;

		combat.ATimers[0].maxTime = ACooldowns[0];
		combat.ATimers[1].maxTime = ACooldowns[1];
		combat.ATimers[2].maxTime = ACooldowns[2];

		//Maybe reset timers
		// combat.spAtkTimer.ResetTimer(); //Removed for balancing
		// combat.ultTimer.ResetTimer();
		
		combat.ATimers[0].ResetTimer();
		combat.ATimers[1].ResetTimer();
		combat.ATimers[2].ResetTimer();
	}
	
	public virtual void initialize()
	{
		stats = GetComponentInParent<Stats>();
		parent = transform.parent;
	}

	public abstract void DrawBoxes();
	#endregion
	
	void Awake()
	{
		initialize();	
	}
	
	void OnDrawGizmosSelected()
	{
		DrawBoxes();
	}
}
