using UnityEngine;

public sealed class SoftAndWet : Melee
{
	[Header("Soft And Wet Vars")]
	public Hitbox A1Box, A2Box;
	public Transform A3Point;
	public GameObject Bubble, GoBeyond;
	public GoBeyond current_beyond;
	StandAttribute attributes;
	LayerMask wall;

	public override void SpAtk(){
		if (current_beyond != null) current_beyond.Target(parent);
		else current_beyond = Instantiate(GoBeyond, A3Point.position, parent.rotation).GetComponent<GoBeyond>();
	}

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
		if (Physics.CheckSphere(parent.position, 2.5f, wall)) // 6 = Wall
			parent.GetComponent<Rigidbody>().useGravity = false;
		else if (parent.GetComponent<Rigidbody>().useGravity)
			parent.GetComponent<Rigidbody>().useGravity = true;
	}

	public override void initialize()
	{
		base.initialize();
		attributes = GetComponentInParent<StandAttribute>();
		wall = LayerMask.GetMask("Wall");
	}

	public override void DrawBoxes(){
		base.DrawBoxes();
		A1Box.DrawHitBox();
		A2Box.DrawHitBox();
	}
}
