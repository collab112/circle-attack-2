using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed to manage scenes

public class MainMenu : MonoBehaviour
{

    public void playGame() { // Called on button "Play" click

        SceneManager.LoadScene(1); 

    }
    
    public void quitGame() { // Called on button "Quit" click 

        Application.Quit(); // Quit the game

    }

}
