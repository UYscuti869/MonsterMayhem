using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 200;
    public int maxEnemyCount = 6;
    public int maxBlueEnemyCount = 6;
    public int maxRedEnemyCount = 5;
    public int maxBlackEnemyCount = 4;
    public bool hasSpeedPotion;
    public bool hasPowerPotion;
    public bool hasHpPotion;
    public bool hasAttackSpeedPotion;
    public bool hasDefencePotion;
    public GameObject shield;
    public GameObject gun;
    public GameObject bow;
    public GameObject iceGun;
    public GameObject poisonGun;
    public GameObject pauseScreen;
    public AudioClip potionSound;
    private AudioSource potionAudio;
    private Rigidbody2D playerRb;
    //private Shoot shoot;
    private float topYRange = 33.0f;
    private float bottomYRange = -14;
    private float xRangeLeft = -37;
    private float xRangeRight = 17;
    private float speedBoost = 2;
    private float coolDown = 10;
    private float startDelay = 0.5f;
    private float spawnInterval = 1;
    private float arrowSpawnInterval = 0.5f;
    private float speedUpInterval = 0.5f;
    private float arrowSpeedUpInterval = 0.25f;
    private int checkInterval = 1;
    private bool isPaused;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        potionAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(CheckAndDestroyEnemies());
        //shoot = FindObjectOfType<Shoot>();
    }

    // Update is called once per frame
    void Update() 
    {
       ChangeGun();
       if (Input.GetKeyDown(KeyCode.Escape))
       {
        CheckForPause();
       }
       PlayerAnimation();
    }
    void FixedUpdate()
    {
        InvisibleWall();
        MovePlayer();
    }
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontalInput,verticalInput);

         if (!hasSpeedPotion)
        {
            playerRb.velocity = moveSpeed * movement * Time.deltaTime;
        }
        else 
        {
            playerRb.velocity = moveSpeed * movement * speedBoost * Time.deltaTime;
        }
    }
    void InvisibleWall()
    {
        if (transform.position.y >= topYRange)
        {
            transform.position = new Vector2(transform.position.x,topYRange);
        }
        if (transform.position.y <= bottomYRange)
        {
            transform.position = new Vector2(transform.position.x,bottomYRange);
        }
        if (transform.position.x >= xRangeRight)
        {
            transform.position = new Vector2(xRangeRight,transform.position.y);
        }
        if (transform.position.x <= xRangeLeft)
        {
            transform.position = new Vector2(xRangeLeft,transform.position.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Speed Potion"))
        {
            hasSpeedPotion = true;
            Destroy(other.gameObject);
            potionAudio.PlayOneShot(potionSound);
            StartCoroutine(GreenCoolDownTimer());
        }
        if (other.gameObject.CompareTag("Power Potion"))
        {
            hasPowerPotion = true;
            Destroy(other.gameObject);
            potionAudio.PlayOneShot(potionSound);
            StartCoroutine(PurpleCoolDownTimer());
        }
        if (other.gameObject.CompareTag("HP Potion"))
        {
            hasHpPotion = true;
            Destroy(other.gameObject);
            potionAudio.PlayOneShot(potionSound);
        }
        if (other.gameObject.CompareTag("Attack Speed Potion"))
        {
            hasAttackSpeedPotion = true;
            Destroy(other.gameObject);
            potionAudio.PlayOneShot(potionSound);
            if (hasAttackSpeedPotion && gun.activeInHierarchy)
            {
                gun.GetComponent<Shoot>().CancelInvoke("Shooting");
                gun.GetComponent<Shoot>().InvokeRepeating("Shooting", startDelay, speedUpInterval);
            }
            if (hasAttackSpeedPotion && bow.activeInHierarchy)
            {
                bow.GetComponent<SpawnArrow>().CancelInvoke("Shooting");
                bow.GetComponent<SpawnArrow>().InvokeRepeating("Shooting",startDelay,arrowSpeedUpInterval);
            }
            if (hasAttackSpeedPotion && iceGun.activeInHierarchy)
            {
                iceGun.GetComponent<IceShoot>().CancelInvoke("Shooting");
                iceGun.GetComponent<IceShoot>().InvokeRepeating("Shooting",startDelay,speedUpInterval);
            }
            if (hasAttackSpeedPotion && poisonGun.activeInHierarchy)
            {
                poisonGun.GetComponent<PoisonShoot>().CancelInvoke("Shooting");
                poisonGun.GetComponent<PoisonShoot>().InvokeRepeating("Shooting",startDelay,speedUpInterval);
            }
            StartCoroutine(WhiteCoolDownTimer());
        }
        if (other.gameObject.CompareTag("Defence Potion"))
        {
            hasDefencePotion = true;
            Destroy(other.gameObject);
            potionAudio.PlayOneShot(potionSound);
            shield.SetActive(true);
            StartCoroutine(BlueCoolDownTimer());
        }
        if (other.gameObject.CompareTag("Portal"))
        {
            SceneManager.LoadScene("Start Menu",LoadSceneMode.Single);
        }
    }
    IEnumerator WhiteCoolDownTimer()
    {
        yield return new WaitForSeconds(coolDown);
        
        
        if (hasAttackSpeedPotion)
        {
            hasAttackSpeedPotion = false;
            if (!hasAttackSpeedPotion && gun.activeSelf)
            {
                gun.GetComponent<Shoot>().CancelInvoke("Shooting");
                gun.GetComponent<Shoot>().InvokeRepeating("Shooting",startDelay,spawnInterval);
            }
            if (!hasAttackSpeedPotion && bow.activeSelf)
            {
                bow.GetComponent<SpawnArrow>().CancelInvoke("Shooting");
                bow.GetComponent<SpawnArrow>().InvokeRepeating("Shooting",startDelay,arrowSpawnInterval);
            }
            if (!hasAttackSpeedPotion && iceGun.activeSelf)
            {
                iceGun.GetComponent<IceShoot>().CancelInvoke("Shooting");
                iceGun.GetComponent<IceShoot>().InvokeRepeating("Shooting",startDelay,spawnInterval);
            }
            if (!hasAttackSpeedPotion && poisonGun.activeSelf)
            {
                poisonGun.GetComponent<PoisonShoot>().CancelInvoke("Shooting");
                poisonGun.GetComponent<PoisonShoot>().InvokeRepeating("Shooting",startDelay,spawnInterval);
            }
        }
        
    }
    IEnumerator BlueCoolDownTimer()
    {
        yield return new WaitForSeconds(coolDown);
        if (hasDefencePotion)
        {
            hasDefencePotion = false;
            shield.SetActive(false);
        }
    }
    IEnumerator GreenCoolDownTimer()
    {
        yield return new WaitForSeconds(coolDown);
        if (hasSpeedPotion)
        {
            hasSpeedPotion = false;
        }
    }
    IEnumerator PurpleCoolDownTimer()
    {
        yield return new WaitForSeconds(coolDown);
        if (hasPowerPotion)
        {
            hasPowerPotion = false;
        }
    }
    IEnumerator CheckAndDestroyEnemies()
    {
        while(true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] blueEnemies = GameObject.FindGameObjectsWithTag("Blue Enemy");
            GameObject[] redEnemies = GameObject.FindGameObjectsWithTag("Red Enemy");
            GameObject[] blackEnemies = GameObject.FindGameObjectsWithTag("Black Enemy");

            if (enemies.Length > maxEnemyCount)
            {
                int enemiesToDelete = enemies.Length - maxEnemyCount;
                for (int i = 0; i < enemiesToDelete; i++)
                {
                    Destroy(enemies[i]);
                }
            }
            if (blueEnemies.Length > maxBlueEnemyCount)
            {
                int enemiesToDelete = blueEnemies.Length - maxBlueEnemyCount;
                for (int i = 0; i < enemiesToDelete; i++)
                {
                    Destroy(blueEnemies[i]);
                }
            } 
            if (redEnemies.Length > maxRedEnemyCount)
            {
                int enemiesToDelete = redEnemies.Length - maxRedEnemyCount;
                for (int i = 0; i < enemiesToDelete; i++)
                {
                    Destroy(redEnemies[i]);
                }
            } 
            if (blackEnemies.Length > maxBlackEnemyCount)
            {
                int enemiesToDelete = blackEnemies.Length - maxBlackEnemyCount;
                for (int i = 0; i < enemiesToDelete; i++)
                {
                    Destroy(blackEnemies[i]);
                }
            } 
            yield return new WaitForSeconds(checkInterval);
        }
    }
    void ChangeGun()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
       {
        gun.SetActive(true);
        bow.SetActive(false);
        bow.GetComponent<SpawnArrow>().CancelInvoke("Shooting");
        iceGun.SetActive(false);
        iceGun.GetComponent<IceShoot>().CancelInvoke("Shooting");
        poisonGun.SetActive(false);
        poisonGun.GetComponent<PoisonShoot>().CancelInvoke("Shooting");
       }
       if (Input.GetKeyDown(KeyCode.Alpha2))
       {
        bow.SetActive(true);
        gun.SetActive(false);
        gun.GetComponent<Shoot>().CancelInvoke("Shooting");
        iceGun.SetActive(false);
        iceGun.GetComponent<IceShoot>().CancelInvoke("Shooting");
        poisonGun.SetActive(false);
        poisonGun.GetComponent<PoisonShoot>().CancelInvoke("Shooting");
       }
       if (Input.GetKeyDown(KeyCode.Alpha3))
       {
        iceGun.SetActive(true);
        bow.SetActive(false);
        bow.GetComponent<SpawnArrow>().CancelInvoke("Shooting");
        gun.SetActive(false);
        gun.GetComponent<Shoot>().CancelInvoke("Shooting");
        poisonGun.SetActive(false);
        poisonGun.GetComponent<PoisonShoot>().CancelInvoke("Shooting");
       }
       if (Input.GetKeyDown(KeyCode.Alpha4))
       {
        poisonGun.SetActive(true);
        bow.SetActive(false);
        bow.GetComponent<SpawnArrow>().CancelInvoke("Shooting");
        gun.SetActive(false);
        gun.GetComponent<Shoot>().CancelInvoke("Shooting");
        iceGun.SetActive(false);
        iceGun.GetComponent<IceShoot>().CancelInvoke("Shooting");
       }
    }
    void CheckForPause()
    {
        if (!isPaused)
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
    void PlayerAnimation()
    {
        if (Input.GetKeyDown(KeyCode.W))
       {
        animator.SetBool("WPressed", true);
       }
       else if (Input.GetKeyUp(KeyCode.W))
       {
        animator.SetBool("WPressed", false);
       }
       if (Input.GetKeyDown(KeyCode.S))
       {
        animator.SetBool("SPressed", true);
       }
       else if (Input.GetKeyUp(KeyCode.S))
       {
        animator.SetBool("SPressed", false);
       }
       if (Input.GetKeyDown(KeyCode.A))
       {
        spriteRenderer.flipX = false;
        animator.SetBool("APressed", true);
       }
       else if (Input.GetKeyUp(KeyCode.A))
       {
        animator.SetBool("APressed", false);
       }
       if (Input.GetKeyDown(KeyCode.D))
       {
        spriteRenderer.flipX = true;
        //transform.localScale = new Vector3(-1,1,1);
        animator.SetBool("DPressed", true);
        
       }
       else if (Input.GetKeyUp(KeyCode.D))
       {
        animator.SetBool("DPressed", false);
        //transform.localScale = new Vector3(1,1,1);
       }
    }
}