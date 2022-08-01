using UnityEngine;

public sealed class Movement : MonoBehaviour
{
	public float sensetivity, speed, jumpForce, dashForce;
	public short jumps = 2, maxJumps = 2;
	public Timer dashCooldown;

	[Header("Camera")] public Transform cam; 
	float smoothVelocity;

	[HideInInspector] public Animator ani;
	PlayerInput input;
	LayerMask ground;
	Rigidbody rb;
	Player stats;
	Vector3 MvIn;

	private void Start()
	{
		rb = GetComponentInChildren<Rigidbody>();
		ground = LayerMask.GetMask("ground");
		ani = GetComponent<Animator>();
		stats = GetComponent<Player>();
		input = InputManager.input;

		Cursor.lockState = CursorLockMode.Locked;

		inputs();
	}

	private void FixedUpdate()
	{
		dashCooldown.UpdateTimer();

		if (stats.stopped) return;

		float x = input.Movement.Horizontal.ReadValue<float>();
		float y = input.Movement.Vertical.ReadValue<float>();

		// ani.SetFloat("x", x);
		// ani.SetFloat("y", y);
		
		MvIn = new Vector3(x, 0f, y);

		if (MvIn.magnitude >= .1f){
			float targetAngle = Mathf.Atan2(x, y) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, sensetivity);

			transform.rotation = Quaternion.Euler(0f, angle, 0f);
			rb.MovePosition(transform.position + ((Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized * speed/10));
		}	
	}

	public void Jump()
	{
		if (stats.stopped) return; // NOTE: Remove this if we have an animation that calls this function | Also, breaks control flow
		
		if (Physics.CheckSphere(transform.position + Vector3.down, .25f, ground))
			jumps = maxJumps;
		
		if (jumps > 0){
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			jumps -= 1;
		}
	}

	public void Dash()
	{
		if (dashCooldown.isRunning || stats.stopped) return; // NOTE: Remove stats.stopped if animation is for this
	
		dashCooldown.Start();
		
		if (MvIn == Vector3.zero)
			rb.AddForce((transform.forward * -1) * (dashForce*100), ForceMode.Acceleration);
		else 
			rb.AddForce(transform.TransformDirection(MvIn) * dashForce, ForceMode.Impulse);
	}
	
	private void inputs()
	{
		input.Movement.Dash.started += _ => Dash();
		input.Movement.Jump.started += _ => Jump();
		
		// input.Movement.Jump.started += _ => ani.SetTrigger("Jumping", true);
		// input.Movement.Dash.started += _ => ani.SetTrigger("Dashing", true);
	}
}
