using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerscript : MonoBehaviour

{
    public GameObject explosion;
    public PlayerHealthBar  playerHealthbar;
    public CoinCount coinCountScript;
    public GameController gameController;
    public AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip explosionSound;
    public AudioClip coinSound;
    public float speed = 10f;
    public float padding = 0.8f;

    float minX, maxX, minY, maxY;

     public float health = 20f;
     float barFillAmount = 1f;
     float damage = 0;

    void Start()
    {
        damage = barFillAmount / health;
        Camera cam = Camera.main;

        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 topRight   = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        minX = bottomLeft.x + padding;
        maxX = topRight.x - padding;
        minY = bottomLeft.y + padding;
        maxY = topRight.y - padding;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        float clampedX = Mathf.Clamp(transform.position.x + moveX, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y + moveY, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, 0f);
    }
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
            audioSource.PlayOneShot(damageSound,0.5f);
            DamagePlayerHealthbar();
            Destroy(collision.gameObject);
            if(health<=0)
            {
                AudioSource.PlayClipAtPoint(explosionSound,Camera.main.transform.position);
                gameController.gameOver();
                 Destroy(gameObject);
          GameObject blast = Instantiate(explosion,transform.position,Quaternion.identity);
          Destroy(blast,2f);
            }
           
        }
        if (collision.gameObject.tag=="Coin")
        {
            audioSource.PlayOneShot(coinSound,0.5f);
            Destroy(collision.gameObject);
            coinCountScript.AddCount();
        }
    }
    void DamagePlayerHealthbar()
    {
        if (health>0)
        {
            health -= 1;
            barFillAmount = barFillAmount - damage;
            playerHealthbar.SetAmount(barFillAmount);
        }
    }
}
