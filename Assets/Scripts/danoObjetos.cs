using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class danoObjetos : MonoBehaviour
{   
    public int dano;                                //dano que o inimigo vai dar no player
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<SimpleMove>().TakeDamage(dano);
    }
}
