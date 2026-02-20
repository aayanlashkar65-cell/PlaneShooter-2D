using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{ 
    public Transform []gunPoint;
    public GameObject enemyBullet;
     public float bulletSpawnTime = 0.5f;
     public GameObject flash;
     public float speed = 1f;
     public GameObject enemyExplosionPrefab;
     public float health = 30f;
     public HealthBar healthbar;
     public GameObject coinPrefab;
     public AudioClip bulletSound;
     public AudioClip damageSound;
     public AudioClip explosionSound;
     public AudioSource audioSource;

     float barSize = 1f;
     float damage = 0;
    // Start is called before the first frame update
    void Start()
    {
        flash.SetActive(false);
    StartCoroutine(EnemyShoot());
    damage = barSize / health;

    healthbar.SetSize(1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down*speed*Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            audioSource.PlayOneShot(damageSound);
             DamageHealthbar();
             Destroy(collision.gameObject);

             if(health<=0)
            {
                AudioSource.PlayClipAtPoint(explosionSound,Camera.main.transform.position,0.5f);
                Instantiate(coinPrefab,transform.position,Quaternion.identity);
                Destroy(gameObject);
          GameObject enemyExplosion = Instantiate(enemyExplosionPrefab,transform.position,Quaternion.identity);
             Destroy(enemyExplosion,0.4f);
            }
             
        }
       
    }
     
        void DamageHealthbar()
    {
        if(health>0)
        {
            health -= 1;
           barSize = barSize - damage;
           healthbar.SetSize(barSize);
        }
    }
    void EnemyFire()
    {
        for (int i = 0; i<gunPoint.Length; i++)
        {
           Instantiate(enemyBullet,gunPoint[i].position,Quaternion.identity); 
        }
         /* Instantiate(enemyBullet,gunPoint1.position,Quaternion.identity);
          Instantiate(enemyBullet,gunPoint2.position,Quaternion.identity);*/
    }
    IEnumerator EnemyShoot()
    {
        while (true)
        {
             yield return new WaitForSeconds(bulletSpawnTime);
        EnemyFire();
        audioSource.PlayOneShot(bulletSound,0.5f);
        flash.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        flash.SetActive(false);
        }
    }
}
