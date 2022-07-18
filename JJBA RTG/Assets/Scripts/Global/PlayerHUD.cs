using UnityEngine;
using UnityEngine.UI;

public sealed class PlayerHUD : MonoBehaviour
{
   	public Slider healthBar;
	public void SetMaxHealth(int health){
		healthBar.maxValue = health;
		healthBar.value = health;
	}
	public void SetHealth (int health) {
		healthBar.value = health;
	}

}
