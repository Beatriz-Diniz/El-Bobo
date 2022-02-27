using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transicao : MonoBehaviour
{
    public GameObject [] texto;
    public GameObject botao;
    public int i;
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
        if(i == 7)
            botao.SetActive(true);  
        
    }

    void Ativar() {
        if(i == 4){
            texto[0].SetActive(false); 
            texto[1].SetActive(false); 
            texto[2].SetActive(false); 
            texto[3].SetActive(false); 
        }
        texto[i].SetActive(true);     
        i++;
    }

}
