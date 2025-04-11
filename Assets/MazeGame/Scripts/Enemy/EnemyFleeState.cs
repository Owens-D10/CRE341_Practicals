using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFleeState : IEnemyStateMachine
{


    public void Enter(EnemyBase enemy)
    {
        Debug.Log("Entering Flee State");
        enemy.agent.SetDestination(enemy.destination.transform.position);

    }

    public void Update(EnemyBase enemy)
    {

        // Transition back to Idle if player is out of range


        if (enemy.vision.playerSpotted == false && enemy.vision.playerInRange == false)
        {
            enemy.SetState(new EnemyPatrolState());
        }
    }

    public void Exit(EnemyBase enemy)
    {
        Debug.Log("Exiting Flee State");

    }
}
