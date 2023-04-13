using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawnManager : MonoBehaviour
{
    public GameObject potionToSpawn;
    public Transform spawnPoint;
    public float spawnInterval = 10;
    private float timer = 0;
    private GameObject spawnedPotion;
    // Start is called before the first frame update
    void Start()
    {
        spawnedPotion = GameObject.FindGameObjectWithTag("HP Potion");
        if (spawnedPotion != null)
        {
            StopTimer();
        }
        else
        {
            SpawnPotion();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedPotion == null)
        {
            StartTimer();
        }
        else
        {
            StopTimer();
        }
    }
    void SpawnPotion()
    {
        Instantiate(potionToSpawn,spawnPoint.position,potionToSpawn.transform.rotation);
        spawnedPotion = GameObject.FindGameObjectWithTag("HP Potion");
    }
    void StartTimer()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPotion();
            timer = 0;
        }
    }
    void StopTimer()
    {
        timer = 0;
    }
}
