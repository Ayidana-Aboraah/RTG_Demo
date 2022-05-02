using UnityEngine.UI;
using TMPro;

[System.Serializable]
public sealed class StandSlot
{
	public StandBody standbody;
	public Button button;
	public bool full;
	
	public void Initialize()
	{
		button.interactable = true;
		button.GetComponentInChildren<TMP_Text>().text = standbody.stand.name;
	}
	
	public void addStand(StandBody newStand)
	{
		standbody = newStand;
		full = true;
		button.interactable = true;
		button.GetComponentInChildren<TMP_Text>().text = standbody.name;
	}
	
	public void removeStand()
	{
		standbody = null;
		full = false;
		button.interactable = false;
		button.GetComponentInChildren<TMP_Text>().text = "empty";
	}
}
