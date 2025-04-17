using UnityEngine;

public class ObjectSpawning : MonoBehaviour
{
    public Transform keySpawnPoint1;
    public Transform keySpawnPoint2;
    public Transform keySpawnPoint3;
    public Transform keySpawnPoint4;
    public GameObject key;
    public Transform playerSpawnPoint;
    public GameObject player;
    public int monsterSpawn;
    public GameObject monster;
    public Transform gunBoxSpawnPoint;
    public GameObject gunBox;
    
    void Start()
    {
        monsterSpawn = Random.Range(1, 5);

        Instantiate(key, keySpawnPoint1);
        Instantiate(key, keySpawnPoint2);
        Instantiate(key, keySpawnPoint3);
        Instantiate(key, keySpawnPoint4);

        playerSpawnPoint = GameObject.FindWithTag("PlayerSpawn").transform;
        Instantiate(player, playerSpawnPoint);

        if(monsterSpawn == 1)
        {
            Instantiate(monster, keySpawnPoint1);
        }
        if(monsterSpawn == 2)
        {
            Instantiate(monster, keySpawnPoint2);
        }
        if(monsterSpawn == 3)
        {
            Instantiate(monster, keySpawnPoint3);
        }
        if(monsterSpawn == 4)
        {
            Instantiate(monster, keySpawnPoint4);
        }

        gunBoxSpawnPoint = GameObject.FindWithTag("GunBoxSpawn").transform;
        Instantiate (gunBox, gunBoxSpawnPoint);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
