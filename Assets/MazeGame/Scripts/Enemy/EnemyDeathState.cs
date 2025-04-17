using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeathState : IEnemyStateMachine
{


    public void Enter(EnemyBase enemy)
    {
        enemy.Destroy();
        
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
        Debug.Log("Exiting Chase State");

    }
}
