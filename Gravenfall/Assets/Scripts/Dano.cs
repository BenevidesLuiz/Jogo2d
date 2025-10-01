using UnityEngine;

public class Dano : MonoBehaviour{

    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.TryGetComponent<Health>(out var health))
        {

            health.Damage(amount: 1);

            //health.Damage(amount: 1); //aqui é o valor que o Player da de Dano.
        }
    }

}
