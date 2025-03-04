using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyVision : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;
    public GameObject player;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool playerSpotted;
    public bool playerInRange;
    public float inRangeRadius;
    [Range(0, 360)]
    public float attackAngle;



    void Update()
    {
        FieldOfViewCheck();
        InRangeCheck();
        
    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    playerSpotted = true;
                }
                else
                {
                    playerSpotted = false;
                }
            }
            else
            {
                playerSpotted = false;
            }


        }
        else if (playerSpotted == true)
        {
            playerSpotted = false;
        }
    }

    private void InRangeCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, inRangeRadius, targetMask);

        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < attackAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    playerInRange = true;
                }
                else
                {
                    playerInRange = false;
                }
            }
            else
            {
                playerInRange = false;
            }

        }
        else if (playerInRange == true)
        {
            playerInRange = false;
        }
    }

}





