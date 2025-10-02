using UnityEngine;

public class AttackVerifier : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.TryGetComponent<Health>(out var health))
        {

            health.Damage(amount: 1);

            //health.Damage(amount: 1); //aqui é o valor que o Player da de Dano.
        }

    }
}
