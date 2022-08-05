using UnityEngine;

public sealed class SoftAndWet : Melee
{
	[Header("Soft And Wet Vars")]
	public Hitbox A1Box, A2Box;
	public Transform A3Point;
	public GameObject Bubble, GoBeyond;
	StandAttribute attributes;

	public override void SpAtk() => Instantiate(GoBeyond, A3Point.position, parent.rotation, A3Point);

	public override void A1()
	{
		A1Box.Atk();
		A1Box.Effect(3, A1Box.damage / 2);
	}

	public void A1x2() => attributes.StartBuff(0, A1Box.damage, 0);

	public override void A2() => A2Box.Effect(5, A2Box.range, A2Box.damage); // Defense Debuff

	public override void A3() => Instantiate(Bubble, A3Point.position, atkBox.parent.rotation);

	public override void Ult(){}

	public override void ApplyAttributes()
	{
		if (Physics.CheckSphere(parent.position, 2.5f, 6)) // 6 = Wall
			parent.GetComponent<Rigidbody>().useGravity = false;
		else if (parent.GetComponent<Rigidbody>().useGravity)
			parent.GetComponent<Rigidbody>().useGravity = true;
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
