using UnityEngine;

[RequireComponent(typeof(Standx), typeof(Animator))]
public sealed class StandBody : MonoBehaviour
{
	[HideInInspector] public string key;
	public Animator ani;
	public Standx stand;
	//public SkillTree tree;

	public void Spawn(PlayerCombat combat) => combat.stand = Instantiate(gameObject, combat.transform.position, combat.transform.rotation, combat.transform).GetComponent<StandBody>();

	public void Update() => stand.ApplyAttributes();

    public void Despawn() => stand.despawn();
}