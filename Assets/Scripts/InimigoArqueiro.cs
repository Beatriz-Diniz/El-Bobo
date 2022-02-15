using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoArqueiro : MonoBehaviour
{
    //definicao de variaveis manipulaveis
    public float speedInimigo;
    public int vida;
    public int distancia;
    public int municao;
    //definicao de variaveis internas
    public bool atirando = false;
    public bool cooldowntiro = false;
    private bool ladoDireito = false;
    private float posicaoRelativa;

    //definicao do projetil e de sua posisao
    [SerializeField]
    private GameObject PosicaoProjetil;
    [SerializeField]
    private GameObject projetil;

    //defini objeto que contem a posicao do jogador
    private Transform posicaoDoJogador;

    
    void Start()
    {
        //posicaoDoJoador carrega o local em que o jogador esta no momento
        posicaoDoJogador = GameObject.FindGameObjectWithTag("Player").transform;   
    }

    void Update()
    {
        //chama a funcao que permite o disparo das flechas
        disparaDirecao();
        
    }

    void disparaDirecao(){
        //verifica a posicao d jogador
        posicaoRelativa = (transform.position.x - posicaoDoJogador.position.x);

        //verifica se esta para o lado direito ou esquerdo
        if(posicaoRelativa > 0)
            ladoDireito = true;
        else
            ladoDireito = false;

        //verifica se nao esta na distancia para disparar baseado na variavel "distancia"
        if((posicaoRelativa >= distancia)||(posicaoRelativa*(-1) >= distancia))
        {
            transform.position = Vector2.MoveTowards(transform.position, posicaoDoJogador.position, speedInimigo * Time.deltaTime);
        }
        else//caso possa atirar
        {
            //parado pra atacar
            if((municao > 0)&&(atirando != true))
                {
                atirando = true;
                atirar();//funcao que faz o disparo
                Debug.Log(atirando);
                }
        }

        //para onde a sprite esta virada
        if(transform.position.x - posicaoDoJogador.position.x > 0) //vira pra direita
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        if(transform.position.x - posicaoDoJogador.position.x < 0) //vira pra esquerda
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }

    private void atirar()
    {
        //gasta municao e cria o objeto flecha
        municao--;
        GameObject tmpProjetil = (GameObject) (Instantiate(projetil, PosicaoProjetil.transform.position, Quaternion.identity));
        
        //verifica a direcao do disparo
        if (ladoDireito)
            tmpProjetil.GetComponent<Flecha>().InicializarDirecao(Vector2.left);
        else
            tmpProjetil.GetComponent<Flecha>().InicializarDirecao(Vector2.right);
        
        
        StartCoroutine("cooldown");
        
        
        //cooldownTiro();
        //disparaDirecao();  
    }

    IEnumerator cooldown(){
        //canMove = false;
        yield return new WaitForSeconds(.5f);
        atirando = false;
        //canMove = true;
    }
}
