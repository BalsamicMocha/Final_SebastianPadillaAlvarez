using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    public void Init(NavMeshAgent agentRef)
    {
        agent = agentRef;
    }

    public void MoveToTarget(Transform targetTransform)
    {
        target = targetTransform;
        agent.SetDestination(target.position);
    }

    private void Update()
    {
        if (target != null)
            agent.SetDestination(target.position);
    }

    public void Stop()
    {
        target = null;
        agent.ResetPath();
    }
}
