using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MudarCena : MonoBehaviour
{       public Animator animator;
        public GameObject menu;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {       
                
        }
        
        public void IniciaTransicao()
        {       
                animator.SetTrigger("Inicia");
        }

        public void DesativarMenu(){
                menu.SetActive(false); 
        }

        public void AtivarMenu(){
                menu.SetActive(true); 
        }

        public void DesativarTela(){
                gameObject.SetActive(false);
        }

        public void AtivarTela(){
                gameObject.SetActive(true);
        }

        /*para mudar de cena quando interagir com uma porta ou quando clicar no botao*/
        public void proximaCena(){
                //carrega a proxima cena          
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Destroy(gameObject);
        }

        public void Sair(){
                Application.Quit();
        }

        /*para mudar de cena quando passar em um checkpoint*/
        void OnTriggerEnter2D(Collider2D other){

                //carrega a proxima cena     
                if(other.gameObject.CompareTag("Player"))
                        IniciaTransicao();
                       
        }


}
