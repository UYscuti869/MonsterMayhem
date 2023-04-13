using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPoint : MonoBehaviour
{
    public Slider hp;
    public int maxHp;
    public SpawnManager spawnManager;
    public GameObject[] potions;
    private PlayerController player;
    private int currentHp = 0;
    private int HpUp = 0;
    private float potionSpawnChance = 0.3f;
    private Animator animator;
    private MoveEnemy moveEnemy;
    //private MoveEnemy moveEnemy;
    // Start is called before the first frame update
    void Start()
    {
        //moveEnemy = MoveEnemy.instance; <--처음에는 enemy가 없기 때문에 시작하자마자 값을 할당하면 null값이됨
        player = FindObjectOfType<PlayerController>();
        spawnManager = FindObjectOfType<SpawnManager>();
        hp.maxValue = maxHp;
        hp.value = 0;
        hp.fillRect.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        moveEnemy = GetComponent<MoveEnemy>();
    }
    void Update() 
    {
        if (player.hasHpPotion)
        {
            HpUp = 1;
            PlayerDamageInput(0,HpUp);
            HpUp = 0;
            player.hasHpPotion = false;
        }
    }
    public void EnemyDamageInput(int damage)
    {
        currentHp += damage;
        hp.fillRect.gameObject.SetActive(true);
        hp.value = currentHp;
        if (currentHp >= maxHp)
        {
            Destroy(gameObject);
            SpawnPotion();
        }
    }
    public void PlayerDamageInput(int damage,int HpUp)
    {
        currentHp += damage;
        currentHp -= HpUp;
        hp.fillRect.gameObject.SetActive(true);
        hp.value = currentHp;
        if (currentHp >= maxHp)
        {
            //싱글톤으로 해결하기
            MoveEnemy moveEnemy = MoveEnemy.instance;
            moveEnemy.GameOver();
        }
    }
    public void BossDamageInput(int damage)
    {
        currentHp += damage;
        hp.fillRect.gameObject.SetActive(true);
        hp.value = currentHp;
        if (currentHp >= maxHp)
        {
            spawnManager.portal.gameObject.SetActive(true);
            if (animator == null)
            {

            }
            else
            {
                animator.SetBool("Death", true);
            }
            moveEnemy.enemySpeed = 0;
            Destroy(gameObject,1.4f);
            SpawnPotion();
        }
    }
    void SpawnPotion()
    {
        if(Random.value < potionSpawnChance)
        {
            int index = Random.Range(0,potions.Length);
            Instantiate(potions[index], transform.position, potions[index].transform.rotation);
        }
    }
}
