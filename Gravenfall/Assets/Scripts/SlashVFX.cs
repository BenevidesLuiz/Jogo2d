using UnityEngine;

public class SlashVFX : MonoBehaviour
{

    private ParticleSystem vfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vfx = GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartVFX() {
        vfx.Play();
    }
    public void StopVFX() {
        vfx.Stop();
    }
}
