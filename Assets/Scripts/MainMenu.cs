using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject deathOptions;
    public GameObject save;
    public GameObject menu;
    private int valueSave;
    public bool morte;

    void Start()
    {   
        Debug.Log("MENU MORTE");
        menu.SetActive(false); 
        //ativar cursor do mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;    
    }

    void Update(){
        menu.SetActive(false); 
    }
    
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
        Debug.Log("Cena atual");
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        valueSave = save.transform.GetComponent<Save>().save;
        SceneManager.LoadScene(valueSave);
    }
    
}