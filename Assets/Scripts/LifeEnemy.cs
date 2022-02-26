using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEnemy : MonoBehaviour
{
    public bool recovering = false;                 // estah invulneravel nesse tempo

    //public float recoveryTime;                    // OBS: pensei em nao usar essa variavel e
                                                    // tomar automaticamente como recoveryTime
                                                    // o tempo da animacao do knockback

    public int health;

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
        if (health <= 0) 
        {
            animator.SetTrigger("Morrendo");
            Debug.Log("Soldado morreu por vida");
            Destroy(inimigo, tempoAnimacaoMorte);
        }
        
    }


    void UpdateHealthBar() 
    {
        healthBarScale.x = healthPercent * health;
        healthBar.localScale = healthBarScale;
    }

    public void TakeDamage(int damage) {
        if(!recovering) 
        {
            //animacao do inimigo sofrendo dano
            GetComponent<Animator>().SetTrigger("Knockback");

            Debug.Log("Tomou dano");
            health -= damage;
            UpdateHealthBar();
        }
    }
    
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player")) 
        {
            other.gameObject.GetComponent<SimpleMove>().TakeDamage(dano);
        }
    }
}
