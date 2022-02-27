using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Used for access the preload scene from any scene and other stuffs
// This is a Singleton
// obs: ONLY USED FOR DEVELOPMENT, NOT IMPLEMENTED FOR THE GAME
public class DevPreload : MonoBehaviour
{
    public static DevPreload Instance { get; private set; }
    public enum Type { Preload, Cutscene1, _1FaseFloresta, _2FaseFloresta, _3FaseCastelo, _4Boss, Creditos };
    public Type nextScene;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        Debug.Log(nextScene);

        GameObject check = GameObject.Find("__app");

        if (check == null)
        {
            SceneManager.LoadScene("Preload");
        }
    }
}
