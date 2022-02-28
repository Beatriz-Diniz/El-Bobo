using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class SistemaDialogo : MonoBehaviour
{
    [SerializeField] private GameObject _caixaDeDialogo;

    [SerializeField] private LetraPorLetra _letraPorLetra;
    [SerializeField] private TextMeshProUGUI _nomePersonagem;
    public Color[] color;

    public Conversa _conversaAtual;
    private int _indiceFalas;
    public int _indiceColor;
    private Queue<string> _filaFalas;    
    public MudarCena transicao;

    void Start()
    {
        IniciarDialogo(_conversaAtual);
    }

    public void IniciarDialogo(Conversa conversa)
    {   
        //Faz aparecer a caixa de diálogo
        _caixaDeDialogo.SetActive(true);

        //Inicializa a fila
        _filaFalas = new Queue<string>();

        _conversaAtual = conversa;
        _indiceFalas = 0;
        _indiceColor = 0;
        
        ProximaFala();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space))
            ProximaFala();
    }

    public void ProximaFala()
    {
        if(_letraPorLetra.EstaMostrando)
        {
            _letraPorLetra.MostrarTextoTodo();
            return;
        }

        if(_filaFalas.Count == 0)
        {
            if(_indiceFalas < _conversaAtual.Falas.Length)
            {

                //Coloca o nome do personagem na caixa de diálogo
                _nomePersonagem.color = color[_indiceColor];
                _nomePersonagem.text = _conversaAtual.Falas[_indiceFalas].Personagem.Nome;
                
                //Coloca todas as falas da expressão atual em uma fila
                foreach (string textoFala in _conversaAtual.Falas[_indiceFalas].TextoDasFalas)
                {   
                    _filaFalas.Enqueue(textoFala);
                }

                
                _indiceFalas++;
            }
            else
            {   
                //Faz sumir a caixa de diálogo
                _caixaDeDialogo.SetActive(false);
                transicao.IniciaTransicao();
                return;
            }
        }

        _letraPorLetra.MostrarTextoLetraPorLetra(_filaFalas.Dequeue());
        _indiceColor++;
    }
}