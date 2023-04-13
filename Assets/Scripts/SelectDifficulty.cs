using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectDifficulty : MonoBehaviour
{
    public void Easy()
    {
        SceneManager.LoadScene("Easy Scene",LoadSceneMode.Single);
    }
    public void Medium()
    {
        SceneManager.LoadScene("Medium Scene");
    }
    public void Hard()
    {
        SceneManager.LoadScene("Hard Scene");
    }
    public void Impossible()
    {
        SceneManager.LoadScene("Impossible Scene");
    }
}
