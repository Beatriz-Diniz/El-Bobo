using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Novo Dialogo", menuName = "Dialogo/Conversa")]
public class Conversa : ScriptableObject
{
    public FalasDaConversa[] Falas;
}

[System.Serializable]
public class FalasDaConversa
{   
    public Personagem Personagem;
    
    [TextArea]
    public string[] TextoDasFalas;  
}

