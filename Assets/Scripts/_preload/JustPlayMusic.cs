using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustPlayMusic : MonoBehaviour
{
    public enum Type { MainMenu, Forest, Castle, Dragon, Bonfire };
    public Type songName;

    AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        audioManager.StopAll();

        audioManager.Play(songName.ToString());
    }
}
