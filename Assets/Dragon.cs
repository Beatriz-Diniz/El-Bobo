using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
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
        //anim.SetTrigger("Ataque");
        Instantiate(deathOptions, new Vector3(0, 0, 0), Quaternion.identity); 
        ataque = false; 
    }
}
