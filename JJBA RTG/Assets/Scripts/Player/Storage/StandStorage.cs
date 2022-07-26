using UnityEngine;

public sealed class StandStorage : MonoBehaviour
{
    public StandSlot[] slots = new StandSlot[6];
    public PlayerCombat combat;

    private void Awake()
    {
        combat = GetComponentInParent<PlayerCombat>();
		for (int i = 0; i < slots.Length; i++) slots[i].Initialize();
    }

    public void SwitchStand(int idx) // FIX: This function isn't working correctly, the player
    {
        if (combat.stand != null) combat.stand.Despawn();
		combat.standOn = false;

        slots[idx].standbody.Spawn(combat.transform);
        slots[idx].standbody.stand.SetCooldowns(combat);
        combat.stand = combat.transform.GetChild(2).GetComponent<StandBody>();
    }

    public void AddToStorage(StandBody newStand)
    {
        for (int i = 0; i <= slots.Length; i++)
        {
            if (!slots[i].full)
            {
                slots[i].addStand(newStand);
                break;
            }
        }
    }

    public void RemoveStand(int idx) => slots[idx].removeStand();

    public void AddToExternalStorage(StandStorage external, int idx) => external.AddToStorage(slots[idx].standbody);
}
