using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenu : MonoBehaviour
{

    public bool gameIspaused = false;//This allow me to work with the paused game in other scripts.

    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
    }

    //Makes posible to pause or unpause the game by pressing escape.
    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                gameIspaused = true;
            }
            else if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                gameIspaused = false;
            }
        }

    }

}
