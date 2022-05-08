using UnityEngine;

public class TheWorldAI : EnemyAI
{
	[Header("Boss Vars")]
	public float poseDistance, throwMin;

	public GameObject minion;
	public Transform spawnPoint;
	public Timer spawnTimer;
	TheWorld m_TheWorld;

	internal override void Start()
	{
		base.Start();
		m_TheWorld = (TheWorld) stand.stand;
	}

	public void SpawnMinion()
	{
		Instantiate(minion, spawnPoint.position, spawnPoint.rotation, spawnPoint);
		spawnTimer.Start();
	}

	public override void Movement()
	{
		if (spawnTimer.complete) agent.destination = transform.position;
		else base.Movement();
	}

	public override void InputCycles()
	{
		base.InputCycles();
		spawnTimer.UpdateTimer();

	#region Phase 1
		if (distance <= m_TheWorld.atkBox.range && atkTimer.complete) Atk();
		if (distance <= m_TheWorld.spAtkBox.range / 2 && !spAtkTimer.isRunning) SpAtk();
		if (distance > throwMin && ATimers[2].complete) A(3, 1);
	#endregion

	#region Phase 2
		if (stats.hp > 200) return;


		if (spawnTimer.complete && distance > throwMin / 2) SpawnMinion();

		//Start Muda kicks
		if (distance <= m_TheWorld.A1Box.range && ATimers[0].complete) A(1, 1);

		//Start Time skipping to dodge
		if (distance <= poseDistance + 1f && ATimers[1].complete) A(2, 1);
	#endregion

	#region Phase 3
		if (stats.hp > 100) return;
		
		
		if (distance <= poseDistance && !posing)
			if (stats.shieldHp > -1 && !stats.blocking) Block(true);
			else if (stats.shieldHp < 0) Pose(true);

		if (distance > poseDistance)
		{
			if (stats.blocking) Block(false);
			if (posing) Pose(false);
		}
	#endregion
	}
}
