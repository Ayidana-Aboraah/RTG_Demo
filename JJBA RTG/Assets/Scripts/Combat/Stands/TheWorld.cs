using UnityEngine;

public class TheWorld : Melee
{
	public Hitbox spAtkBox, A1Box, A4Box;
	
	[Header("The World")]
	public GameObject knives;
	public Transform A3Point;
	public float bleedLength, skipDistance;
	
	internal Rigidbody rb;
	
	public override void SpAtk() => spAtkBox.Effect(0, spAtkBox.damage);
	
	public override void Heavy()
	{
		base.Heavy();
		heavyBox.Effect(1, bleedLength, heavyBox.damage);
	}
	
	public override void Ult()
	{
	}

	public override void A1() => A1Box.Atk(stats.damageMultiplier);
	
	public override void A2() => rb.MovePosition(rb.position + parent.TransformDirection(Vector3.forward * skipDistance));
	
	public override void A3() => Instantiate(knives, A3Point.position, parent.rotation);

	public virtual void A4() => A4Box.Atk();

	public virtual void A5(){
		//Camera
	}

	public override void initialize(){
		base.initialize();
		rb = GetComponentInParent<Rigidbody>();
		
		spAtkBox.parent = parent;
		A1Box.parent = parent;
		A4Box.parent = parent;
	}

	public override void DrawBoxes(){
		base.DrawBoxes();
		spAtkBox.DrawHitBox();
		A1Box.DrawHitBox();
		A4Box.DrawHitBox();
	}
}
