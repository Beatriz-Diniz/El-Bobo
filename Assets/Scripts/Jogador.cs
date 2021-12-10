using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    [SerializeField] private int lifeMax = 3;
    [SerializeField] private int life;
    //[SerializeField] private BarraVida barraVida;

    //atualiza as vidas ao iniciar o jogo
    private void Awake()
    {
        AtualizaVida();
    }
    
    //calcula o dano tomado do jogador
    public void TomaDano(int damage)
    {
       /* life -= damage;
        if(life <= 0)
            Kill();
        AtualizaVida();*/
      
    }
    //Atualiza a barra de vida de acordo com o total de vidas do jogador
    public void AtualizaVida()
    {
   //     barraVida.AtualizaBarrasDeVida(life,lifeMax);
    }

    //mata o jogador destruindo o  objeto caso ele fique sem vida
    private void Kill()
    {
        Destroy(gameObject);
    }

}
