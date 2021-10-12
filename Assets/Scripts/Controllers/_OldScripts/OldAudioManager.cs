using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OldAudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource Music;
    public AudioSource[] SFXSounds;

    [SerializeField] public bool musicOn= true;
    [SerializeField] public bool sfxOn = true;
    public static OldAudioManager instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        

        foreach (AudioSource s in SFXSounds)
        {
            s.volume = s.volume;
            s.pitch = s.pitch;
            s.loop = s.loop;
        }
    }

    private void Start()
    {
        Music.Play();
    }
    

    public void Play(string name)
    {
        // // Sound s = Array.Find(SFXSounds, sound => sound.name == name);
        // if (s == null)
        // {
        //     Debug.LogWarning("Sound: " + name + " not found!");
        //     return;
        // }
        // s.source.Play();
    
        // FindObjectOfType<AudioManager>().Play("Jump"); // Call SoundManager from Any script
        
    }

    public void Update()
    {
        if (!musicOn) Music.volume = 0f;
        else if (musicOn) Music.volume = 0.1f;
        if (!sfxOn)
            for (int i = 0; i < SFXSounds.Length; i++) SFXSounds[i].volume = 0f;
        else if (sfxOn)
            for (int i = 0; i < SFXSounds.Length; i++) SFXSounds[i].volume = 0.5f;

    }

    // public void toggleMusic(string name)
    // {
    //
    //     
    //     if (s == null)
    //     {
    //         Debug.LogWarning("Sound: " + name + " not found!");
    //         return;
    //     }
    //
    //
    //     if (musicOn == true)
    //     {
    //         PlayerPrefs.SetFloat("musicIsOn", 0);
    //         Music.volume = 0f;
    //         musicOn = false;
    //     }
    //     else if (musicOn == false)
    //     {
    //         PlayerPrefs.SetFloat("musicIsOn", 1);
    //         Music.volume = 0.1f;
    //         musicOn = true;
    //     }
    //     else
    //     {
    //         return;
    //     }
    //
    // }
    //
    // public void toggleSFX(string name)
    // {
    //     Sound s = Array.Find(sounds, sound => sound.name == name);
    //     
    //     if (s == null)
    //     {
    //         Debug.LogWarning("Sound: " + name + " not found!");
    //         return;
    //     }
    //
    //
    //     if (sfxOn == true)
    //     {
    //         PlayerPrefs.SetFloat("sfxIsOn", 0);
    //         s.source.volume = 0f;
    //         sfxOn = false;
    //     }
    //     else if (sfxOn == false)
    //     {
    //         PlayerPrefs.SetFloat("sfxIsOn", 1);
    //         s.source.volume = 0.5f;
    //         sfxOn = true;
    //     }
    //     else
    //     {
    //         return;
    //     }
    //
    // }
}
