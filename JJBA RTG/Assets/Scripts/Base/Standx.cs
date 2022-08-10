using UnityEngine;

public abstract class Standx : MonoBehaviour
{
	internal Transform parent;
	internal Stats stats;
	
	#region Stand On
	public abstract void Atk();
	public abstract void SpAtk();
	public abstract void Strong();
	public abstract void Heavy();
	public abstract void A1();
	public abstract void A2();
	public abstract void A3();
	public abstract void Ult();
	#endregion

	public virtual void ApplyAttributes(){}
	
	#region Maintenence
	
	public virtual void initialize()
	{
		// NOTE: Set Player Animator Controller to Preset Stand Ani-Controller for simplicity when working with animations
		stats = GetComponentInParent<Stats>();
		parent = transform.parent;
	}

	public virtual void despawn(){
		// NOTE: Reset Player Animator back to normal
		Destroy(gameObject);
	}

	public abstract void DrawBoxes();
	#endregion
	
	void Awake() => initialize();	
	
	void OnDrawGizmosSelected() => DrawBoxes();
}
