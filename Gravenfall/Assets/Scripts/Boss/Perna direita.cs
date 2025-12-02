using System.Collections;
using UnityEngine;

public class PernaDireita : MonoBehaviour
{
    private SpriteRenderer bossSprite;
    public ControlerMusica controllerMusica;
    public GameObject bossLimit;
    public GameObject SceneLimit;
    public float bossAttackForce = 25;
    public GameObject player;
    public GameObject pEObj;
    private Collider2D pE, pD;
    private int pEStagger = 2;
    private Rigidbody2D pERig;
    private Rigidbody2D rb;
    private bool canAttack = false;



    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("Ground")) {
            controllerMusica.HitGround();
        }

    }


    void Start()
    {
        bossSprite = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
        pD = GetComponent<Collider2D>();
        pE = pEObj.GetComponent<Collider2D>();
        pERig = pEObj.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(pE, pD);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (canAttack && player.transform.position.x > bossLimit.transform.position.x && player.transform.position.x < SceneLimit.transform.position.x)
        {
            canAttack = false;
            StartCoroutine(BossAttack());
        }

    }
    public void IncreaseBossForce() {
        bossAttackForce += 2f;
    }
    public void canStartAttack() {
        canAttack = true;
    }
    public void ReceiveDamage()
    {
        StartCoroutine(HitAnimation());
    }
    IEnumerator HitAnimation()
    {
        float duration = 2f;
        float elapsed = 0f;
        float blinkSpeed = 5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float alpha = Mathf.Lerp(0.3f, 1f, Mathf.PingPong(Time.time * blinkSpeed, 1f));

            Color c = bossSprite.color;
            c.a = alpha;
            bossSprite.color = c;

            yield return null;
        }

        Color finalColor = bossSprite.color;
        finalColor.a = 1f;
        bossSprite.color = finalColor;
    }
    IEnumerator BossAttack()
    {
        rb.linearVelocity = new Vector2(0, 20);
        

        yield return new WaitForSeconds(2f);
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        
        yield return new WaitForSeconds(Random.Range(0, 4));
        if (player.transform.position.x > bossLimit.transform.position.x && player.transform.position.x < SceneLimit.transform.position.x)
        {
            transform.localPosition = new Vector3(player.transform.position.x, transform.localPosition.y, transform.localPosition.z);

        }
        rb.constraints = ~RigidbodyConstraints2D.FreezePositionY;

        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y - bossAttackForce);
        
        pERig.linearVelocity = new Vector2(pEStagger, 0);
  
        yield return new WaitForSeconds(5f);
        pEStagger *= -1;
        canAttack = true;

    }
   

}
