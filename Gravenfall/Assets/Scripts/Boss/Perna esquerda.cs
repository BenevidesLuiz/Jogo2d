using UnityEngine;
using System.Collections;

public class PernaEsquerda : MonoBehaviour
{
    public GameObject player;
    

    private Rigidbody2D rb;
    private bool canAttack = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
  

    }

    // Update is called once per frame
    void Update()
    {
        
        

    }

   

}
