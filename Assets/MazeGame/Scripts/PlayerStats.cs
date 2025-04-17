
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int collectables;
    public GameObject jumpscare;
    public bool inBoxRange;
    public GameObject weaponBox;
    public bool playerHasGun;
    public GameObject torch;
    public GameObject shotgun;
    public AudioSource jumpscareSound;
    public AudioSource groovy;
    public AudioSource gunCock;
    public AudioSource keyPickUp;
    public GameObject muzzleFlash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collectables = 0;
        weaponBox = GameObject.FindWithTag("WeaponBox");
        torch = GameObject.FindWithTag("Torch");
        shotgun = GameObject.FindWithTag("Shotgun");
        jumpscare = GameObject.FindWithTag("Jumpscare");
        muzzleFlash = GameObject.FindWithTag("MuzzleFlash");

        shotgun.SetActive(false);
        jumpscare.SetActive(false);
        muzzleFlash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetKeyDown(KeyCode.Space) && inBoxRange == true && collectables >= 4)
        {
            Object.Destroy(weaponBox);
            playerHasGun = true;
            torch.SetActive(false);
            shotgun.SetActive(true);
            gunCock.Play();
            groovy.Play();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectables")
        {
            Destroy(other.gameObject);
            collectables += 1;
            keyPickUp.Play();
        }

        if (other.tag == "Monster" && playerHasGun == false)
        {
           StartCoroutine(JumpScare());
        }

        if (other.tag == "WeaponBox")
        {
            inBoxRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WeaponBox")
        {
            inBoxRange = false;
        }
    }

    private IEnumerator JumpScare()
    {
        Time.timeScale = 0;
        jumpscare.SetActive(true);
        torch.SetActive(false);
        jumpscareSound.Play();
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("TitleScreen");

    }
}
