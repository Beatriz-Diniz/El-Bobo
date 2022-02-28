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
    
    
    
    //pular
    public float jumpForce;                     //para determinar a forca do pulo
    private Transform groundCheck;              //para verificar se o personagem esta no chao
    private bool noChao;                        //para indicar se o personagem esta no chao ou nao
                                                
                                                
    //teste ataque
    private bool recovering = false;            //se recuperando de um ataque
    public int health;                          //total vida
    public float recoveryTime;                  //tempo para receber um novo dano
    private float recoveryCounter = 0;          //calculo do tempo de recuperacao 

    //vida coracao
    public UIManagerScript UIManagerScript;

    //menu de pause
    public GameObject menu;
    public bool isPaused;

    //interagir com objeto
    public bool EstaInteragindo {get;set;}


    //para teletransportar o player
    private Vector3 startPosition;
    private bool ForaDeCena;
    private bool naPlataforma;

    public static SimpleMove instance;
    void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        UIManagerScript = GameObject.FindObjectOfType<UIManagerScript>();
        instance = this;

        /*buscar o objeto com o nome de GroundCheck*/
        groundCheck = gameObject.transform.Find("GroundCheck");
    }

    void Update()
    {
        
        isPaused = menu.transform.GetComponent<Menu>().isPaused;
        if(canMove == true && isPaused == false){
            
            //interagir com objeto, tecla f
            if(Input.GetKeyDown(KeyCode.V)){
                Debug.Log("Interagiu");
                EstaInteragindo = true;
            }else{
                EstaInteragindo = false;
            }

            if(recovering){  //cooldown de recuperacao
                recoveryCounter += Time.deltaTime;
                
                if(recoveryCounter >= recoveryTime){
                    recoveryCounter = 0;
                    recovering = false;
                }
            }

            /*verifica se o personagem esta em contato com um objeto na layer no chao*/
            noChao = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("ground"));
            ForaDeCena = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("ForaDeCena"));
            naPlataforma = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("plataforma"));
           

            /*para o personagem pular*/
            if(Input.GetButtonDown("Jump") && (noChao || naPlataforma)){
                //anim.SetTrigger("Pulando");
                Debug.Log("Entrou Pulo");
                rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
           
            //teletransportar o player para a ultima posicao no chao antes de cair fora da cena
            if(noChao && !naPlataforma)
                startPosition = transform.position;

            if(!noChao && ForaDeCena){
                TakeDamage(1);
                transform.position = startPosition;
            }


            /*utilizar as setas do teclado para andar*/
            float speed = Input.GetAxisRaw("Horizontal");
            anim.SetFloat("Walk", Mathf.Abs(speed));

            /*determinar uma velocidade constante, velocidade de movimento = 7*/
            rigid.velocity = new Vector2(speed * moveSpeed, rigid.velocity.y);

            /*salvar o valor inverso do valor de x do eixo do personagem*/
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;

            /*mover o personagem para a direita ou esquerda*/
            if(speed > 0 && !facinRight){
                Flip();
            }
            if(speed < 0 && facinRight){
                Flip();
            }
            
        }else{
                rigid.velocity = Vector2.zero;
        }

        if(health <= 0)
                Die();
    }
    
    void Flip(){
        /*salvar o valor inverso do valor de x do eixo do personagem*/
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        
        facinRight = !facinRight;
        transform.localScale = theScale;
    }

    //player se mexer junto com a plataforma
    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("plataforma"))
            this.transform.parent = col.transform;
    }

    //player para de ser mexer junto com a plataforma
    void OnCollisionExit2D(Collision2D col) {
        if(col.gameObject.CompareTag("plataforma"))
            this.transform.parent = null;
    }

    //funcao para receber dano
    public void TakeDamage(int damage){

        if(!recovering){
            //playerAnim.SetTrigger("hurt"); //animcao quando receber o dano
            health -= damage;
            Debug.Log(damage);
            Debug.Log(health);
            UIManagerScript.updateLives(health);
            
            recovering = true;
        }
        
        
    }

    void Die(){
        Debug.Log("Morreu");
        UIManagerScript.updateLives(0);
        //desativar o bobo do jogo
        gameObject.SetActive(false);
        Time.timeScale = 0f;
        deathOptions.SetActive(true); 
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
}