using UnityEngine;

public class GoBeyond : Projectile
{
	[Header("Go Beyond")]
	public float targetRange;
	public Transform target;
	
	public override void Move()
	{
		if(target != null)
			rb.MovePosition(Vector3.MoveTowards(rb.position, target.position, speed/3 * Time.deltaTime));
	}
	
	public void Target(Transform parent){
		box.parent = parent;
		Collider[] hits = Physics.OverlapSphere(transform.position, targetRange, box.opponent);
		foreach(Collider hit in hits)
			if(hit.transform != box.parent){
				target = hit.transform;
				return;
			}
	}

	internal override void DrawBoxes(){
		base.DrawBoxes();
		Gizmos.DrawWireSphere(transform.position, targetRange);
	}
}
