using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonLazer : MonoBehaviour
{
    public float speed = 25;
    public int damage = 1;
    public AudioClip damageSound;
    private AudioSource damageAudio;
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
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Blue Enemy") || other.gameObject.CompareTag("Red Enemy") || other.gameObject.CompareTag("Black Enemy"))
        {
            //damageAudio.PlayOneShot(damageSound);
            other.GetComponent<HitPoint>().EnemyDamageInput(damage);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Boss") || other.gameObject.CompareTag("Blue Boss") || gameObject.CompareTag("Red Boss"))
        {
            //damageAudio.PlayOneShot(damageSound);
            other.GetComponent<HitPoint>().BossDamageInput(damage);
            Destroy(gameObject); 
        }
        if (other.gameObject.CompareTag("Final Boss"))
        {
            other.GetComponent<HitPoint>().BossDamageInput(damage);
            Destroy(gameObject);
        }
    }
}
