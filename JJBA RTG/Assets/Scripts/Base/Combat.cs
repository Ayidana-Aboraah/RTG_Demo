using System.Collections;
using UnityEngine;

public class Combat : MonoBehaviour
{
	[Header("Stand")]
    public StandBody stand;
	public bool standOn;

	[Header("Posing")] public int recovery;

	[Header("Cooldowns")] public Timer ultTimer; // TODO: Set a universal value for the max for this
	internal Animator ani;
	internal Stats stats;

	private void Start()
	{
		stats = GetComponent<Stats>();
		ani = GetComponentInChildren<Animator>(); // NOTE: A bug might appear where it call
		
		m_Start();
		standOn = (stand == null) ? false : true;
	}

	internal void Update()
	{
		ultTimer.UpdateTimer();
		m_Update();
	}

	#region Basics

	public void PoseHeal() => stats.hp += recovery;

	public void Pose(bool newState)
	{
		if (stats.stopped) return;

		ani.SetBool("Posing", newState);
		if (standOn) stand.ani.SetBool("Posing", newState);
	}

	public void Block(bool blocking)
	{
		if (stats.stopped) return;

		stats.blocking = blocking;
		ani.SetBool("Blocking", blocking);
		if (standOn) stand.ani.SetBool("Blocking", blocking);
	}

	#endregion

	#region Normal Stand on and off attacks

	public void Atk()
	{
		if (stats.stopped) return;

		ani.SetTrigger("Atk");
		if (standOn) stand.ani.SetTrigger("Atk");
	}

	public void SpAtk()
	{
		if (stats.stopped) return;

		ani.SetTrigger("SpAtk");
		if (standOn) stand.ani.SetTrigger("SpAtk");
	}

	public void Strong()
	{
		if (stats.stopped) return;

		ani.SetTrigger("Strong");
		if (standOn) stand.ani.SetTrigger("Strong");
	}

	public void Heavy()
	{
		if (stats.stopped) return;

		ani.SetTrigger("Heavy");
		if (standOn) stand.ani.SetTrigger("Heavy");
	}

	public void A(int AType, int TypeVariant)
	{
		if (stats.stopped) return;

		ani.SetTrigger("A" + AType + "." + TypeVariant);
		if (standOn) stand.ani.SetTrigger("A" + AType + "." + TypeVariant);
	}

	//Figure out how to make Ultimate cutscene thing
	public void Ult()
	{
		if(ultTimer.isRunning || stats.stopped) return;
		
		if (standOn) stand.stand.Ult();
		else ani.SetTrigger("Ult"); //Fill in until we add a proper ultimate technique
	}

	#endregion

	internal virtual void Inputs(){}

	internal virtual void m_Start(){}

	internal virtual void m_Update(){}
}