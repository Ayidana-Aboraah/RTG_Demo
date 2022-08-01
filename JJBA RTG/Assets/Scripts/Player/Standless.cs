using UnityEngine;

public sealed class Standless : MonoBehaviour
{
	[Header("Standless")]
	public Hitbox atkBox, barrageBox, heavyBox;

	#region Standless Atks
	public void AtkSL() => atkBox.Atk();

	public void SpAtkSL() => GetComponent<Player>().shieldHp += 5;

	public void StrongSL() => barrageBox.Atk();

	public void HeavySL()
	{
		heavyBox.Atk();
		heavyBox.Effect(2, 2.5f);
	}

	public void ASL(int index)
	{
		switch (index-1)
		{
			case 1:
				heavyBox.Effect(3, 3f); //Use tripped attribute
				break;
			case 2:
				atkBox.Atk(2f); //Extra hit box
				break;
			default:
				heavyBox.Effect(2, 1f); //Stun hitbox at the end
				break;
		}
		
		heavyBox.Atk();
	}

	public void UltimateSL(){} //Activate Thunder Cross split Attack! animation

	#endregion

	void OnDrawGizmos()
	{
		atkBox.DrawHitBox();
		barrageBox.DrawHitBox();
		heavyBox.DrawHitBox();
	}
}
