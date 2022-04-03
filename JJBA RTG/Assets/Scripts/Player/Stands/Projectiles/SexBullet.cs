using UnityEngine;

public class SexBullet : Projectile
{
	public int pistolType;
	public Transform target;
	public bool targeting;
	Animator ani;
	
	internal override void Start()
	{
		base.Start();
		ani = GetComponent<Animator>();
	}
	
	public override void Atk()
	{
		if(box.AtkProjectile(damageMultiplier)){
			switch(pistolType){
				case 2:
				// StartCoroutine(Barrage());
				break;
				//Shield break pistol
				case 1:
					Collider[] plrs = Physics.OverlapSphere(box.point.position, box.range, box.opponent);
					foreach (Collider other in plrs)
						if(other.transform != box.parent)
						{
							//Just get a object that would be responsible for shield break
							other.GetComponent<Stats>().ShieldBreak();
						}
				break;
				//Barrage pistol
				default:
				Destroy(gameObject);
				break;
			}
		}
	}
	
	public void Target(Transform newTarget)
	{
		target = newTarget;
		targeting = true;
	}

	public void DeTarget(){
		target = null;
		targeting = false;
	}

	public override void Move()
	{
		if(!targeting)
			base.Move();
		else
			rb.MovePosition(Vector3.MoveTowards(transform.position, target.position, speed));
	}

	// IEnumerator Barrage()
	// {
	// 	ani.enabled = !ani.enabled;
	// 	ani.SetBool("Barrage", true);
	// 	WaitUntil(ani.GetBool("Barrage"));
	// 	ani.enabled = !ani.enabled;
	// }

	public void StopBarrage(){
		ani.SetBool("Barrage", false);
	}
}
