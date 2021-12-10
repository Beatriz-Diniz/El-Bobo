using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fog.Dialogue
{
    [RequireComponent(typeof(Collider2D))]
    public class Interactable : MonoBehaviour, IInteractable
    {
        [SerializeField] private Dialogue dialogue = null;

        public void Reset(){
            Debug.Log("Teste 1");
            Collider2D[] colliders = GetComponents<Collider2D>();
            int nColliders = colliders.Length;
            // Se so tem um collider, se certifica que ele seja trigger
            if(nColliders == 1){
                colliders[0].isTrigger = true;
            }else{
                bool hasTrigger = false;
                // Se tiver mais de um collider, verifica se ao menos um deles e trigger
                foreach(Collider2D col in colliders){
                    hasTrigger = col.isTrigger;
                    if(hasTrigger)
                        break;
                }
                // Se nenhum deles for, se certifica de que o primeiro deles seja trigger
                if(!hasTrigger)
                    colliders[0].isTrigger = true;
            }
        }

        public void OnInteractAttempt(){
            // TO DO: Parsear o dialogo de alguma maneira aqui dentro
            // Uma alternativa é não fazer o parsing aqui e sim no Start
            if(dialogue != null){
                DialogueHandler.instance.StartDialogue(dialogue);
            }
        }

        public void OnTriggerEnter2D(Collider2D col){
            Agent agent = col.GetComponent<Agent>();
            if(agent != null){
                agent.collidingInteractables.Add(this);
            }
        }

        public void OnTriggerExit2D(Collider2D col){
            Debug.Log("Teste 2");
            Agent agent = col.GetComponent<Agent>();
            if(agent != null){
                agent.collidingInteractables.Remove(this);
            }
        }

    }
}
