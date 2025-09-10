using Pathfinding;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    private AIPath aiPath;
    private Animator animator;
    private Transform player;

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Player precisa da tag "Player"
    }

    void Update()
    {
        if (player == null) return;

        // Sempre define o destino do inimigo como a posição do player
        aiPath.destination = player.position;

        // Animação de corrida (se estiver realmente se movendo)
        bool isMoving = aiPath.desiredVelocity.magnitude > 0.1f;
        animator.SetBool("running", isMoving);

        // Flip do sprite para olhar na direção do movimento
        if (aiPath.desiredVelocity.x > 0.05f)
        {
            transform.localScale = new Vector3(2, 2, 2);
        }
        else if (aiPath.desiredVelocity.x < -0.05f)
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }
    }
}
