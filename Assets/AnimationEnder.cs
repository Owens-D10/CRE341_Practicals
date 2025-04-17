using UnityEngine;

public class AnimationEnder : MonoBehaviour
{
    public GameObject MuzzleFlash;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MuzzleFlashEnd()
    {
        MuzzleFlash.SetActive(false);
    }
}
