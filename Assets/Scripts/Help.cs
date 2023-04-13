using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Help : MonoBehaviour
{
    public GameObject panel;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject next1;
    public GameObject next2;
    public GameObject next3;
    public GameObject back1;
    public GameObject back2;
    public GameObject back3;
    public void Next1()
    {
        panel.SetActive(false);
        panel2.SetActive(true);
        next1.SetActive(false);
        next2.SetActive(true);
        back1.SetActive(false);
        back2.SetActive(true);
    }
    public void Next2()
    {
        panel2.SetActive(false);
        panel3.SetActive(true);
        next2.SetActive(false);
        next3.SetActive(true);
        back2.SetActive(false);
        back3.SetActive(true);
    }
    public void Home()
    {
        SceneManager.LoadScene("Start Menu",LoadSceneMode.Single);
    }
    public void Back2()
    {
        panel2.SetActive(false);
        panel.SetActive(true);
        next2.SetActive(false);
        next1.SetActive(true);
        back2.SetActive(false);
        back1.SetActive(true);
    }
    public void Back3()
    {
        panel3.SetActive(false);
        panel2.SetActive(true);
        next3.SetActive(false);
        next2.SetActive(true);
        back3.SetActive(false);
        back2.SetActive(true);
    }
}
