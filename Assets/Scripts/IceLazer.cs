using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLazer : MonoBehaviour
{
    public float speed = 25;
    public int damage = 1;
    public AudioClip damageSound;
    private AudioSource damageAudio;
    private float iceEffect = 1f;
    private float colorValue = 1f;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        //damageAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (player.hasPowerPotion)
        {
            damage = 5;
        }
        else
        {
            damage = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Red Enemy"))
        {
            //damageAudio.PlayOneShot(damageSound);
            other.GetComponent<HitPoint>().EnemyDamageInput(damage);
            Color enemyColor = other.GetComponent<SpriteRenderer>().material.color;
            enemyColor.b += colorValue;
            other.GetComponent<SpriteRenderer>().material.color = enemyColor;
            if (other.GetComponent<MoveEnemy>().enemySpeed > 0)
            {
                other.GetComponent<MoveEnemy>().enemySpeed -= iceEffect;
                if (other.GetComponent<MoveEnemy>().enemySpeed <= 0)
                {
                    other.GetComponent<MoveEnemy>().enemySpeed = 0;
                }
            }
            else
            {
                other.GetComponent<MoveEnemy>().enemySpeed = 0;
            }

            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Boss") || other.gameObject.CompareTag("Red Boss"))
        {
            //damageAudio.PlayOneShot(damageSound);
            other.GetComponent<HitPoint>().BossDamageInput(damage);
            Color enemyColor = other.GetComponent<SpriteRenderer>().material.color;
            enemyColor.b += colorValue;
            other.GetComponent<SpriteRenderer>().material.color = enemyColor;
            if (other.GetComponent<MoveEnemy>().enemySpeed > 0)
            {
                other.GetComponent<MoveEnemy>().enemySpeed -= iceEffect;
                if (other.GetComponent<MoveEnemy>().enemySpeed <= 0)
                {
                    other.GetComponent<MoveEnemy>().enemySpeed = 0;
                }
            }
            else
            {
                other.GetComponent<MoveEnemy>().enemySpeed = 0;
            }
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Blue Enemy") || other.gameObject.CompareTag("Black Enemy"))
        {
            other.GetComponent<HitPoint>().EnemyDamageInput(damage);
            //damageAudio.PlayOneShot(damageSound);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Blue Boss"))
        {
            other.GetComponent<HitPoint>().BossDamageInput(damage);
            //damageAudio.PlayOneShot(damageSound);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Final Boss"))
        {
            other.GetComponent<HitPoint>().BossDamageInput(damage);
            Destroy(gameObject);
        }
    }
}
