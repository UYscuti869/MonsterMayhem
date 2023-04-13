using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShoot : MonoBehaviour
{
    public GameObject iceLazer;
    public Transform gunTransform;
    public AudioClip lazerSound;
    private AudioSource lazerAudio;
    private float offsetDegree = 180;
    private float rotZ;
    private float startDelay = 0.5f;
    private float spawnInterval = 1;
    private void Start() 
    {
        lazerAudio = GetComponent<AudioSource>();  
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
        Vector3 spawnPos = gunTransform.position;
        Instantiate(iceLazer,spawnPos,Quaternion.Euler(0,0,spawnRotation));
        lazerAudio.PlayOneShot(lazerSound);
    }
    void OnEnable() 
    {
        InvokeRepeating("Shooting",startDelay,spawnInterval);
    }
}
