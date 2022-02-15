using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour {

    private InimigoIA ia;
    public int StopTime = 1;
    bool waiting;

    void Start()
    {   
        ia = GameObject.FindGameObjectWithTag("enemy").GetComponent<InimigoIA>();
    }
    
    public void Stop(){
        if(waiting)
            return;
        StartCoroutine(Wait());
    }

    IEnumerator Wait(){
        waiting = true;
        ia.set_stun(true);
        yield return new WaitForSeconds(StopTime);
        Debug.Log("Wait");
        waiting = false;
        ia.set_stun(false);
    } 
}