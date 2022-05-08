using UnityEngine;

public sealed class StandBody : MonoBehaviour
{
	[HideInInspector] public string key;
	public GameObject body;
	public Animator ani;
	public Standx stand;
	//public SkillTree tree;

	public void Spawn(Transform spawnPoint){
		Instantiate(body, spawnPoint.position, spawnPoint.rotation, spawnPoint);
	}

	public void Update(){
		stand.ApplyAttributes();
	}

    public void Despawn() => stand.despawn();

    void OnDrawGizmosSelected()
	{
		stand.DrawBoxes();
	}
}