using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class EnemyBase : MonoBehaviour, IDamagable
{



    [Space(10)]
    [Header("Enemy State")]
    private IEnemyStateMachine currentState;     // Reference to the current state  
    public Transform target;              // Reference to the player or target  


 /*   [Space(10)]
    [Header("Enemy FX")]
    public GameObject dieEffectPrefab; // Reference to the die effect prefab  */

    
    public Rigidbody rb;
    
   public EnemyVision vision;
   
    public Transform centrePoint;
    public float range;

    public GameObject destination;
    public NavMeshAgent agent;
    public GameObject monster;
    public GameObject player;
    public PlayerStats stats;
    public Camera playerCamera;


    public int currentHealth;
    public int maxHealth = 1;

    
    public AudioSource spotPlayer;
    

    private void Start()
    {
        // Start with the Idle state
        SetState(new EnemyPatrolState());

        player = GameObject.FindWithTag("Player");

        stats = player.GetComponent<PlayerStats>();

        destination = player;

        centrePoint = GameObject.FindWithTag("CentrePoint").transform;

        agent = GetComponent<NavMeshAgent>();

        playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();


        currentHealth = maxHealth;

        
    }


    public void ShowHitEffect()
    {
        throw new System.NotImplementedException();
    }

    

    

    private void Update()
    {
        // Delegate behaviour to the current state
        currentState?.Update(this);

        
    }

    public void Destroy()
    {
        Destroy(monster);
        SceneManager.LoadScene("TitleScreen");
    }





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

    

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
