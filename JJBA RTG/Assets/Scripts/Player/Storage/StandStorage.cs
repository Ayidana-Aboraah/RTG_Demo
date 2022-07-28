using UnityEngine;

public sealed class StandStorage : MonoBehaviour
{
    public StandSlot[] slots = new StandSlot[6];
    public PlayerCombat combat;

    private void Awake()
    {
        for (int i = 0; i < slots.Length; i++) slots[i].Initialize();
    }

    public void SwitchStand(int idx)
    {     
        if (combat.stand != null) combat.stand.Despawn();
        combat.standOn = true;
        combat.ani.SetBool("Standless", false);
        slots[idx].standbody.Spawn(combat);
        slots[idx].standbody.stand.SetCooldowns(combat);
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
