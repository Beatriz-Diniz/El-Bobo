using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject deathOptions;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void PlayGame ()
    {
        //Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity); 
        // Stop the menu song
        audioManager.Stop("MenuTheme");

        // Jump to the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitGame() {  
        Debug.Log("QUIT");  
        Application.Quit();
    }

    public void OpenMenu () 
    {
        Destroy(deathOptions);
        SceneManager.LoadScene("Menu");
    }
    
}