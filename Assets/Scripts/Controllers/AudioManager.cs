using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private bool musicOn= true;
    private bool sfxOn = true;
    public static AudioManager instance;

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

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("Theme"); // Play sound from within manager
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();

        // FindObjectOfType<AudioManager>().Play("Jump"); // Call SoundManager from Any script


    }

    public void toggleMusic(string name)
    {
        // Toggle
    }
}
