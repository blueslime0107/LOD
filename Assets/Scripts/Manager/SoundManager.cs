using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public string startSound;
    public Sound[] sounds;
    public AudioMixerGroup[] mixer;
    

    void Awake()
    {
        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = mixer[s.group];
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

        }
    }

    void Start(){
        if(startSound == ""){return;}
        Play(startSound);
    }

    public void Play(string name){
        try
        {Sound s = Array.Find(sounds, sound => sound.name.Equals(name));
        if(s == null){
            Debug.Log("Sound Missing");
            return;
            }
        s.source.Play();}
        catch{
            Debug.Log("Play Error!");
        }
    }
}
