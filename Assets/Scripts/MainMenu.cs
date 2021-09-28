using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject deathOptions;

    public void PlayGame ()
    {        
        //Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);   
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