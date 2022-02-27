using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    [SerializeField]
    private Sprite [] livesSprites;


    [SerializeField]
    private Image livesImage;
   
    // Start is called before the first frame update
    void Start()
    {
        updateLives(10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateLives(int currentLives){
        this.GetComponent<Image>().sprite = livesSprites[currentLives];
    }
}
