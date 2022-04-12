using UnityEngine;

public sealed class StandDictionary : MonoBehaviour
{
	public StandBody[] standDictions;
	
    public static StandDictionary instance;
	
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);

		if(instance == null) instance = this;
		else Destroy(gameObject);
	}

	public StandBody FindBody(string targetStand)
	{
		foreach (StandBody body in standDictions) if(body.key == targetStand) return body;
		return null;
	}
}
