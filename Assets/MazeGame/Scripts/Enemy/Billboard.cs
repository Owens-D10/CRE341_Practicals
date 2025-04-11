using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform _camera;

    private void Start()
    {
        _camera = GameObject.FindWithTag("MainCamera").transform;
    }

    private void LateUpdate()
    {
         transform.LookAt(transform.position + _camera.forward);
    }
}
