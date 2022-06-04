using UnityEngine;

public sealed class StandDictionary : MonoBehaviour
{
	public StandBody[] entries;
	
    public static StandDictionary instance;
	
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);

		if(instance == null) instance = this;
		else Destroy(gameObject);
	}

	public StandBody FindBody(string targetStand)
	{
		// foreach (StandBody body in entries) if(body.key == targetStand) return body;
		// return null;

		for (int i = 0, z = entries.Length -1; i < entries.Length; i++, z--){
			if (entries[i].key == targetStand)
				return entries[i];
			if (entries[z].key == targetStand)
				return entries[z];
		}
		return null;
	}
}
