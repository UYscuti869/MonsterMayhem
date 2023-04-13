using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{
    public GameObject arrow;
    public Transform bowTransform;
    public AudioClip arrowSound;
    private AudioSource arrowAudio;
    private float offsetDegree = 180;
    private float rotZ;
    private float startDelay = 0.5f;
    private float spawnInterval = 0.5f;
    private void Start() 
    {
        arrowAudio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        MousePointing();
    }
    public void MousePointing()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shootDirection = worldMousePos - transform.position;
        rotZ = (Mathf.Atan2(shootDirection.y,shootDirection.x) * Mathf.Rad2Deg) + offsetDegree;
        transform.rotation = Quaternion.Euler(0,0,rotZ);
    }
    void Shooting()
    {
        float spawnRotation = rotZ - offsetDegree;
        Vector3 spawnPos = bowTransform.position;
        Instantiate(arrow,spawnPos,Quaternion.Euler(0,0,spawnRotation));
        arrowAudio.PlayOneShot(arrowSound);
    }
    void OnEnable() 
    {
        InvokeRepeating("Shooting",startDelay,spawnInterval);
    }
}
