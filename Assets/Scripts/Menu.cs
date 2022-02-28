using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{   
    public bool isPaused;

    //painel do menu
    public GameObject pausePanel;

    //painel de opcoes
    public GameObject optionsPanel;


    //volume
    public AudioMixer audioMixer;

    //resolucao
    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;


    //verificar se o player morreu
    public GameObject mainMenu;
    public bool morte;

    public MudarCena transicao;
    public GameObject tra;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        transicao = GameObject.FindObjectOfType<MudarCena>();
        morte = mainMenu.transform.GetComponent<MainMenu>().morte;
        //limpar o botao Dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        //criar uma lista de opcoes
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        //adicionar na lista as opcoes de resolucoes
        for(int i = 0; i<resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        
        //adicionar as opcoes de resolucao o botao Dropdown
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        //verificar se player morreu para o menu de pause nao ser ativado com esc e misturar com o menu de morte
        morte = mainMenu.transform.GetComponent<MainMenu>().morte;

        //para pausar ou despausar o jogo quando apertar o esc e o menu de opcoes nao estiver ativado
        if(Input.GetKeyDown(KeyCode.Escape) && optionsPanel.activeSelf == false && morte == false)
            PauseScreen();        
        
        //para sair do menu de opcoes quando apertar o esc e voltar para o menu principal
        if(Input.GetKeyDown(KeyCode.Escape) && optionsPanel.activeSelf == true){
            optionsPanel.SetActive(false);
            pausePanel.SetActive(true); 
        }

    }

    public void PauseButton(){

        if(pausePanel.activeSelf && morte == false)
            pausePanel.SetActive(false);

        if(!optionsPanel.activeSelf && !pausePanel.activeSelf && morte == false)
            PauseScreen();
        
        if(optionsPanel.activeSelf && morte == false)
            BacktoMenu();
    }

    /*pausa o jogo, se tiver pausado volta para o jogo*/
    void PauseScreen(){
        if(isPaused){
            isPaused = false;
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }else{           
            isPaused = true;
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
    }


    /*funcoes para o menu principal*/
    //sair do menu de pause e continuar o jogo
    public void Continuar(){
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void Iniciar(){
        Debug.Log("Entrou Iniciar");
        //tra.SetActive(true);
        transicao.IniciaTransicao();
    }


    //sair do jogo
    public void QuitGame() {  
        //no editor da unity, quando compilar o executavel, deixar essa parte comentada
        //UnityEditor.EditorApplication.isPlaying = false;

        //no executavel, quando compilar na Unity, deixar essa parte comentada
        Application.Quit();
    }

    //abrir menu de configuracoes
    public void Options(){
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);    
    }


    /*funcoes do menu de opcoes*/
    //voltar para o menu principal
    public void BacktoMenu(){
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    //configurar o volume do jogo
    public void SetVolume(float volume){
        audioMixer.SetFloat("volume", volume);
    }

    //mudar qualidade grafica
    public void SetQuality(int qualityIndex){
        Debug.Log(qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //colocar ou sair da tela cheia
    public void SetFullscreen(bool isFullscreen){
        Screen.fullScreen = isFullscreen;
    }

    //mudar resolucao da tela
    public void SetResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
