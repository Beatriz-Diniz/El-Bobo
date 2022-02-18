using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vida : MonoBehaviour
{   
    public GameObject bobo;
    public UIManagerScript UIManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("vida: " + bobo.transform.GetComponent<SimpleMove>().health);
        
        if(bobo.transform.GetComponent<SimpleMove>().health < 10)
            bobo.transform.GetComponent<SimpleMove>().health += 1;
        
        Debug.Log("vida atualizada: " + bobo.transform.GetComponent<SimpleMove>().health);
        UIManagerScript.updateLives(bobo.transform.GetComponent<SimpleMove>().health);

        Destroy(gameObject);
        }
    }
}
