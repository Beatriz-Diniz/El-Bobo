using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colidiu : MonoBehaviour
{
    [SerializeField] private int lifeMax = 30;
    [SerializeField] private int life;
    private Rigidbody2D rig;
    //[SerializeField] private BarraVida barraVida;

    public float forcaHorizontal;
    public float forcaHorizontalPadrao;
    public float empurrada;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "soldado_espada")
        {
            if(collision.transform.position.x > transform.position.x)
                forcaHorizontal *= -1;
            rig.AddForce(new Vector2(empurrada*forcaHorizontal, empurrada), ForceMode2D.Impulse);
            if(life > 0) 
                life = life - 10;
            else
                Destroy(gameObject);
                //comando de fim do jogo aqui

            forcaHorizontal = forcaHorizontalPadrao;
        }
    }
}
