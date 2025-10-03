using System.Collections;
using UnityEngine;

public class CameraVerifier : MonoBehaviour
{
    public PernaDireita pernaDireita;
    public GameObject cameraObj;
    private bool wasCalled = false;
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
            if (cameraObj.TryGetComponent<CameraFllow>(out var cameraFllow))
            {
                cameraFllow.TravarCamera();
                if (!wasCalled) { 
                    StartCoroutine(StartBossFight());
                }

            }
        }
        
    }

    IEnumerator StartBossFight()
    {
        yield return new WaitForSeconds(1f);
        wasCalled = true;
        pernaDireita.canStartAttack();
    }
}
