using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy), typeof(NavMeshAgent))]
public class EnemyCombat : Combat
{
    [Header("Enemy Combat")]
    public Transform target;
    internal NavMeshAgent agent;

    internal override void m_Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.speed;
    }

    internal override void m_Update()
    {
        Inputs();
    }

    public virtual void Movement()
	{
		agent.SetDestination(target.position);
	}
}
