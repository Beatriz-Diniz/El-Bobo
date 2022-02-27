using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoSoldado_espada : MonoBehaviour
{

    public float speedInimigo;
    public int vida;

    private Transform posicaoDoJogador;
    private Rigidbody2D rig;
    public float JumpForce;
    public float forcaHorizontal;
    public float forcaHorizontalPadrao;
    private float posicaoRelativa;
    public int distancia;
    private bool ladoDireito = false;

    // Start is called before the first frame update
    void Start()
    {
        posicaoDoJogador = GameObject.FindGameObjectWithTag("Player").transform;
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        posicaoRelativa = (transform.position.x - posicaoDoJogador.position.x);

        if(posicaoRelativa > 0)
            ladoDireito = true;
        else
            ladoDireito = false;   

        //if((!GameController.instance.morto)&&((transform.position.x - posicaoDoJogador.position.x < 3)&&(posicaoDoJogador.position.x - transform.position.x < 3)))
        if((posicaoRelativa >= distancia)||(posicaoRelativa*(-1) >= distancia))
        {
            transform.position = Vector2.MoveTowards(transform.position, posicaoDoJogador.position, speedInimigo * Time.deltaTime);
        }
        if(transform.position.x - posicaoDoJogador.position.x > 0) //vira pra direita
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        if(transform.position.x - posicaoDoJogador.position.x < 0) //vira pra esquerda
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //inimigo que segue o player
        if(collision.gameObject.tag == "Player")
        {
            if(collision.transform.position.x > transform.position.x)
                forcaHorizontal *= -1;
            rig.AddForce(new Vector2(JumpForce*forcaHorizontal, 0), ForceMode2D.Impulse);
        }
        forcaHorizontal = forcaHorizontalPadrao;

        //inimigo se mexer junto com a plataforma
        if(collision.gameObject.CompareTag("plataforma"))
            this.transform.parent = collision.transform;

        //inimigo eh destruido se cair fora da cena
        if (collision.gameObject.tag.Equals("ForaDeCena"))
        {
            Debug.Log("Soldado Saiu da Cena");
            Destroy(gameObject);
        }
    }

    

    //inimigo para de ser mexer junto com a plataforma
    void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("plataforma"))
            this.transform.parent = null;
    }
}
