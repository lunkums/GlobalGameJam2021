using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // State enum
    public enum State
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK
    }

    public float lookRadius = 10f;
    public Transform[] waypoints;

    protected Transform target;
    protected NavMeshAgent agent;
    protected State state;
    protected int waypointIndex;

    private Animator animator;

    // Private methods

    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = 0;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        state = State.IDLE;
        animator = GetComponent<Animator>();
    }

    private void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
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

    // Public methods

    public void Idle()
    {
        animator.Play("Sit");
        Debug.Log("Enemy State: [IDLE]");
    }

    public void Patrol()
    {
        animator.Play("Walk");
        SetDestination(waypoints[waypointIndex].position);
        Debug.Log("Enemy State: [PATROL]");
    }

    public void Chase()
    {
        animator.Play("Run");
        SetDestination(target.position);
        Debug.Log("Enemy State: [CHASE]");
    }

    public void Attack()
    {
        FaceTarget();
        Destroy(PlayerManager.instance.player);
        Debug.Log("Enemy State: [ATTACK]");
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
