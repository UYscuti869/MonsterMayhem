using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBound : MonoBehaviour
{
    private float topYBound = 34.0f;
    private float bottomYBound = -14.0f;
    private float leftXBound = -38.0f;
    private float rightXBound = 18.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > topYBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.y < bottomYBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > rightXBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < leftXBound)
        {
            Destroy(gameObject);
        }
    }
}
