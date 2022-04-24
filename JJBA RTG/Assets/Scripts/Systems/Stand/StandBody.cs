using UnityEngine;

public sealed class StandBody : MonoBehaviour
{
	[HideInInspector] public string key;
	public GameObject body;
	public Animator ani;
	public Standx stand;
	//public SkillTree tree;

	public void Initialize()
	{
		body = gameObject;
		ani = GetComponent<Animator>();
		stand = GetComponent<Standx>();
		// tree = GetComponent<SkillTree>();
	}

	public void Spawn(Transform spawnPoint){
		Instantiate(body, spawnPoint.position, spawnPoint.rotation, spawnPoint);
	}

	public void Despawn(){
		Destroy(body);
	}
	
	void OnDrawGizmosSelected()
	{
		stand.DrawBoxes();
	}
}