using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using System.Collections;

public class EnemyBase : MonoBehaviour
{



    [Space(10)]
    [Header("Enemy State")]
    private IEnemyStateMachine currentState;     // Reference to the current state  
    public Transform target;              // Reference to the player or target  


 /*   [Space(10)]
    [Header("Enemy FX")]
    public GameObject dieEffectPrefab; // Reference to the die effect prefab  */

    // From previous health script
   // public int currentHealth;
   // public int maxHealth = 3;
    //public bool dead;
   // public Animator animator;
    public Rigidbody rb;
    public Collider collider;
   public EnemyVision vision;
   // public AudioSource deathHowl;
   // public ParticleSystem bloodEffect;
    public Transform centrePoint;
    public float range;

    public GameObject destination;
    public NavMeshAgent agent;
    public GameObject monster;
    //public float attackCooldown = 1.5f;
    //public bool canAttack;
    //public int damage = 1;
    //public AudioSource attack;
   // public AudioSource hurt;

    private void Start()
    {
        // Start with the Idle state
        SetState(new EnemyPatrolState());

        // Find the player in the scene
       // currentHealth = maxHealth;

        agent = GetComponent<NavMeshAgent>();

       // canAttack = true;
    }


    public void ShowHitEffect()
    {
        throw new System.NotImplementedException();
    }

    

    

    private void Update()
    {
        // Delegate behaviour to the current state
        currentState?.Update(this);

       /* if (currentHealth <= 0)
        {
            Die();
        }*/
    }

   /* void Die()
    {
        animator.SetTrigger("Dying");
        dead = true;
        rb.useGravity = false;
        box.enabled = false;
        agent.enabled = false;
        //vision.enabled = false;



    }*/

    

    

    public void SetState(IEnemyStateMachine newState)
    {
        // Exit the current state and enter the new state
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
    }

    public string GetCurrentStateName()
    {
        if (currentState != null)
        {
            string stateName = currentState.GetType().Name;
            return stateName.Replace("Enemy", "");
        }
        return "No State";
    }

    

    

    


    
}
