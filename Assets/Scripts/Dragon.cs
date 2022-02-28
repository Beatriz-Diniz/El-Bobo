using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    //verificar se o player morreu
    public GameObject mainMenu;
    public GameObject bobo;
    public bool morte;


    private static Animator anim;                      /*para manipular o animator*/
    public GameObject deathOptions;
    private static bool _ataque;
    public static bool ataque{
        get{
            return _ataque;
        }
        set{
            _ataque = value;
            if(value == true)
                anim.SetTrigger("Ataque");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(){
        Debug.Log("Entrou Game Over");
        //marcar que o player morreu para o menu de pause nao ser ativado com esc e misturar com o menu de morte
        mainMenu.transform.GetComponent<MainMenu>().morte = true;
        bobo.gameObject.GetComponent<SimpleMove>().TakeDamage(10);
        Instantiate(deathOptions, new Vector3(0, 0, 0), Quaternion.identity); 
        ataque = false; 
    }
}
