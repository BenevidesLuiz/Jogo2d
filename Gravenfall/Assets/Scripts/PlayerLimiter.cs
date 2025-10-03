using UnityEngine;

public class PlayerLimiter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int stopMovement = 0;
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
            if (col.gameObject.TryGetComponent<Player>(out var player))
            {
                player.stopMovement(stopMovement);
                player.CancelDash();
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (col.gameObject.TryGetComponent<Player>(out var player))
            {
                player.stopMovement(0);
            }
        }
    }
}
