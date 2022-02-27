using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script para criar multiplos inimigos
public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate;     //tempo em segundos para criar um novo inimigo

    private float nextSpawn = 0f;
    
    public int limiteSlimes;
    private int count = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //criar um novo inimigo dentro do limite maximo de inimigos
        if(Time.time > nextSpawn && count < limiteSlimes){
            count++;
            nextSpawn = Time.time + spawnRate;
            Instantiate(enemy, transform.position, enemy.transform.rotation);
        }
    }
}
