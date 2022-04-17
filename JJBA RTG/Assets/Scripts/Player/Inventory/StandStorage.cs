using UnityEngine;

public sealed class StandStorage : MonoBehaviour
{
	public StandSlot[] slots = new StandSlot[6];
	public PlayerCombat combat;

	private void Awake()
	{
		combat = GetComponentInParent<PlayerCombat>();

		foreach( StandSlot slot in slots) slot.Initialize();	
	}

	public void SwitchStand(int idx)
	{
		combat.ClearStands();

		slots[idx].standbody.Spawn(combat.transform);
		combat.stand = combat.GetComponentInChildren<StandBody>();

		slots[idx].standbody.stand.SetCooldowns(combat);
	}

	public void AddToStorage(StandBody newStand)
	{
		for (int i = 0; i <= slots.Length; i++)
		{
			if(!slots[i].full) continue;
			
			slots[i].addStand(newStand);
			break;
		}
	}

	public void RemoveStand(int idx){
		slots[idx].removeStand();
	}
	
	public void AddToExternalStorage(StandStorage external, int idx){
		external.AddToStorage(slots[idx].standbody);
	}
}
