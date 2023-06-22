using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed to manage scenes 
using UnityEngine.UI;

// Elements of this script were taken from a previous project
public class PauseMenu : MonoBehaviour
{

    public bool paused; 

    public GameObject escapeMenu; // Allow this script to open and close the escape menu
	
	public Button button1;
	public Button button2;
	public Button button3;

    public Button playButton;

    void Start() { // When the script is loaded, set the paused variable to false

        paused = false;

    }

    public void PlayPauseGame() { // Check if paused is true or false

        if (paused == true) {
            
            pause();
        
        } else {
            
            resume();            
        
        }
    }

    public void quitToMenu() { // Load the main menu scene

        SceneManager.LoadScene(0);

    }

    public void pause() { 

        Time.timeScale = 0; // Set the timescale to 0 (Time does not go forward)
        paused = true; 

        escapeMenu.SetActive(true); // Set the menu object active 
		button1.GetComponent<Button>().enabled = false;
		button2.GetComponent<Button>().enabled = false;
		button3.GetComponent<Button>().enabled = false;

        playButton.enabled = false;
		
    }

    public void resume() {

        Time.timeScale = 1; // Time will run normally
        paused = false; 

        escapeMenu.SetActive(false);
		button1.GetComponent<Button>().enabled = true;
		button2.GetComponent<Button>().enabled = true;
		button3.GetComponent<Button>().enabled = true;

        playButton.enabled = true;

    }

    void Update() {

        if ( Input.GetKeyDown(KeyCode.Escape) ) { // Check if escape is pressed

            paused = !paused; // Toggle the paused variable

        }

        PlayPauseGame();

    }

}
