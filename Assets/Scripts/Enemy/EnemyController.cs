using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { Idle, Chasing, Attacking, Dead }

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [Header("Componentes")]
    public NavMeshAgent agent;
    public EnemyMovement movement;
    public EnemyAttack attack;

    [Header("Detección")]
    public float attackRange = 1.5f; // distancia para atacar al jugador
    private Transform player;

    [Header("Estado")]
    public EnemyState currentState = EnemyState.Idle;

    private void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (movement == null)
            movement = GetComponent<EnemyMovement>();

        if (attack == null)
            attack = GetComponent<EnemyAttack>();

        movement.Init(agent);
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:                
                break;

            case EnemyState.Chasing:
                if (player != null)
                {
                    movement.MoveToTarget(player);

                    float distance = Vector3.Distance(transform.position, player.position);
                    if (distance <= attackRange)
                        StartAttack();
                }
                break;

            case EnemyState.Attacking:                
                break;

            case EnemyState.Dead:
                agent.isStopped = true;
                break;
        }
    }
        
    public void StartChasing(Transform playerTransform)
    {
        player = playerTransform;
        currentState = EnemyState.Chasing;
        agent.isStopped = false;
    }
        
    public void StopChasing()
    {
        player = null;
        currentState = EnemyState.Idle;
        agent.ResetPath();
    }

    private void StartAttack()
    {
        currentState = EnemyState.Attacking;
        Die();
    }

    public void Die()
    {
        currentState = EnemyState.Dead;
       
    }
}
