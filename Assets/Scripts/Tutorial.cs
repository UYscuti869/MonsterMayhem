using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject tutorial2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0;
    }
    public void Next()
    {
        tutorial.SetActive(false);
        tutorial2.SetActive(true);
    }
    public void Play()
    {
        tutorial2.SetActive(false);
    }
    private void OnDisable() {
        Time.timeScale = 1;
    }
}
