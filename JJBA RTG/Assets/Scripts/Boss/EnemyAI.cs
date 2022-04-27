using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy), typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
	[Header("Stand")] public StandBody stand;
	[Header("Posing")] public bool posing;
	public int recovery;

	[Header("Cooldowns")]
	#region Timers

	public Timer atkTimer;
	public Timer spAtkTimer;
	public Timer strongTimer;
	public Timer heavyTimer;
	public Timer ultTimer;
	public Timer[] ATimers;

	#endregion

	[Header("Misc")] public float distance;
	public Transform target;

	private Animator ani;
	internal Enemy stats;
	internal NavMeshAgent agent;	

	internal virtual void Start()
	{
		stats = GetComponent<Enemy>();
		ani = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		stand = GetComponentInChildren<StandBody>();

		agent.speed = stats.speed;
	}

	private void Update()
	{
		UpdateTimers();
		// UpdateAnimations();
		InputCycles();
		Movement();
		if (stand != null) stand.stand.ApplyAttributes();
	}

	#region Basics
	public void PoseHeal() { stats.hp += recovery; }

	public void Block(bool val)
	{
		ani.SetBool("Blocking", val);
		stats.blocking = val;
	}

	public void Pose(bool val)
	{
		ani.SetBool("Posing", val);
		posing = val;
	}
	#endregion

	#region Normal Stand on and off attacks

	public void Atk()
	{
		stand.ani.SetTrigger("Atk");
		atkTimer.Start();
	}

	public void SpAtk()
	{
		stand.ani.SetTrigger("SpAtk");
		spAtkTimer.Start();
	}

	public void Strong()
	{
		stand.ani.SetTrigger("Strong");
		strongTimer.Start();
	}

	public void Heavy()
	{
		stand.ani.SetTrigger("Heavy");
		heavyTimer.Start();
	}

	public void A(int AType, int TypeVariant)
	{
		stand.ani.SetTrigger("A" + AType.ToString() + "." + TypeVariant.ToString());
		ATimers[AType - 1].Start();
	}
	
	public void Ult() // Figure out how to make Ultimate cutscene thing
	{
		stand.stand.Ult();
	}

	#endregion
	
	#region Tools/Maintainance

	private void UpdateTimers()
	{
		atkTimer.UpdateTimer();
		spAtkTimer.UpdateTimer();
		strongTimer.UpdateTimer();
		heavyTimer.UpdateTimer();
		ultTimer.UpdateTimer();

		foreach (Timer ATimer in ATimers) ATimer.UpdateTimer();
	}

	private void UpdateAnimations()
	{
		ani.SetBool("Posing", posing);
		ani.SetBool("Blocking", stats.blocking);

		stand.ani.SetBool("Posing", posing);
		stand.ani.SetBool("Blocking", stats.blocking);
	}

	#endregion

	public virtual void InputCycles()
	{
		distance = Vector3.Distance(transform.position, target.position);
	}

	public virtual void Movement()
	{
		agent.SetDestination(target.position);
	}
}