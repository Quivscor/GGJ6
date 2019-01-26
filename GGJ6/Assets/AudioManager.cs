using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AM;

    public Sound[] sounds;

    private void Awake()
    {
        if (AM == null)
            AM = this;
        else if (AM != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);

        foreach(Sound sound in sounds)
        {
            sound.audio = gameObject.AddComponent<AudioSource>();
            sound.audio.clip = sound.clip;
            sound.audio.volume = sound.volume;
            sound.audio.pitch = sound.pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.audio.PlayOneShot(s.clip);
    }
}

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource audio;
}
