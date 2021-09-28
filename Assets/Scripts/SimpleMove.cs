using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleMove : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float moveSpeed;
    public bool canMove = false;
    public bool kill = false;
    private Rigidbody2D rigid;
    public GameObject deathOptions;
    private Animator anim;                      /*para manipular o animator*/
    private bool facinRight = true;             /*para indicar em qual direcao o personagem esta*/

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(canMove == true){
            Vector2 speed = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
            
            /*para colocar a animacao quando o personagem esta se movendo*/
            if(speed.x != 0)
                anim.SetFloat("Walk", Mathf.Abs(1));
            else
                anim.SetFloat("Walk", Mathf.Abs(0));

            /*salvar o valor inverso do valor de x do eixo do personagem*/
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;

            /*mover a animacao do personagem para a direita ou esquerda*/
            if(speed.x > 0 && !facinRight){
                facinRight = !facinRight;
                transform.localScale = theScale;
            }
            if(speed.x < 0 && facinRight){
                facinRight = !facinRight;
                transform.localScale = theScale;
            }

            speed *= moveSpeed;
            rigid.velocity = speed;
            
        }else{
            rigid.velocity = Vector2.zero;
        }
    }

    
}
