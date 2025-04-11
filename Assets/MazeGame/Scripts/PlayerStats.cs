using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class PlayerStats : MonoBehaviour
{
    public int collectables;
    public GameObject jumpscare;
    public bool inBoxRange;
    public GameObject weaponBox;
    public bool playerHasGun;
    public GameObject torch;
    public GameObject shotgun;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collectables = 0;
        weaponBox = GameObject.FindWithTag("WeaponBox");
        torch = GameObject.FindWithTag("Torch");
        shotgun = GameObject.FindWithTag("Shotgun");
        jumpscare = GameObject.FindWithTag("Jumpscare");

        shotgun.SetActive(false);
        jumpscare.SetActive(false);
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

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectables")
        {
            Destroy(other.gameObject);
            collectables = +1;
        }

        if (other.tag == "Monster")
        {
            JumpScare();
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

    private void JumpScare()
    {
        Time.timeScale = 0;
        jumpscare.SetActive(true);
    }
}
