using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : EnemyBehaviors
{
<<<<<<< Updated upstream
    // State enum
    public enum State
=======
    public float lookRadius = 10f;

    private State state;

    private enum State
>>>>>>> Stashed changes
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK
    }

    public float walkSpeed = 25f;
    public float runSpeed = 50f;
    public float lookRadius = 100f;
    public Transform[] waypoints;
    public bool debugOn = false;

    protected Transform target;
    protected NavMeshAgent agent;
    protected State state;
    protected int waypointIndex;

    private Animator animator;

    // Private methods

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
        waypointIndex = 0;
        target = PlayerManager.instance.player.transform;
=======
        player = PlayerManager.instance.player;
        target = player.transform;
>>>>>>> Stashed changes
        agent = GetComponent<NavMeshAgent>();
        state = State.IDLE;
        animator = GetComponent<Animator>();
    }

    private void SetDestination(Vector3 destination)
    {
<<<<<<< Updated upstream
        agent.SetDestination(destination);
=======
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            if (distance <= agent.stoppingDistance)
            {
                state = State.ATTACK;
            } else
            {
                state = State.CHASE;
            }
        } else
        {
            state = State.PATROL;
        }

        switch (state)
        {
            case State.IDLE:
                // Idle
                Idle();
                break;
            case State.PATROL:
                // Patrol
                Patrol();
                break;
            case State.CHASE:
                // Chase
                Chase();
                break;
            case State.ATTACK:
                // Attack
                Attack();
                break;
            default:
                // Do nothing
                break;
        }
>>>>>>> Stashed changes
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void Log(string message)
    {
        if (debugOn)
        {
            Debug.Log(message);
        }
    }

    // Public methods

    public void Idle()
    {
        animator.Play("Sit");
        Log("Enemy State: [IDLE]");
    }

    public void Patrol()
    {
        agent.speed = walkSpeed;
        animator.Play("Walk");
        SetDestination(waypoints[waypointIndex].position);
        Log("Enemy State: [PATROL]");
    }

    public void Chase()
    {
        agent.speed = runSpeed;
        animator.Play("Run");
        SetDestination(target.position);
        Log("Enemy State: [CHASE]");
    }

    public void Attack()
    {
        FaceTarget();
        PlayerManager.instance.EndGame(false);
        Log("Enemy State: [ATTACK]");
    }

    public void IncreaseIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}
