using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class PlayerStats : MonoBehaviour
{
    public int collectables;
    public GameObject jumpscare;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collectables = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    private void JumpScare()
    {
        Time.timeScale = 0;
        jumpscare.SetActive(true);
    }
}
