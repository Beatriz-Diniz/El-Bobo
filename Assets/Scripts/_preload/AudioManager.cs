using UnityEngine.Audio;
using System;
using UnityEngine;

// How to use:
// Inside some script of an object you desire to play a sound listed on the AudioManager
// you can simply type: 
// ***************** 'FindObjectOfType<AudioManager>().Play("Name of the sound");' ******************
// passing the name of the sound to play it.

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // 'instance' references to itself
    public static AudioManager instance;

    // Awake is called before the Start method
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            
            s.source.outputAudioMixerGroup = s.output;

            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    // OBS: The functions bellow could be one function with a "switch - case"
    //      

    // Play the sound with the 'name' passed by parameter
    public void Play (string name)
    {
        // search in the sound array the sound with de given name
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // Check it the given 'name' exists
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + " not found to PLAY!");
            return;
        }         

        
        // Play the sound found
        s.source.Play();
    }

    // Stop the sound with the 'name' passed by parameter
    public void Stop (string name)
    {
        // search in the sound array the sound with de given name
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // Check it the given 'name' exists
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + " not found to STOP!");
            return;
        }

        // Stop the sound found
        s.source.Stop();
    }

    // Stop the sound with the 'name' passed by parameter
    public void Pause(string name)
    {
        // search in the sound array the sound with de given name
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // Check it the given 'name' exists
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + " not found to PAUSE!");
            return;
        }

        // Test if the audio is playing
        if (s.source.isPlaying)
            s.source.Pause();
    }

    // Stop the sound with the 'name' passed by parameter
    public void Unpause(string name)
    {
        // search in the sound array the sound with de given name
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // Check it the given 'name' exists
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + " not found to UNPAUSE!");
            return;
        }

        // Test if the audio is not playing
        if (s.source.isPlaying == false)
            s.source.UnPause();
    }

    /*
    // Can play multiple sounds on one AudioSource
    public void PlayOneShot(string name, float volumeScale)
    {
        // search in the sound array the sound with de given name
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // Check it the given 'name' exists
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + " not found to PlayOneShot!");
            return;
        }

        s.source.PlayOneShot();
    }


    */
}
