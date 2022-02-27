using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEnemy : MonoBehaviour
{
    private Transform player;
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

    private Rigidbody2D rb;
    [HideInInspector] public bool recovering = false;         // estah invulneravel nesse tempo
    public float recoveryTime;
    public float knockbackForceX;
    public float knockbackForceY;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
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

    public void TakeDamage(int damage) 
    {
        if (!recovering) 
        {
            recovering = true;
            //animacao do inimigo sofrendo dano
            //GetComponent<Animator>().SetTrigger("Knockback");
            StartCoroutine(Knockback(damage));
        }
    }
    
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player")) 
        {
            other.gameObject.GetComponent<SimpleMove>().TakeDamage(dano);
        }
    }

    public IEnumerator Knockback (int damage)
    {
        if (rb != null)
        {
            recovering = true;
            rb.velocity = Vector2.zero;

            if (rb.transform.position.x < player.position.x)
            {
                rb.AddForce(new Vector2(-knockbackForceX, knockbackForceX), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(knockbackForceX, knockbackForceX), ForceMode2D.Impulse);
            }
            health -= damage;
            UpdateHealthBar();
                        
            yield return new WaitForSeconds(recoveryTime);
            recovering = false;
        }        
    }
}
