using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShield : MonoBehaviour
{
    private float rotateSpeed = 720;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,rotateSpeed * Time.deltaTime);
    }
}
