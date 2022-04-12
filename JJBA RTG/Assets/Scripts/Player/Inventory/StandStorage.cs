using UnityEngine;

public sealed class StandStorage : MonoBehaviour
{
	public StandSlot[] stands = new StandSlot[6];
	public PlayerCombat combat;

	private void Awake()
	{
		combat = GetComponentInParent<PlayerCombat>();
		
		foreach( StandSlot slot in stands) slot.Initialize();	
	}

	public void SwitchStand(int idx)
	{
		combat.ClearStands();

		stands[idx].standbody.Spawn(combat.transform);
		combat.stand = combat.GetComponentInChildren<StandBody>();

		stands[idx].standbody.stand.SetCooldowns(combat);
	}

	// public void SwitchStandless()
	// {
	// 	combat.standOn = true;

	// 	//Set default standless combat variables
	// 	combat.atkTimer.maxTime = combat.standless.atkCooldown;
	// 	combat.spAtkTimer.maxTime = combat.standless.gunCooldown;
	// 	combat.strongTimer.maxTime = combat.standless.barrageCooldown;
	// 	combat.heavyTimer.maxTime = combat.standless.heavyCooldown;
		
	// 	combat.ATimers[0].maxTime = combat.standless.A1Cooldown;
	// 	combat.ATimers[1].maxTime = combat.standless.A2Cooldown;
	// 	combat.ATimers[2].maxTime = combat.standless.A3Cooldown;
	// }

	public void AddToStorage(StandBody newStand)
	{
		for (int i = 0; i <= stands.Length; i++)
		{
			if(!stands[i].full) continue;
			
			stands[i].addStand(newStand);
			break;
		}
	}

	public void RemoveStand(int idx){
		stands[idx].removeStand();
	}
	
	public void AddToExternalStorage(StandStorage external, int idx){
		external.AddToStorage(stands[idx].standbody);
	}
}
