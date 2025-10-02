using System.Collections;
using TreeEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PernaDireita : MonoBehaviour
{
    public GameObject bossLimit;
    public GameObject SceneLimit;

    public GameObject player;
    public GameObject pEObj;
    private BossHP hp;
    private Collider2D pE, pD;
    private int pEStagger = 2;
    private Rigidbody2D pERig;
    private Rigidbody2D rb;
    private bool canAttack = true;
    private Health bossHP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossHP = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        pD = GetComponent<Collider2D>();
        pE = pEObj.GetComponent<Collider2D>();
        hp = new BossHP(15);
        pERig = pEObj.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(pE, pD);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (canAttack && player.transform.position.x > bossLimit.transform.position.x - 5 && player.transform.position.x < SceneLimit.transform.position.x - 5)
        {
            canAttack = false;
            StartCoroutine(BossAttack());
        }

    }

  
    IEnumerator BossAttack()
    {
        rb.linearVelocity = new Vector2(0, 15);
        

        yield return new WaitForSeconds(2f);
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        
        yield return new WaitForSeconds(Random.Range(0, 4));
        if (player.transform.position.x > bossLimit.transform.position.x - 5 && player.transform.position.x < SceneLimit.transform.position.x - 5)
        {
            transform.localPosition = new Vector3(player.transform.position.x, transform.localPosition.y, transform.localPosition.z);

        }
        rb.constraints = ~RigidbodyConstraints2D.FreezePositionY;

        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y - 15);
        
        pERig.linearVelocity = new Vector2(pEStagger, 0);
  
        yield return new WaitForSeconds(5f);
        pEStagger *= -1;
        canAttack = true;

    }
   

}
