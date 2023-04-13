using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void Restart()
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
    public void MainMenu()
    {
        SceneManager.LoadScene("Start Menu",LoadSceneMode.Single);
    }
}
