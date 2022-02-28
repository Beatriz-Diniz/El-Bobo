using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaCena : MonoBehaviour
{
    public MudarCena transicao;
    // Start is called before the first frame update
    void Start()
    {
        transicao = GameObject.FindObjectOfType<MudarCena>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Return) && SceneManager.GetActiveScene().buildIndex == 1){
            transicao.IniciaTransicao();
        }
    }

    public void ProximaCenaButton(){
        transicao.IniciaTransicao();
    }

    

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player") && gameObject.CompareTag("checkpoint"))
            transicao.IniciaTransicao();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
            transicao.IniciaTransicao();
    }
}
