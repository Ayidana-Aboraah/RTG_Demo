using UnityEngine;

public sealed class StandAttribute : MonoBehaviour
{
	public float bleedDamage;
	float m_Drag;
	float m_BaseDefenseMultipler, m_BaseHpMultiplier, m_BaseDamgeMulitiplier;

	[Header("Debuffs")]
	public Timer bleedTimer;
	public Timer stunTimer;
	public Timer floatTimer;
	
	[Header("Buffs")]
	public Timer defenseTimer;
	public Timer hpTimer;
	public Timer dmgTimer;

	private void Update()
	{
		UpdateTimers();
		
		if (stunTimer.complete) // TimeStop is a type of stun (I guess)
		{
			GetComponent<Stats>().stopped = false;
			GetComponent<Rigidbody>().useGravity = true;
			GetComponentInChildren<Animator>().SetBool("Stun", false);
			
			stunTimer.complete = false;
		}

		if (floatTimer.complete)
		{
			GetComponent<Rigidbody>().drag = m_Drag;
			floatTimer.complete = false;
		}

		if (defenseTimer.complete)
		{
			GetComponent<Stats>().defenseMultiplier = m_BaseDefenseMultipler;
			defenseTimer.complete = false;
		}

		if (hpTimer.complete)
		{
			GetComponent<Stats>().hpMultiplier = m_BaseHpMultiplier;
			hpTimer.complete = false;
		}

		if (dmgTimer.complete)
		{
			GetComponent<Stats>().damageMultiplier = m_BaseDamgeMulitiplier;
			dmgTimer.complete = false;
		}

		if (bleedTimer.isRunning)
		{
			if (bleedTimer.m_CurrentTime % 2.0f == 0) GetComponent<Stats>().hp -= bleedDamage;
		}
	}
	public void StartDebuff(int effectType, float duration)
	{
		stunTimer.maxTime = duration;
		
		switch (effectType)
		{
			//Tripped
			case 3:
				GetComponentInChildren<Animator>().SetBool("Trip", true);
				stunTimer.Start();
				break;
			//Stunning //Also no need to be stopped here since the walking animation will block it
			case 2:
				GetComponent<Movement>().ani.SetBool("Stun", true);
				stunTimer.Start();
				break;
			//Case 1: is in the overload under the method
			//TimeStop
			case 0:
				GetComponent<Stats>().stopped = true;
				GetComponent<Rigidbody>().useGravity = false;
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				stunTimer.Start();
				break;
		}
	}

	public void StartDebuff(int effectType, float duration, float damage)
	{
		switch (effectType)
		{
			case 1: //bleeding/Poison
				bleedTimer.maxTime = duration;
				bleedDamage = damage;
				bleedTimer.Start();
				break;
		}
	}

	/// <summary>
	/// 0 is Floating
	/// 1 is Defense buff
	/// 2 is Hp buff
	/// 3 is Damage buff
	/// </summary>
	public void StartBuff(int effectType, float duration, float amount)
	{
		switch (effectType)
		{
			//Damage Buff
			case 3:
				dmgTimer.maxTime = duration;
				m_BaseDamgeMulitiplier = GetComponent<Stats>().damageMultiplier;
				GetComponent<Stats>().damageMultiplier += amount;
				dmgTimer.Start();
				break;
			//Hp buff
			case 2:
				hpTimer.maxTime = duration;
				m_BaseHpMultiplier = GetComponent<Stats>().hpMultiplier;
				GetComponent<Stats>().hpMultiplier += amount;
				hpTimer.Start();
				break;
			//Defense Buff
			case 1:
				defenseTimer.maxTime = duration;
				m_BaseDefenseMultipler = GetComponent<Stats>().defenseMultiplier;
				GetComponent<Stats>().defenseMultiplier += amount;
				defenseTimer.Start();
				break;
			//Floating
			case 0:
				floatTimer.maxTime = duration;
				m_Drag = GetComponent<Rigidbody>().drag;
				GetComponent<Rigidbody>().drag = 10f;
				floatTimer.Start();
				break;
		}
	}

	public void UpdateTimers()
	{
		bleedTimer.UpdateTimer();
		stunTimer.UpdateTimer();
		
		floatTimer.UpdateTimer();
		defenseTimer.UpdateTimer();
		hpTimer.UpdateTimer();
		dmgTimer.UpdateTimer();
	}
}