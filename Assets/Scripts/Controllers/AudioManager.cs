using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; 
    
    public AudioSource Music;
    public Sound[] SFXSounds;
    [CanBeNull] private AudioSource Player;

    public bool musicOn = true;
    public bool sfxOn = true;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

    }
    
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "1_GameLevel")
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        }

        else Player = null;
        
        if (!musicOn) Music.volume = 0f;
        else if (musicOn) Music.volume = 0.1f;
        if (!sfxOn) Player.volume = 0f;
        else if (sfxOn) Player.volume = 0.2f;
    }
    
    public void toggleMusic(string name)
    {
        if (musicOn)
        {
            PlayerPrefs.SetFloat("musicIsOn", 0);
            Music.volume = 0f;
            musicOn = false;
        }
        
        else if (!musicOn)
        {
            PlayerPrefs.SetFloat("musicIsOn", 1);
            Music.volume = 0.1f;
            musicOn = true;
        }

    }

    public void PlaySFX(string name)
    {
        Sound sfx = Array.Find(SFXSounds, sound => sound.name == name);
        if (sfx == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        Player.clip = sfx.clip;
        Player.Play();
    }

    public void toggleSFX(string name)
    {
        if (sfxOn)
        {
            PlayerPrefs.SetFloat("musicIsOn", 0);
            Player.volume = 0f;
            musicOn = false;
        }
        
        else if (!sfxOn)
        {
            PlayerPrefs.SetFloat("musicIsOn", 1);
            Player.volume = 0.2f;
            musicOn = true;
        }

    }


}
