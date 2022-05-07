using UnityEngine;

public class Melee : Standx
{
	[Header("Hitboxes")]public Hitbox atkBox, barrageBox, heavyBox;
	
	public override void Atk()
	{
		atkBox.Atk(stats.damageMultiplier);
	}

	public override void Strong(){
		barrageBox.Atk(stats.damageMultiplier);
	}
	
	public override void Heavy()
	{
		heavyBox.Atk(stats.damageMultiplier);
	}
	
	public override void SpAtk(){}
	public override void Ult(){}
	public override void A1(){}
	public override void A2(){}
	public override void A3(){}

	public override void DrawBoxes()
	{
		atkBox.DrawHitBox();
		barrageBox.DrawHitBox();
		heavyBox.DrawHitBox();
	}

	public override void initialize()
	{
		base.initialize();
		atkBox.parent = parent;
		barrageBox.parent = parent;
		heavyBox.parent = parent;
	}
}
