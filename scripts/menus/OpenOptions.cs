using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOptions : MonoBehaviour
{

	public GameObject menuPanel;
    public GameObject optionsPanel;	// Allow this script to manage the panels
    public GameObject fileHandler;

    public void openOptionsMenu() { // Called on button "Options" click

        optionsPanel.SetActive(!optionsPanel.activeSelf); 
		menuPanel.SetActive(!menuPanel.activeSelf); // Toggle the panel on and off

        fileHandler.GetComponent<OptionsMenu>().writeToFile();

        /*/ / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / 
        *
        * Call a function from the script OptionsMenu that is attached to
        * the optionsPanel game object. The function is writeToFile() and
        * saves the data on the sliders into a file.
        *
        / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /*/

    }

}
