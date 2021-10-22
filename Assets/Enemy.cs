using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    private float speed = 0;               /*determinar a velocidade do personagem*/
    
    private Transform groundCheck;          /*para verificar se o personagem esta no chao*/
    private Rigidbody2D rb;                 /*para manipular o Rigidbody do personagem*/
    private Animator anim;                  /*para manipular o animator*/
    
    private bool facinRight = false;        /*para indicar em qual direcao o personagem esta*/
    private bool noChao = false;            /*para indicar se o personagem esta no chao ou nao*/
    
    
    private bool canMove = true;
    private bool recovering;
    private float recoveryCounter;

   
    public int health;    
    public float recoveryTime;
    

    //para criar a barra de hp
    public Transform healthBar; //barra verde
    public GameObject healthBarObject; //objeto pai das barras

    private Vector3 healthBarScale; //tamanho da barra
    private float healthPercent; //porcentagem da vida
    

    public float KnockbackPower = 200;
    public float KnockbackDuration = 2;
    // Start is called before the first frame update
    void Start(){
        /*iniciar o Rigidbody e o Animator*/
        rb = gameObject.GetComponent<Rigidbody2D>();    
        anim = gameObject.GetComponent<Animator>();

        /*buscar o objeto com o nome de GroundCheck*/
        groundCheck = gameObject.transform.Find("EnemyGroundCheck");

        healthBarScale = healthBar.localScale;
        healthPercent = healthBarScale.x/health;
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
         
            StartCoroutine("StopMove");

            recovering = true;

            if(health <= 0){
                Destroy(gameObject);
            }
        }
    }

    IEnumerator StopMove(){
        rb.velocity = Vector2.zero;
        canMove = false;
        yield return new WaitForSeconds(.5f);
        canMove = true;
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    



    // Update is called once per frame
    void Update(){
      
            
    }

    /*toda movimentacao do inimigo*/
    void FixedUpdate(){

        if(canMove = true){

            if(recovering){
                recoveryCounter += Time.deltaTime;
                if(recoveryCounter >= recoveryTime){
                    recoveryCounter = 0;
                    recovering = false;
                }
            }
            /*verifica se o personagem esta em contato com um objeto na layer Ground*/
            noChao = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
            
            if (!noChao){
                speed = speed*(-1);
            }

            /*determinar uma velocidade constante*/
            rb.velocity = new Vector2(speed, rb.velocity.y);
            
            /*salvar o valor inverso do valor de x do eixo do personagem*/
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            
            /*mover o personagem para a direita ou esquerda*/
            if(speed > 0 && !facinRight){
                facinRight = !facinRight;
                transform.localScale = theScale;
                healthBarObject.transform.localScale = new Vector3(healthBarObject.transform.localScale.x *-1, 
                                                                healthBarObject.transform.localScale.y, 
                                                                healthBarObject.transform.localScale.z);
            }
            else if(speed < 0 && facinRight){
                facinRight = !facinRight;
                transform.localScale = theScale;
                healthBarObject.transform.localScale = new Vector3(healthBarObject.transform.localScale.x *-1, 
                                                                healthBarObject.transform.localScale.y, 
                                                                healthBarObject.transform.localScale.z);
            }
        }

    }

 
 
    /*para o player perder vida quando colidir com o inimigo*/
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            StartCoroutine(SimpleMove.instance.Knockback(KnockbackDuration, KnockbackPower, this.transform));
            other.gameObject.GetComponent<SimpleMove>().TakeDamage(1);
        }
    }
}


