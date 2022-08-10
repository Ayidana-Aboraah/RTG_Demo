using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : Stats
{
	public Hitbox box;
	public Timer lifeTime;
	public float speed;
	internal Rigidbody rb;
	
	internal override void Start()
	{
		base.Start();
		lifeTime.Start();

		rb = GetComponent<Rigidbody>();
		// transform.SetParent(null); // May be needed for something
		box.parent = transform.parent;
	}
	
	internal virtual void FixedUpdate()
	{
		lifeTime.UpdateTimer();
		if (stopped) return;
		
		else if (lifeTime.complete)	Destroy(gameObject);
		else if (box.parent != null) Atk();
		
		Move();
	}
	
	public virtual void Atk()
	{
		if(box.AtkProjectile(damageMultiplier))	Destroy(gameObject);
	}
	
	public virtual void Move() => rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);

	internal virtual void DrawBoxes() => box.DrawHitBox();

	void OnDrawGizmosSelected() => DrawBoxes();
}