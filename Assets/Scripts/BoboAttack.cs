using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoboAttack : MonoBehaviour
{
    public Animator animator; 
    public Transform attackPoint; //ponto de ataque
    public float attackRange = 0.5f; //alcance do ataque
    public LayerMask enemyLayers; //layers inimigas
    public int attackDamage; //dano por ataque

    //teste trocar arma
    public bool cetro;
    public bool espada;
    public bool arco;
    

    //ataque tempo
    private float attackTimer;                  //calculo do cooldown de ataque
    public float attackCooldown;                //cooldown do ataque
    
    public bool canMove = true;

    //menu de pause
    public GameObject menu;
    public bool isPaused;

    void Update()
    {
        isPaused = menu.transform.GetComponent<Menu>().isPaused;
        //se o attack nao esta em cooldown
        if(attackTimer <= 0 && isPaused == false){
            if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.C)){    
                Attack();
                attackTimer = attackCooldown;
            } 
        }else
            attackTimer -=Time.deltaTime;
         
    }

    void Attack(){
        Collider2D[] targets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Debug.Log("Ataque");
        if(cetro == true){
            //anim.SetTrigger("AtaqueCetro");
        }else{

            if(espada == true){
                animator.SetTrigger("AtaqueEspada");
            }else if(arco == true){
                //anim.SetTrigger("AtaqueArco");
            }
            
        }
        //para acessar o script de todos os inimigos no mapa
            foreach(Collider2D target in targets){
                Debug.Log("We hit");
                target.GetComponent<LifeEnemy>().TakeDamage(attackDamage);
            }
        
        //StartCoroutine("Freeze");
    }

    void OnDrawGizmosSelected() 
    {   
        if(attackPoint == null) 
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    IEnumerator Freeze(){
        canMove = false;
        yield return new WaitForSeconds(.5f);
        canMove = true;
    }
}