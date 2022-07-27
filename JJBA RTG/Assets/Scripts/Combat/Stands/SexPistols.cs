using UnityEngine;

public sealed class SexPistols : Standx
{
	public Hitbox heavyBox;
	[Header("Gun")]
	public float bulletDmg, targetRange;
	public int pistols, ammo;
	public SexBullet bullet;
	public Transform aimbotTarget, firingPoint;
	private void Start()
	{
		pistols = 6;
		ammo = 6;
	}
	
	public void shoot(int pistolType){
		if (ammo < 1) return;
		
		bullet.damageMultiplier = (bulletDmg *(pistols/2)) * stats.damageMultiplier;
		bullet.pistolType = pistolType;
		Instantiate(bullet.gameObject, firingPoint.position, transform.rotation);
		ammo--;
	}

	public override void Atk() => shoot(0); //Normal shot
	public override void SpAtk() =>	shoot(1); //sets shield health to 0 or breaks the shield
	public override void Strong() => shoot(2); // Makes a bullet that will perform the barrage animation and do damage based on the current number of sex pistols
	public override void Heavy() =>	heavyBox.Atk(); //Just slaps with pistol
	
	public override void Ult()
	{
		//Calls cutscene
		//Cutscene finishes off with a bleeding hitbox and a large amount of damage
	}

	public override void A1(){
		RaycastHit hit; //sets the target a target variable 
		if(Physics.Raycast(firingPoint.position, Vector3.forward, out hit, targetRange, 7)) // 7 = player
			aimbotTarget = hit.transform;
	}
	
	public override void A2()
	{
		if(aimbotTarget == null) return;
		bullet.Targeting(aimbotTarget, () => shoot(0));
	}
	
	public override void A3() => ammo = 6;
	
	public override void DrawBoxes() => Gizmos.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * targetRange);
}
