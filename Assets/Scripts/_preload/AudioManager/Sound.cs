using UnityEngine.Audio;
using UnityEngine;

// Mark the Sound class as serializable enabling a variable list of Sound (used in the AudioManager.cs)
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    // For sound mixer, changeble by menu or something else
    public AudioMixerGroup output;

    // Limits the range of volume and pitch
    [Range(0f, 1f)]
    public float volume = 0.5f;
    [Range(.1f, 3f)]
    public float pitch = 1f;

    // Sets if the sound will loop after his time ends
    public bool loop;

    // Hides in the inspector, even the source being public
    [HideInInspector]
    public AudioSource source;    
}
