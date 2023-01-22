using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;
    public int group;
    public bool loop;

    [HideInInspector]
    public AudioSource source;
    [HideInInspector]
    public AudioReverbFilter reverb;
}
