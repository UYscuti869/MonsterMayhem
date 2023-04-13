using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject[] blueEnemies;
    public GameObject[] RedEnemies;
    public GameObject[] blackEnemies;
    public GameObject boss;
    public GameObject portal;
    public GameObject blinkText;
    private Timer timer;
    private float topSpawnYRange = 34.0f;
    private float bottomSpawnYRange = -14.0f;
    private float leftSpawnXRange = -38.0f;
    private float rightSpawnXRange = 18.0f;
    public float enemySpawnInterval = 6.0f;
    public float blueEnemySpawnInterval = 4.0f;
    public float redEnemySpawnInterval = 4.0f;
    public float blackEnemySpawnInterval = 4.0f;
    private int startDelay = 2;
    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        //portal = GameObject.FindGameObjectWithTag("Portal");
        InvokeRepeating("SpawnTopRandomEnemy",startDelay,enemySpawnInterval);
        InvokeRepeating("SpawnBottomRandomEnemy",startDelay,enemySpawnInterval);
        if (SceneManager.GetActiveScene().name == "Medium Scene" || SceneManager.GetActiveScene().name == "Hard Scene" || SceneManager.GetActiveScene().name == "Impossible Scene")
        {
            InvokeRepeating("SpawnTopRandomBlueEnemy",startDelay,blueEnemySpawnInterval);
            InvokeRepeating("SpawnBottomRandomBlueEnemy",startDelay,blueEnemySpawnInterval);
        }
        if (SceneManager.GetActiveScene().name == "Hard Scene" || SceneManager.GetActiveScene().name == "Impossible Scene")
        {
            InvokeRepeating("SpawnTopRandomRedEnemy",startDelay,redEnemySpawnInterval);
            InvokeRepeating("SpawnBottomRandomRedEnemy",startDelay,redEnemySpawnInterval);
        }
        if (SceneManager.GetActiveScene().name == "Impossible Scene")
        {
            InvokeRepeating("SpawnTopRandomBlackEnemy",startDelay,blackEnemySpawnInterval);
            InvokeRepeating("SpawnBottomRandomBlackEnemy",startDelay,blackEnemySpawnInterval);
        }
    }

    // Update is called once per frame
    void Update()
    {
    if (timer.timeIsOver)
        {
            timer.timeIsOver = false;
            SpawnBoss();
            GameObject.FindObjectOfType<Timer>().enabled = false;
            blinkText.SetActive(true);
            GameObject.Find("Timer").SetActive(false); 
            StartCoroutine(ActiveBlinkText());
        }
    }
    void SpawnTopRandomEnemy()
    {
        int enemyIndex = Random.Range(0,Enemies.Length);
        Vector2 spawnRange = new Vector2(Random.Range(leftSpawnXRange,rightSpawnXRange),topSpawnYRange);
        Instantiate(Enemies[enemyIndex],spawnRange,Enemies[enemyIndex].transform.rotation);
    }
    void SpawnBottomRandomEnemy()
    {
        int enemyIndex = Random.Range(0,Enemies.Length);
        Vector2 spawnRange = new Vector2(Random.Range(leftSpawnXRange,rightSpawnXRange),bottomSpawnYRange);
        Instantiate(Enemies[enemyIndex],spawnRange,Enemies[enemyIndex].transform.rotation);
    }
    void SpawnTopRandomBlueEnemy()
    {
        int enemyIndex = Random.Range(0,blueEnemies.Length);
        Vector2 spawnRange = new Vector2(Random.Range(leftSpawnXRange,rightSpawnXRange),topSpawnYRange);
        Instantiate(blueEnemies[enemyIndex],spawnRange,blueEnemies[enemyIndex].transform.rotation);
    }
    void SpawnBottomRandomBlueEnemy()
    {
        int enemyIndex = Random.Range(0,blueEnemies.Length);
        Vector2 spawnRange = new Vector2(Random.Range(leftSpawnXRange,rightSpawnXRange),bottomSpawnYRange);
        Instantiate(blueEnemies[enemyIndex],spawnRange,blueEnemies[enemyIndex].transform.rotation);
    }
    void SpawnTopRandomRedEnemy()
    {
        int enemyIndex = Random.Range(0,RedEnemies.Length);
        Vector2 spawnRange = new Vector2(Random.Range(leftSpawnXRange,rightSpawnXRange),topSpawnYRange);
        Instantiate(RedEnemies[enemyIndex],spawnRange,RedEnemies[enemyIndex].transform.rotation);
    }
    void SpawnBottomRandomRedEnemy()
    {
        int enemyIndex = Random.Range(0,RedEnemies.Length);
        Vector2 spawnRange = new Vector2(Random.Range(leftSpawnXRange,rightSpawnXRange),bottomSpawnYRange);
        Instantiate(RedEnemies[enemyIndex],spawnRange,RedEnemies[enemyIndex].transform.rotation);
    }
    void SpawnTopRandomBlackEnemy()
    {
        int enemyIndex = Random.Range(0,blackEnemies.Length);
        Vector2 spawnRange = new Vector2(Random.Range(leftSpawnXRange,rightSpawnXRange),topSpawnYRange);
        Instantiate(blackEnemies[enemyIndex],spawnRange,blackEnemies[enemyIndex].transform.rotation);
    }
    void SpawnBottomRandomBlackEnemy()
    {
        int enemyIndex = Random.Range(0,blackEnemies.Length);
        Vector2 spawnRange = new Vector2(Random.Range(leftSpawnXRange,rightSpawnXRange),bottomSpawnYRange);
        Instantiate(blackEnemies[enemyIndex],spawnRange,blackEnemies[enemyIndex].transform.rotation);
    }
    public void SpawnBoss()
    {
        Vector3 bossSpawnPosition = new Vector3(0,0,0);
        Instantiate(boss,bossSpawnPosition,boss.transform.rotation);
    }
    IEnumerator ActiveBlinkText()
    {
        yield return new WaitForSeconds(5);
        blinkText.SetActive(false);
    }
}
