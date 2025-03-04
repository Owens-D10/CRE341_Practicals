using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int collectables;

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
    }
}
