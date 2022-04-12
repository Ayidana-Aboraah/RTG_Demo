using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public sealed class PlayerCombat : MonoBehaviour
{
	[Header("Stand")] public StandBody stand;
	bool standOn;

	[Header("Posing")]
	public int recovery;

	[Header("Cooldowns")]

	#region Timers

	public Timer atkTimer;
	public Timer spAtkTimer;
	public Timer strongTimer;
	public Timer heavyTimer;
	public Timer ultTimer;
	public Timer[] ATimers = new Timer[9];

	#endregion

	[Header("Misc")] public LayerMask enemy;

	[HideInInspector] public Standless standless;
	Animator ani;
	Player stats;

	#region Player Input

	PlayerInput input;

	private void Awake()
	{
		input = new PlayerInput();
	}

	private void OnEnable()
	{
		input.Enable();
	}

	private void OnDisable()
	{
		input.Disable();
	}

	#endregion

	private void Start()
	{
		stats = GetComponent<Player>();
		ani = GetComponent<Animator>();
		standless = GetComponent<Standless>();
		
		Inputs();

		if (stand == null) standOn = false;
		else
		{
			standOn = true;
			
			atkTimer.maxTime = 0;
			spAtkTimer.maxTime = 0;
			heavyTimer.maxTime = 0;
			strongTimer.maxTime = 0;
			
			ATimers[0].maxTime = 0;
			ATimers[1].maxTime = 0;
			ATimers[2].maxTime = 0;
		}
	}

	private void Update()
	{
		UpdateTimers();
		ani.SetBool("Standless", !standOn);

		if (standOn) stand.stand.ApplyAttributes();
	}

	#region Basics

	public void PoseHeal()
	{
		stats.hp += recovery;
	}

	public void Pose()
	{
		if (stats.stopped) return;
		bool newState = !(ani.GetBool("Posing"));

		ani.SetBool("Posing", newState);
		if (standOn) stand.ani.SetBool("Posing", newState);
	}

	public void Summon()
	{
		if (stats.stopped) return;
		bool summoning = !(standOn);

		stand.body.SetActive(summoning);
		standOn = summoning;

		if (standOn) return;

		atkTimer.maxTime   = 0;
		spAtkTimer.maxTime = 0;
		strongTimer.maxTime= 0;
		heavyTimer.maxTime = 0;
		
		ATimers[0].maxTime = 0;
		ATimers[1].maxTime = 0;
		ATimers[2].maxTime = 0;
	}

	public void Block()
	{
		if (stats.stopped) return;
		bool blocking = !(stats.blocking);

		ani.SetBool("Blocking", blocking);
		if (standOn) stand.ani.SetBool("Blocking", blocking);
	}

	#endregion

	#region Normal Stand on and off attacks

	public void Atk()
	{
		if (atkTimer.isRunning || stats.stopped) return;

		ani.SetBool("Atk", true);
		stand.ani.SetTrigger("Atk");
	}

	public void SpAtk()
	{
		if (spAtkTimer.isRunning || stats.stopped) return;

		ani.SetBool("SpAtk", true);
		stand.ani.SetTrigger("SpAtk");
	}

	public void Strong()
	{
		if (strongTimer.isRunning || stats.stopped) return;

		ani.SetBool("Strong", true);
		stand.ani.SetTrigger("Strong");
	}

	public void Heavy()
	{
		if (heavyTimer.isRunning || stats.stopped) return;

		ani.SetBool("Heavy", true);
		stand.ani.SetTrigger("Heavy");
	}

	public void A(int AType, int TypeVariant)
	{
		if (ATimers[AType - 1].isRunning || stats.stopped) return;

		ani.SetBool("A" + AType + "." + TypeVariant, true);
		stand.ani.SetTrigger("A" + AType + "." + TypeVariant);
	}

	//Figure out how to make Ultimate cutscene thing
	public void Ult()
	{
		if(ultTimer.isRunning || stats.stopped) return;
		
		if (!standOn) ani.SetBool("Ult", true);
		else stand.stand.Ult();
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

	public void ClearStands()
	{
		standOn = false;
		stand = null;
		
		if (transform.childCount < 2) return;
		
		StandBody[] stands = GetComponentsInChildren<StandBody>();
		foreach (StandBody body in stands) body.Despawn();
	}

	#endregion

	public void Inputs()
	{
		input.Combat.Pose.started += _ =>   Pose();
		input.Combat.Summon.started += _ => Summon();
		input.Combat.Block.started += _ =>  Block();

		input.Combat.Atk.started += _ => 	Atk();
		input.Combat.SpAtk.started += _ =>  SpAtk();
		input.Combat.Strong.started += _ => Strong();
		input.Combat.Heavy.started += _ =>  Heavy();

		input.A.A1.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(1, 1);
			if (context.interaction is HoldInteraction) 	A(1, 2);
			if (context.interaction is MultiTapInteraction) A(1, 3);
		};
		input.A.A2.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(2, 1);
			if (context.interaction is HoldInteraction) 	A(2, 2);
			if (context.interaction is MultiTapInteraction) A(2, 3);
		};
		input.A.A3.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(3, 1);
			if (context.interaction is HoldInteraction) 	A(3, 2);
			if (context.interaction is MultiTapInteraction) A(3, 3);
		};

		input.A.A4.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(4, 1);
			if (context.interaction is HoldInteraction) 	A(4, 2);
			if (context.interaction is MultiTapInteraction) A(4, 3);
		};
		input.A.A5.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(5, 1);
			if (context.interaction is HoldInteraction) 	A(5, 2);
			if (context.interaction is MultiTapInteraction) A(5, 3);
		};
		input.A.A6.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(6, 1);
			if (context.interaction is HoldInteraction) 	A(6, 2);
			if (context.interaction is MultiTapInteraction) A(6, 3);
		};

		input.A.A7.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(7, 1);
			if (context.interaction is HoldInteraction) 	A(7, 2);
			if (context.interaction is MultiTapInteraction) A(7, 3);
		};
		input.A.A8.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(8, 1);
			if (context.interaction is HoldInteraction) 	A(8, 2);
			if (context.interaction is MultiTapInteraction) A(8, 3);
		};
		input.A.A9.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(9, 1);
			if (context.interaction is HoldInteraction) 	A(9, 2);
			if (context.interaction is MultiTapInteraction) A(9, 3);
		};
	}
}