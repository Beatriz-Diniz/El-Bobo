using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEnemy : MonoBehaviour
{
    private bool recovering;
    public int health;

    public float recoveryTime;

    //para criar a barra de hp
    public Transform healthBar;                     //barra verde
    public GameObject healthBarObject;              //objeto pai das barras

    public GameObject inimigo;
    private Vector3 healthBarScale;                 //tamanho da barra
    private float healthPercent;                    //porcentagem da vida

    public float tempoAnimacaoMorte;                //tempo de animacao da morte do inimigo
    private Animator animator;                      //para manipular o animator

    public int dano;                                //dano que o inimigo vai dar no player


    void Start()
    {
        healthBarScale = healthBar.localScale;
        healthPercent = healthBarScale.x/health;
        animator = gameObject.GetComponent<Animator>();
    }


    void Update()
    {
        //animacao de morte e destruir inimigo
        if(0 == health){
            animator.SetTrigger("Morrendo");
            Destroy(inimigo, tempoAnimacaoMorte);
        }
        
    }


    void UpdateHealthBar(){
        healthBarScale.x = healthPercent * health;
        healthBar.localScale = healthBarScale;
    }

    public void TakeDamage(int damage){
        if(!recovering){
            //animacao do inimigo sofrendo dano
            health -= damage;
            UpdateHealthBar();
        }
    }
    
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<SimpleMove>().TakeDamage(dano);
        }
    }
}
