using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy), typeof(NavMeshAgent))]
public class EnemyCombat : Combat
{
    [Header("Enemy Combat")]
    public Transform target;
    public float distance;
    internal NavMeshAgent agent;

    internal override void m_Start() => agent = GetComponent<NavMeshAgent>();

    internal override void m_Update()
    {
        if (target != null) distance = Vector3.Distance(transform.position, target.position);
        Inputs();
    }

    public virtual void Movement() => agent.SetDestination(target.position);
    internal override void Inputs(){}
}
