using UnityEngine;

public class CameraVerifier : MonoBehaviour
{
    public GameObject camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Player"))
        {
            if (camera.TryGetComponent<CameraFllow>(out var cameraFllow))
            {
                cameraFllow.TravarCamera();
            }
        }
        

    }
}
