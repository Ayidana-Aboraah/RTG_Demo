using UnityEngine;

[RequireComponent(typeof(Standx), typeof(Animator))]
public sealed class StandBody : MonoBehaviour
{
	[HideInInspector] public string key;
	public Animator ani;
	public Standx stand;
	//public SkillTree tree;

	public void Spawn(Transform spawnPoint) => Instantiate(gameObject, spawnPoint.position, spawnPoint.rotation, spawnPoint);

	public void Update() => stand.ApplyAttributes();

    public void Despawn() => stand.despawn();
}