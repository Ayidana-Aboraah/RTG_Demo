using UnityEngine;

public sealed class Standless : MonoBehaviour
{
	[Header("Standless")]
	public Hitbox atkBox, barrageBox, heavyBox;

	[Header("Gun")]
	public int gunDamage, ammo = 4;
	public float fireRange;
	
	#region Standless Atks
	public void AtkSL()
	{
		atkBox.Atk();
	}

	public void SpAtkSL()
	{
		//May pass camera for a more precise shot
		Ray shot = new Ray(transform.position, Vector3.forward * fireRange);
		RaycastHit hit;

		if (!Physics.Raycast(shot, out hit, fireRange, atkBox.opponent) || ammo < 1) return;

		hit.collider.GetComponent<Stats>().TakeDamage(gunDamage);
		ammo--;
	}

	public void StrongSL()
	{
		barrageBox.Atk();
	}

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
				ammo = 4;
				//Gun cooldown
				break;
			default:
				heavyBox.Effect(2, 1f); //Stun hitbox at the end
				break;
		}
		
		heavyBox.Atk();
	}

	public void UltimateSL()
	{
		//Activate Thunder Cross split Attack! animation
	}
	#endregion

	void OnDrawGizmos()
	{
		atkBox.DrawHitBox();
		barrageBox.DrawHitBox();
		heavyBox.DrawHitBox();
	}
}
