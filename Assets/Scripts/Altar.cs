using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    public GameObject Spawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InvocarSlimes(){
        Debug.Log("Invocar Slimes");
        Spawner.SetActive(true);
    }
}
