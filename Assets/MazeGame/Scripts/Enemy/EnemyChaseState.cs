using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : IEnemyStateMachine
{


    public void Enter(EnemyBase enemy)
    {
        Debug.Log("Entering Chase State");
        enemy.agent.SetDestination(enemy.destination.transform.position);
        enemy.spotPlayer.Play();
        
    }

    public void Update(EnemyBase enemy)
    {

        // Transition back to Idle if player is out of range
        

        if (enemy.vision.playerSpotted == false && enemy.vision.playerInRange == false)
        {
            enemy.SetState(new EnemyPatrolState());
        }
        if (enemy.currentHealth <= 0)
        {
            enemy.SetState(new EnemyDeathState());
        }
    }

    public void Exit(EnemyBase enemy)
    {
        Debug.Log("Exiting Chase State");

    }
}
