using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusic : MonoBehaviour
{
    public enum Music { MenuTheme, ForestTheme, CastleTheme, DragonTheme };
    private AudioManager audioManager;
    public Music musicName;

    void Start()
    {
        Debug.Log(musicName.ToString());
        FindObjectOfType<AudioManager>().Play(musicName.ToString());
        //FindObjectOfType<AudioManager>().Play("Menu");
    }

}
