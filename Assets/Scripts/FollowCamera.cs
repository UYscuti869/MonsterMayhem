using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private float minXRange = -28;
    private float maxXRange = 8;
    private float minYRange = -9;
    private float maxYRange = 29;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
    }

    void FixedUpdate()
    {
        InvisibleWall();
        Vector3 playerPosition = playerTransform.position;
        playerPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position,playerPosition,ref velocity,smoothTime);
    }
    void InvisibleWall()
    {
        if (transform.position.x >= maxXRange)
        {
            transform.position = new Vector3(maxXRange,transform.position.y,transform.position.z);
        }
        if (transform.position.x <= minXRange)
        {
            transform.position = new Vector3(minXRange,transform.position.y,transform.position.z);
        }
        if (transform.position.y >= maxYRange)
        {
            transform.position = new Vector3(transform.position.x,maxYRange,transform.position.z);
        }
        if (transform.position.y <= minYRange)
        {
            transform.position = new Vector3(transform.position.x,minYRange,transform.position.z);
        }
    }
}
