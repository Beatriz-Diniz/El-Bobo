using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transicao : MonoBehaviour
{
    public GameObject [] texto;
    public GameObject botao;
    public int i;
    private float time = 0f;
    private bool firstPage = true;
    private bool secondPage = true;

    // Start is called before the first frame update
    void Start()
    {   
        InvokeRepeating("Ativar", 1f, 4f);
    }

    // Update is called once per frame
    void Update()
    {   
        if(i == 4 && firstPage){
            firstPage = false;
            i = 0;
            CancelInvoke("Ativar");
            InvokeRepeating("Desativar", 4f, 0.00005f);
        }

        if(i == 4 && !firstPage && secondPage){
            secondPage = false;
            CancelInvoke("Desativar");
            AtivarSegundaPagina();
        }

        if(i == 7){
            CancelInvoke("Ativar1");
            botao.SetActive(true);  
        }
    }

    void Ativar() {
        texto[i].SetActive(true);     
        i++;
    }

    private void Desativar(){
        texto[i].SetActive(false);            
        i++;
    }

    void AtivarSegundaPagina(){
        InvokeRepeating("Ativar1", 1f, 4f);
    }

    void Ativar1(){
        texto[i].SetActive(true);     
        i++;
    }
    
}
