using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fim : MonoBehaviour
{   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    /*para mudar de cena quando interagir com uma porta ou quando clicar no botao*/
    public void proximaCena(){
            //carrega a proxima cena          
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Destroy(gameObject);
    }

    public void Sair(){
        Application.Quit();
    }
}
