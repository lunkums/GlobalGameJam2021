using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviors : EnemyController
{
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            if (distance <= agent.stoppingDistance)
            {
                state = State.ATTACK;
            }
            else
            {
                state = State.CHASE;
            }
        }
        else
        {
            state = State.PATROL;

            if (Vector3.Distance(waypoints[waypointIndex].position, transform.position) <= agent.stoppingDistance)
            {
                IncreaseIndex();
            }
        }

        switch (state)
        {
            case State.IDLE:
                Idle();
                break;
            case State.PATROL:
                Patrol();
                break;
            case State.CHASE:
                Chase();
                break;
            case State.ATTACK:
                Attack();
                break;
            default:
                // Do nothing
                break;
        }
    }
}
