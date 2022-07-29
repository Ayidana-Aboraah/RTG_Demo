using UnityEngine;

public class TheWorldAI : EnemyAI
{
    [Header("Boss Vars")]
    public float poseDistance, throwMin;
    // public Transform spawnPoint;
    // public GameObject minion;
    // public Timer spawnTimer;

    TheWorld m_TheWorld;

    bool phase3;

    internal override void Start()
    {
        base.Start();
        m_TheWorld = (TheWorld)stand.stand;
    }

    // public void SpawnMinion()
    // {
    //     Instantiate(minion, spawnPoint.position, spawnPoint.rotation, spawnPoint);
    //     spawnTimer.Start();
    // }

    // public override void Movement()
    // {
    //     // if (spawnTimer.complete) agent.destination = transform.position;
    //     // else base.Movement();
    // }

    public override void InputCycles()
    {
        base.InputCycles();
        // spawnTimer.UpdateTimer();

        #region Phase 1
        if (distance <= m_TheWorld.atkBox.range && !atkTimer.isRunning) Atk();
        if (distance <= m_TheWorld.spAtkBox.range && !spAtkTimer.isRunning) SpAtk();
        if (distance > throwMin && !ATimers[2].isRunning) A(3, 1);
        #endregion

        #region Phase 2
        if (stats.hp > 200) return;

        // if (distance > throwMin / 2 && !spawnTimer.isRunning) SpawnMinion();

        if (distance <= m_TheWorld.barrageBox.range && !strongTimer.isRunning) Strong();

        if (distance <= m_TheWorld.A1Box.range && !ATimers[0].isRunning) A(1, 1); //Start Muda kicks

        if (distance <= poseDistance + 1f && !ATimers[1].isRunning) A(2, 1); //Start Time skipping to dodge
        #endregion

        #region Phase 3
        if (stats.hp > 100 && !phase3) return;
        phase3 = true;

        if (distance <= m_TheWorld.heavyBox.range && !heavyTimer.isRunning) Heavy();

        if (distance < poseDistance) {if (posing) Pose(false);}
        else Pose(true);
        #endregion
    }
}
