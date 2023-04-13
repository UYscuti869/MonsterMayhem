using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    private AudioSource audioSource;
    public static BGMController instance;
    public AudioClip audioClip;
    
    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void OnEnable() 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;   
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "Level Select Menu" || 
        SceneManager.GetActiveScene().name == "Start Menu" || SceneManager.GetActiveScene().name == "Help")
        {
        
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateVolume(float volume)
    {
        audioSource.volume = volume;
    }
    public void FindSliderAndUpdateReference()
    {
        Slider slider = FindObjectOfType<Slider>();
        if (slider != null)
        {
            slider.onValueChanged.RemoveAllListeners();
            slider.onValueChanged.AddListener(UpdateVolume);
        }
    }
}
