using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
public class PlayGame : MonoBehaviour
{
    public GameObject option;
    public void StartGame()
    {
        SceneManager.LoadScene("Level Select Menu",LoadSceneMode.Single);
    }
    public void ExitGame()
    {
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit();
    #endif
    }
    public void Help()
    {
        SceneManager.LoadScene("Help");
    }
    public void Options()
    {
        option.SetActive(true);
    }
    public void ExitOptions()
    {
        option.SetActive(false);
    }
}
