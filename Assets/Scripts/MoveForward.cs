using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 20;
    public int damage = 2;
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
            damage = 2;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Blue Enemy") || other.gameObject.CompareTag("Red Enemy") || other.gameObject.CompareTag("Black Enemy"))
        {
            other.GetComponent<HitPoint>().EnemyDamageInput(damage);
            //damageAudio.PlayOneShot(damageSound);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Boss") || other.gameObject.CompareTag("Blue Boss") || other.gameObject.CompareTag("Red Boss"))
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
