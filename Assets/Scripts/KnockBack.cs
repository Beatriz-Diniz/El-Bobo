using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{   
    
    [SerializeField] private Rigidbody2D rb2d; //body u want to knock back
    
    //define the time and power to be used on the body
    [SerializeField] float time;
    [SerializeField] float power;

    [SerializeField] InimigoIA inimigo;

    //IMPORTANT: call this function on colision to aply the knock back
    public void knock(){
        
        StartCoroutine(Knockback(time, power, this.transform));
    }

    /* KnockDur - how long we add force
     * knockbackPower - force of the knock back
     * knockbackDir - direction of the knock back 
    */

    public IEnumerator Knockback(float KnockbackDuration, float KnockbackPower, Transform obj){
       //new Vector2(transform.position.x + raycastOffset.x, transform.position.y), Vector2.down, Color.blue)
        float timer = 0;
        while(KnockbackDuration > timer){
            Vector2 direcao;
            
            if(inimigo.get_chao() == true){
                if(obj.transform.position.x > 0){
                    direcao = new Vector2(obj.transform.position.x * -1, obj.transform.position.y);
                    rb2d.AddForce(direcao * (KnockbackPower+2));
                }else if(obj.transform.position.x < 0){
                    direcao = new Vector2(obj.transform.position.x, obj.transform.position.y);
                    rb2d.AddForce(direcao * KnockbackPower);
                }
                timer+= Time.deltaTime;
            }else{
                rb2d.AddForce(Vector2.up * 5);
            }
        }

        yield return 0;
    }

    /* exemple 
     * global variable - private KnockBack knockback;
     *
     * on startt - knockback = GameObject.FindGameObjectWithTag("Player").GetComponent<KnockBack>();
     *
     * On collision
     * if(obj.CompareTag("Player")) 
     *  knockback.knock();
     */
}
