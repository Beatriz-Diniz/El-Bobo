using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InimigoIA : MonoBehaviour
{
    private Rigidbody2D rb;

    //define velocidade de movimento padrão
    public float velocidadeHorizontal = 1f;
    public float velocidadeAtual;
    public float forcaPulo;
    [SerializeField] private LayerMask layerPermitidas;
    [SerializeField] private Vector2 raycastOffset; //Valores para definir distancia do raycast
    [SerializeField] private float rangeDetectar; //alcance de detecção do player
    [SerializeField] private bool modoZumbi; //segue o player
   // [SerializeField] private bool modoKnockBack; //afasta o player quando da dano
    [SerializeField] private SimpleMove player; 

    private bool seguindoPlayer;
    private bool estaPulando;
    private bool noChao;
    private bool stun;

    private LifeEnemy lifeEnemy;
    private KnockBack movement;
    private HitStop stop;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        anda(transform.right);//Define a direção de movimento para a direita
    }   

    // Start is called before the first frame update
    void Start()
    {   
        lifeEnemy = GetComponent<LifeEnemy>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<KnockBack>();
        stop = GameObject.FindGameObjectWithTag("Player").GetComponent<HitStop>();
    }

    private void Update()
    {     
        DetectaParede();
        DetectaBorda();
        DetectaPlayer();
    }
    
    void FixedUpdate()
    {
        if (!lifeEnemy.recovering)
        {
            //define a velocidade
            Vector2 vel = rb.velocity;
            vel.x = velocidadeAtual;
            rb.velocity = vel;
        }
    }
    public void set_stun(bool hitstun){
        stun = hitstun;
    }

    private bool get_stun(){
        return stun;
    }
    public bool get_chao(){
        return noChao;
    }

    void anda(Vector2 direcao){
       if(get_stun() == false){
            velocidadeAtual = direcao.x * velocidadeHorizontal;
        }else{
            velocidadeAtual = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {   
        if(collision.gameObject.tag.Equals("Player")){
            movement.knock();
            stop.Stop();
            noChao = false;
        }

        if(collision.gameObject.tag.Equals("ForaDeCena"))
            Destroy(gameObject);
    }

    private void DetectaParede()
    {
        //configura o raycast
        var origemX = transform.position.x + raycastOffset.x;
        var origemY = transform.position.y + raycastOffset.y;
        var raycastParedeDireita = Physics2D.Raycast(new Vector2(origemX,origemY),Vector2.right,0.3f,layerPermitidas);
        var raycastParedeEsquerda = Physics2D.Raycast(new Vector2(transform.position.x-raycastOffset.x,origemY),Vector2.left, 0.3f,layerPermitidas);
    
        //deixa o raio do raycast visivel sem que apareça para o player
        Debug.DrawRay(new Vector2(transform.position.x, origemY),Vector2.right, Color.blue);
        Debug.DrawRay(new Vector2(transform.position.x, origemY), Vector2.left,Color.blue);
        
        //configura para andar até a parede direita e esquerda e voltar caso colida
        if(raycastParedeDireita.collider != null)
        { 
            if(!modoZumbi || !seguindoPlayer)
                anda(transform.right*-1);
            else
                Pula();
        }
            
        if(raycastParedeEsquerda.collider != null)
        {   
           
            if(!modoZumbi || !seguindoPlayer)
                anda(transform.right*1); //muda a direcao
            else
                Pula();
        }
        
    }

    private void DetectaBorda()
    {
        //Detecta o chao para evitar bordas
        var raycastChaoDireita = Physics2D.Raycast(new Vector2(transform.position.x + raycastOffset.x, transform.position.y), Vector2.down, 1f, layerPermitidas);
        var raycastChaoEsquerda = Physics2D.Raycast(new Vector2(transform.position.x - raycastOffset.x, transform.position.y), Vector2.down, 1f, layerPermitidas);
        
        //deixa o raio do raycast visivel sem que apareça para o player
        Debug.DrawRay(new Vector2(transform.position.x + raycastOffset.x, transform.position.y), Vector2.down, Color.blue);
        Debug.DrawRay(new Vector2(transform.position.x - raycastOffset.x, transform.position.y), Vector2.down, Color.blue);
        
        if(raycastChaoDireita.collider !=  null && raycastChaoEsquerda.collider !=null)
        {  
             noChao = true;
        }else{
             noChao = false;
        }

        if(raycastChaoDireita.collider ==null)
        {   
            anda(transform.right*-1);
        }
            
        if(raycastChaoEsquerda.collider ==null)
        {   
            anda(transform.right*1);
           
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,rangeDetectar);
    }
    void DetectaPlayer()
    {
        var diferencaParaPlayer = player.gameObject.transform.position.x - transform.position.x;
        seguindoPlayer = Mathf.Abs(diferencaParaPlayer) < rangeDetectar;
            if(modoZumbi && seguindoPlayer){
                if(diferencaParaPlayer < 0)
                    anda(transform.right*-1); //segue o player a esquerda
                else
                    anda(transform.right*1); //segue o player a direita
            }
    }

    void Pula()
    {   
        if(noChao)
            rb.AddForce(Vector2.up * forcaPulo);
        noChao = false;
        
    }

    //inimigo se mexer junto com a plataforma
    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("plataforma"))
            this.transform.parent = col.transform;
    }

    //inimigo para de ser mexer junto com a plataforma
    void OnCollisionExit2D(Collision2D col) {
        if(col.gameObject.CompareTag("plataforma"))
            this.transform.parent = null;
    }

}