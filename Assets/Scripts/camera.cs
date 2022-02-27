using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField]
    private float maximoX;
    [SerializeField]
    private float minimoX;
    [SerializeField]
    private float maximoY;
    [SerializeField]
    private float minimoY;

    public Transform Player;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(!GameController.instance.morto)    //fazer a condição pra ver se está morto
            transform.position = new Vector3(Mathf.Clamp(Player.position.x, minimoX, maximoX), Mathf.Clamp(Player.position.y, minimoY, maximoY), transform.position.z);
    }
}
