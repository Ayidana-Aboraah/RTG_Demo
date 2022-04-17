using UnityEngine;

public class Combat : MonoBehaviour
{
	[Header("Stand")]
    public StandBody stand;
	public bool standOn;

	[Header("Posing")]
	public int recovery;

	[Header("Cooldowns")]

    //Maybe remove Timers in general
	#region Timers 
	public Timer spAtkTimer;
	public Timer ultTimer;
	public Timer[] ATimers = new Timer[9];

	#endregion

	[Header("Misc")] public LayerMask enemy;

	[HideInInspector] public Standless standless;
	internal Animator ani;
	internal Stats stats;

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
		
		m_Start();

		if (stand == null) standOn = false;
		else {
			standOn = true;
			
			spAtkTimer.maxTime = 0;
			ultTimer.maxTime = 0;

			ATimers[0].maxTime = 0;
			ATimers[1].maxTime = 0;
			ATimers[2].maxTime = 0;
		}
	}

	internal void Update()
	{
		UpdateTimers();
		ani.SetBool("Standless", !standOn);

		if (standOn) stand.stand.ApplyAttributes();

		m_Update();
	}

	#region Basics

	public void PoseHeal(){ stats.hp += recovery;}

	public void Pose(bool newState)
	{
		if (stats.stopped) return;

		ani.SetBool("Posing", newState);
		if (standOn) stand.ani.SetBool("Posing", newState);
	}

	public void Block(bool blocking)
	{
		if (stats.stopped) return;

		ani.SetBool("Blocking", blocking);
		if (standOn) stand.ani.SetBool("Blocking", blocking);
	}

	#endregion

	#region Normal Stand on and off attacks

	public void Atk()
	{
		if (stats.stopped) return;

		ani.SetBool("Atk", true);
		if (standOn) stand.ani.SetTrigger("Atk");
	}

	public void SpAtk()
	{
		if (spAtkTimer.isRunning || stats.stopped) return;

		ani.SetBool("SpAtk", true);
		if (standOn) stand.ani.SetTrigger("SpAtk");
	}

	public void Strong()
	{
		if (stats.stopped) return;

		ani.SetBool("Strong", true);
		if (standOn) stand.ani.SetTrigger("Strong");
	}

	public void Heavy()
	{
		if (stats.stopped) return;

		ani.SetBool("Heavy", true);
		if (standOn) stand.ani.SetTrigger("Heavy");
	}

	public void A(int AType, int TypeVariant)
	{
		if (stats.stopped) return;

		ani.SetBool("A" + AType + "." + TypeVariant, true);
		if (standOn) stand.ani.SetTrigger("A" + AType + "." + TypeVariant);
	}

	//Figure out how to make Ultimate cutscene thing
	public void Ult()
	{
		if(ultTimer.isRunning || stats.stopped) return;
		
		if (standOn) stand.stand.Ult();
		else ani.SetBool("Ult", true); //Fill in until we add a proper ultimate technique
	}

	#endregion

	#region Tools/Maintainance

	private void UpdateTimers()
	{
		spAtkTimer.UpdateTimer();
		ultTimer.UpdateTimer();

		foreach(Timer timer in ATimers) timer.UpdateTimer();
	}

	#endregion

	internal virtual void Inputs(){}

	internal virtual void m_Start(){}

	internal virtual void m_Update(){}
}