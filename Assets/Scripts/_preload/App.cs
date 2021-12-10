using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class App : MonoBehaviour
{
    void Start()
    {
        // Uses the name of the scene selected using the DevPreload Singleton
        string nextScene = FindObjectOfType<DevPreload>().nextScene.ToString();
        Debug.Log(nextScene);

        // Jump to the scene indicated inside the DevPreload object
        if (nextScene != "Preload")
            SceneManager.LoadScene(nextScene);
    }
}
