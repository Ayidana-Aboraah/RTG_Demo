using UnityEngine;

public sealed class Movement : MonoBehaviour
{
	public float sensetivity;
	public Timer dashCooldown;
	public LayerMask ground;
	public float speed, jumpForce, dashForce;
	public short jumps = 2;
	public short maxJumps = 2;

	[HideInInspector] public Animator ani;
	Vector3 MvIn;
	Rigidbody rb;
	Player stats;
	PlayerInput input;

	private void Start()
	{
		ani = GetComponent<Animator>();
		stats = GetComponent<Player>();
		rb = GetComponentInChildren<Rigidbody>();
		input = FindObjectOfType<InputManager>().input;
		
		inputs();
	}

	private void FixedUpdate()
	{
		dashCooldown.UpdateTimer();

		if (stats.stopped) return;

		Move();
		Camera();
	}

	private void Move()
	{
		float x = input.Movement.Horizontal.ReadValue<float>();
		float y = input.Movement.Vertical.ReadValue<float>();

		// ani.SetFloat("x", x);
		// ani.SetFloat("y", y);
		
		MvIn = new Vector3(x, 0f, y);
		
		rb.MovePosition(transform.position + (transform.TransformDirection(MvIn) * speed/10));
	}
	
	private void Camera()
	{
		float mx = input.Camera.Horizontal.ReadValue<float>(); //Maybe remove this variable if readability isn't important
		transform.Rotate(0f, mx * sensetivity, 0f);
	}

	public void Jump()
	{
		if (jumps > 0){
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			jumps -= 1;
		}
		else if (Physics.CheckSphere(transform.position + Vector3.down, .15f, ground))
			jumps = maxJumps;
	}

	public void Dash()
	{
		if (dashCooldown.isRunning) return;
	
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
