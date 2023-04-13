using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveEnemy : MonoBehaviour
{
    public float enemySpeed;
    public float detectionRadius = 3f;
    private float colorValue = 0.5f;
    private float toxicTime = 1;
    private int toxicdamage = 1;
    private Color currentColor;
    private Vector3 followingDirection;
    private Transform playerTransform;
    private static MoveEnemy _instance;
    private int buff = 1;
    private Animator animator;
    public static MoveEnemy instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MoveEnemy>();
            }
            return _instance;
        }
    }
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }
    void Awake() 
    {
        //if (instance != null && instance != this)
        //{
           // Destroy(this.gameObject);
        //}
        //else
        //{
            //instance = this;
            //DontDestroyOnLoad(this.gameObject);
        //}
        //__________
        //if (_instance == null)
        //{
            //_instance = this;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        MovingEnemy();
        AttackAnimi();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<HitPoint>().PlayerDamageInput(1,0);
        }
        if (other.gameObject.CompareTag("Poison Lazer") && (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Blue Enemy")))
        {
            currentColor = GetComponent<SpriteRenderer>().material.color;
            currentColor.g += colorValue;
            GetComponent<SpriteRenderer>().material.color = currentColor;
            StartCoroutine(ToxicTimer());
        }
        if (other.gameObject.CompareTag("Poison Lazer") && (gameObject.CompareTag("Boss") || gameObject.CompareTag("Blue Boss")))
        {
            currentColor = GetComponent<SpriteRenderer>().material.color;
            currentColor.g += colorValue;
            GetComponent<SpriteRenderer>().material.color = currentColor;
            StartCoroutine(BossToxicTimer());
        }
        if (other.gameObject.CompareTag("Ice Lazer") && gameObject.CompareTag("Red Enemy"))
        {
            GetComponent<HitPoint>().EnemyDamageInput(buff);
        }
        if (other.gameObject.CompareTag("Ice Lazer") && gameObject.CompareTag("Red Boss"))
        {
            GetComponent<HitPoint>().BossDamageInput(buff);
        }
        if (other.gameObject.CompareTag("Bullet") && gameObject.CompareTag("Final Boss"))
        {
            animator.SetTrigger("Hurt");
        }
        if (other.gameObject.CompareTag("Arrow") && gameObject.CompareTag("Final Boss"))
        {
            animator.SetTrigger("Hurt");
        }
        if (other.gameObject.CompareTag("Ice Lazer") && gameObject.CompareTag("Final Boss"))
        {
            animator.SetTrigger("Hurt");
        }
        if (other.gameObject.CompareTag("Poison Lazer") && gameObject.CompareTag("Final Boss"))
        {
            animator.SetTrigger("Hurt");
        }
    }
    void MovingEnemy()
    {
        
        followingDirection = (playerTransform.position - transform.position).normalized;
        float horizontalMovement = followingDirection.x;
        transform.Translate(followingDirection * enemySpeed * Time.deltaTime);
        if (horizontalMovement < 0)
        {
            Vector3 currentScale = transform.localScale;
            if (currentScale.x < 0)
            {
                currentScale.x = -currentScale.x;
            }
            transform.localScale = currentScale;
        }
        else if (horizontalMovement > 0)
        {
            Vector3 currentScale = transform.localScale;
            if (currentScale.x > 0)
            {
                currentScale.x = -currentScale.x;
            }
            transform.localScale = currentScale;
        }
        
        
    }
    public void GameOver()
    {
        SceneManager.LoadScene("Game Over",LoadSceneMode.Single);  
    }
    IEnumerator ToxicTimer()
    {
        yield return new WaitForSeconds(toxicTime);
        GetComponent<HitPoint>().EnemyDamageInput(toxicdamage);
        yield return new WaitForSeconds(toxicTime);
        GetComponent<HitPoint>().EnemyDamageInput(toxicdamage);
        yield return new WaitForSeconds(toxicTime);
        currentColor.g -= colorValue;
        GetComponent<SpriteRenderer>().material.color = currentColor;
    }
    IEnumerator BossToxicTimer()
    {
        yield return new WaitForSeconds(toxicTime);
        GetComponent<HitPoint>().BossDamageInput(toxicdamage);
        yield return new WaitForSeconds(toxicTime);
        GetComponent<HitPoint>().BossDamageInput(toxicdamage);
        yield return new WaitForSeconds(toxicTime);
        currentColor.g -= colorValue;
        GetComponent<SpriteRenderer>().material.color = currentColor;
    }
    void AttackAnimi()
    {
        float distance = Vector3.Distance(playerTransform.position,transform.position);

        if (animator == null)
        {

        }
        else
        {
            if (distance <= detectionRadius)
            {
                animator.SetTrigger("Attack");
            }
        }
        
    }
}
