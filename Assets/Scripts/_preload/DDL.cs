using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for objects that must continue to exist even when scenes are changed
public class DDL : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("Dont Destroy the object: " + gameObject.name);
    }
}
