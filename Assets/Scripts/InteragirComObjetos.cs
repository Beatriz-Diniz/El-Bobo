using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class InteragirComObjetos : MonoBehaviour
{   
    
    [SerializeField] private SimpleMove _jogador;
    [SerializeField] private UnityEvent _interagir;
    
    private bool executar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //criar caixa para selecionar uma funcao de outro script
        if(executar){
            if(_jogador.EstaInteragindo == true){
                _interagir.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        executar = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        executar = false;
    }
}
