using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rounds : MonoBehaviour
{
	
    // Set to -1 at the start because round gets incremented at the start of sendNextRound() so this would make it start at 0 
    public int round = -1; 

    public GameObject worldManager;

    public GameObject panel;
	public Button button1;
	public Button button2;
	public Button button3;

    public Text roundsText;


    public void sendNextRound(Button playButton) {

        round++;

        roundsText.text = "ROUND " + (round + 1);

        if (round < 10) { // Win condition is if round >= 10 (in the else statement)

            worldManager.transform.GetChild(round).gameObject.SetActive(true); 

            /*/ / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / 
            *
            * The worldManager has a list of child game objects that are all 
            * set to off. Each one has a level script attached to it. The Start()
            * method runs whenever the game object is set to active, and the script
            * disables the game object by itself at the end of the round, so all this
            * script needs to do is turn it on once.
            *
            / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /*/

        } else { // If the player has passed round 10 (won the game)

            gameObject.GetComponent<Score>().getFinalScore(true); // Calculate the final score

            // Set the panel containing score and menu button on and then turn off all the buttons
            panel.SetActive(true);
			
			button1.GetComponent<Button>().enabled = false;
			button2.GetComponent<Button>().enabled = false;
			button3.GetComponent<Button>().enabled = false;

            playButton.GetComponent<Button>().enabled = false;

        }

    }

}
