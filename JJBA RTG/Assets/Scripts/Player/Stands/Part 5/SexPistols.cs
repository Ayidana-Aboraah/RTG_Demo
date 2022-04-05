using UnityEngine;

public class SexPistols : Standx
{
	public Hitbox atkBox;
	public Hitbox heavyBox;
	[Header("Gun")]
	public float bulletDmg, gunRange;
	public int pistols;
	public int ammo;
	public GameObject bullet;
	public Transform aimbotTarget, firingPoint;
	public LayerMask opp;
	
	private void Start()
	{
		stats = GetComponentInParent<Player>();
		ammo = 6;
	}
	
	public void shoot(int pistolType){
		if (ammo < 1) return;
		
		float finDamage = pistols * stats.damageMultiplier;
		bullet.GetComponent<SexBullet>().damageMultiplier = finDamage/2;
		bullet.GetComponent<SexBullet>().pistolType = pistolType;
		Instantiate(bullet, firingPoint.position, transform.rotation, firingPoint);
		ammo--;
	}

	public override void Atk()
	{
		shoot(0);
	}
	
	public override void SpAtk()
	{
		//sets shield health to 0 or breaks the shield
		shoot(1);
	}
	
	public override void Strong(){
		//Instantiate a bullet
		//that will play an animation that ricochets and does damage
		// (the amount of times the animation is called is based on how many sex pisotols are present)
		shoot(2);
	}
		public override void Heavy()
	{
		heavyBox.Atk();
		//Just slaps with pistol
	}
	
	public override void Ult()
	{
		//Calls cutscene
		//Cutscene finishes off with a bleeding hitbox and a large amount of damage
	}

	public override void A1(){
		//sets the target a target variable 
		RaycastHit hit;
		
		if(Physics.Raycast(firingPoint.position, Vector3.forward, out hit, gunRange, opp))
			aimbotTarget = hit.transform;
	}
	
	public override void A2()
	{
		if(aimbotTarget == null) return;
		bullet.GetComponent<SexBullet>().Target(aimbotTarget);
		shoot(0);
		bullet.GetComponent<SexBullet>().DeTarget();
	}
	
	public override void A3()
	{
		ammo = 6;
	}
	
	public void AtkSL()
	{
		atkBox.Atk();		
	}
	
	public override void DrawBoxes()
	{
		Gizmos.DrawLine(transform.position, Vector3.forward * gunRange);
	}
}
