using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    protected Transform target;
    protected NavMeshAgent agent;
    protected State state;

    public enum State
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK
    }

    public void Idle()
    {
        Debug.Log("Enemy State: [IDLE]");
    }

    public void Patrol()
    {
        Debug.Log("Enemy State: [PATROL]");
    }

    public void Chase()
    {
        agent.SetDestination(target.position);
        Debug.Log("Enemy State: [CHASE]");
    }

    public void Attack()
    {
        Debug.Log("Enemy State: [ATTACK]");
        FaceTarget();
        Destroy(PlayerManager.instance.player);
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
}
