using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public int damage = 1;
    public PlayerStats stats;
    public Camera cam;
    public float range = 100f;
    public bool isFiring = false;
    public AudioSource gunshot;
    public AudioSource monsterHurt;
    

    [SerializeField] LayerMask target;
    [SerializeField] LayerMask obstruction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && stats.playerHasGun ==  true && isFiring == false)
        {
            Shoot();
            stats.muzzleFlash.SetActive(true);
            StartCoroutine(GunCooldown());
        } 
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, target)) // Hits and deals damage to enemies
        {
            Debug.Log("Hit Enemy");
            Debug.DrawRay(cam.transform.position, cam.transform.forward * hit.distance, Color.red);
            hit.collider.gameObject.GetComponent<EnemyBase>()?.TakeDamage(damage);
            monsterHurt.Play();
        }
        else if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, obstruction)) // Hits obstructions such as walls
        {
            Debug.Log("Hit Nothing");
            Debug.DrawRay(cam.transform.position, cam.transform.forward * hit.distance, Color.green);
        }

        gunshot.Play();
    }

    public IEnumerator GunCooldown()
    {
        isFiring = true;
        yield return new WaitForSeconds(5);
        isFiring = false;
        stats.gunCock.Play();
    }

}
