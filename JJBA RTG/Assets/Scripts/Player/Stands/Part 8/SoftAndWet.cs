using UnityEngine;

public class SoftAndWet : Melee
{
	[Header("Soft And Wet Vars")]
	public Hitbox A1Box;
	public Hitbox A2Box;
	public Transform A3Point;
	public GameObject Bubble;
	public GameObject GoBeyond;
	public LayerMask wall;
	public StandAttribute attributes;

	public override void SpAtk()
	{
		Instantiate(GoBeyond, A3Point.position, transform.parent.rotation, A3Point);
	}

	public override void A1()
	{
		A1Box.Atk();
		A1Box.Effect(3, A1Box.damage / 2);
	}

	public void A1x2()
	{
		attributes.StartBuff(0, A1Box.damage, 0);
	}

	public override void A2()
	{
		//make an attribute called weakended defense
		//Call that attribute
		A2Box.Effect(5, A2Box.range, A2Box.damage);
	}

	public override void A3()
	{
		Instantiate(Bubble, A3Point.position, atkBox.parent.rotation, A3Point);
	}

	public override void Ult()
	{
	}

	public override void ApplyAttributes()
	{
		if (Physics.CheckSphere(transform.parent.position, 2.5f, wall))
			transform.parent.GetComponent<Rigidbody>().useGravity = false;
		else
			transform.parent.GetComponent<Rigidbody>().useGravity = false;
	}

	public override void initialize()
	{
		base.initialize();
		attributes = GetComponentInParent<StandAttribute>();
	}

	public override void DrawBoxes(){
		base.DrawBoxes();
		A1Box.DrawHitBox();
		A2Box.DrawHitBox();
	}
}
