using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
public class SimpleMove : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float moveSpeed;
    public bool canMove = true;
    public bool kill = false;
    private Rigidbody2D rigid;
    public GameObject deathOptions;
    private Animator anim;                      //para manipular o animator
    private bool facinRight = true;             //para indicar em qual direcao o personagem esta
                                                
                                                
    //teste ataque
    private bool recovering = false;            //se recuperando de um ataque
    public int health;                          //total vida
    public float recoveryTime;                  //tempo para receber um novo dano
    private float recoveryCounter = 0;          //calculo do tempo de recuperacao 
    private float attackTimer;                  //calculo do cooldown de ataque

    //combate
    //public GameObject vCam;                   //camera para executar efeitos
    public LayerMask enemyLayer;                //layers do inimigo
    public int attackDamage;                    //dano por ataque
    public Transform attackHit;                 //ponto de origem do ataque
    public float attackRange;                   //alcance do ataque
    public float attackCooldown;                //cooldown do ataque




    //teste trocar arma
    public bool cetro;
    public bool espada;
    public bool arco;

    //teste
    public UIManagerScript UIManagerScript;

    public static SimpleMove instance;
    void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        UIManagerScript = GameObject.FindObjectOfType<UIManagerScript>();
        instance = this;
    }

    void Update()
    {
        

        if(canMove == true){
            
            if(recovering){  //cooldown de recuperacao
                recoveryCounter += Time.deltaTime;
                
                if(recoveryCounter >= recoveryTime){
                    recoveryCounter = 0;
                    recovering = false;
                }
            }

            Vector2 speed = new Vector2(Input.GetAxisRaw("Horizontal"), rigid.velocity.y);
            
            /*para colocar a animacao quando o personagem esta se movendo*/
            if(speed.x != 0)
                anim.SetFloat("Walk", Mathf.Abs(1));
            else
                anim.SetFloat("Walk", Mathf.Abs(0));

            /*mover a animacao do personagem para a direita ou esquerda*/
            if(speed.x > 0 && !facinRight){
                Flip();
            }
            if(speed.x < 0 && facinRight){
                Flip();
            }

            speed *= moveSpeed;
            rigid.velocity = speed;

            //se o attack nao esta em cooldown
            if(attackTimer <= 0){
                if(Input.GetMouseButtonDown(0)){    
                    
                    Attack();
                    attackTimer = attackCooldown;
                } 
            }else{
                    attackTimer -=Time.deltaTime;
            } 

            
        }else{
                rigid.velocity = Vector2.zero;
        }
    }
    
    void Flip(){
        /*salvar o valor inverso do valor de x do eixo do personagem*/
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        
        facinRight = !facinRight;
        transform.localScale = theScale;
    }

    void Attack(){
        //anim.SetTrigger("Atacando");
        Collider2D[] targets = Physics2D.OverlapCircleAll(attackHit.position, attackRange, enemyLayer);
        if(cetro == true){
            //anim.SetTrigger("AtaqueCetro");
        }else{
            if(espada == true){
                //anim.SetTrigger("AtaqueEspada");
            }
                
            if(arco == true){
                //anim.SetTrigger("AtaqueArco");
            }
            //para acessar o script de todos os inimigos no mapa
            foreach(Collider2D target in targets){
                target.GetComponent<Enemy>().TakeDamage(attackDamage);
                
            }
        }
        

        
        StartCoroutine("Freeze");
    }

    //so para ver onde esta sendo desenhada a esfera de alcance de ataque
    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackHit.position, attackRange);
    }
    
   

    //funcao para receber dano
    public void TakeDamage(int damage){

        if(!recovering){
            //playerAnim.SetTrigger("hurt"); //animcao quando receber o dano
            
        
            health -= damage;
            Debug.Log(health);
            UIManagerScript.updateLives(health);
            
            recovering = true;
            
            if(health <= 0)
                Die();
            
        }
        
        
    }

    void Die(){
        Debug.Log("Morreu");
        Destroy(gameObject);
        Instantiate(deathOptions, new Vector3(0, 0, 0), Quaternion.identity); 
    }
    

    public IEnumerator Knockback(float KnockbackDuration, float KnockbackPower, Transform obj){
        float timer = 0;
        while(KnockbackDuration > timer){
            timer+= Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            rigid.AddForce(-direction * KnockbackPower);
        }

        yield return 0;
    }

    //funcao que impede o personagem de se mover apos receber dano
    IEnumerator Freeze(){
        canMove = false;
        yield return new WaitForSeconds(.5f);
        canMove = true;
    }
}
