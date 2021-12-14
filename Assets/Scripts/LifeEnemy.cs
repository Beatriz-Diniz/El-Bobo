using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEnemy : MonoBehaviour
{
    private bool recovering;
    public int health;

    public float recoveryTime;

    //para criar a barra de hp
    public Transform healthBar; //barra verde
    public GameObject healthBarObject; //objeto pai das barras

    public GameObject inimigo;
    private Vector3 healthBarScale; //tamanho da barra
    private float healthPercent; //porcentagem da vida

    void Start()
    {
        healthBarScale = healthBar.localScale;
        healthPercent = healthBarScale.x/health;
    }
    void Update()
    {
        if(health <= 0){
            Destroy(inimigo);
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
            other.gameObject.GetComponent<SimpleMove>().TakeDamage(1);
        }
    }
}
