using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Flecha : MonoBehaviour
{
    [SerializeField]
    private float velocidade;
    private Vector2 direcao;

    [SerializeField] private SimpleMove player; 

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
    }

    void FixedUpdate()
    {
        rb.velocity = direcao * velocidade;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            player.TakeDamage(1);
        }    
    }

    public void InicializarDirecao(Vector2 _direcao)
    {
        direcao = _direcao;
    }
}
