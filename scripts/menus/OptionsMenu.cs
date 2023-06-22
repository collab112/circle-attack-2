using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Needed for working with UI elements (Sliders, buttons)
using System.IO; // Needed for working with files
using UnityEngine.Audio; // Needed for working with audio mixers

public class OptionsMenu : MonoBehaviour
{

    string optionsData;
    // Filepath for saving data

    public Slider SFXVolume; 
    public Slider musicVolume; 
    // Allow this script to access the values in the sliders on the options

    public float[] volumeValues = new float[2]; // Declare a float array of length 2
    // A variable to store the data read from the files

    public AudioMixer SFXMixer;
    public AudioMixer MusicMixer;
    // Allow this script to access the audio mixers to adjust volume
    
    void Awake() {

        optionsData = Application.persistentDataPath + "options"; 
        // Set the filepath for where the options will be saved.
        // Application.persistentDataPath is defined automatically by Unity

    }

    // A function that checks if the two files exist, returning true if they do and
    // false if they don't
    public bool checkFile() { 
        
        if ( File.Exists(optionsData) ) {
            // Using File.Exists from System.IO to check if the file exists

            return true;

        } else { 

            return false;

        }

    }    

    // Create the files if they do not exist
    public void createFile() {

        if( !checkFile() ) { // Checks if the files exist or not

            File.WriteAllText(optionsData, "50 \n50"); 
            // Write to optionsData ".5\n.5". These will be the default values
            // for volume 
            
            /*/ / / / / / / / / / / / / / / / / / /
            *
            * Value 1 - Volume of sound effects
            *
            * Value 2 - Volume of music
            *
            / / / / / / / / / / / / / / / / / / /*/

        }

    }

    public void writeToFile() { // Write the new settings to the file

        File.WriteAllText(optionsData, SFXVolume.value + "\n" + musicVolume.value); 

        // Write the values on the sliders into the files

    }

    // Fetch the data from the files and put them into their corresponding variables
    public void getData() {

        string[] optionsValues = File.ReadAllLines(optionsData);

        for ( int i = 0; i <= 1; i++ ) { 
        // Iterate through the two values in optionsValues[] and
        // assign them to volumeValues[]  
            
            volumeValues[i] = float.Parse(optionsValues[i]);
            
        }        

        SFXVolume.value = volumeValues[0];
        musicVolume.value = volumeValues[1];

        // Read directly from the file as it only contains one line. This means
        // the string must be parsed to a float to be available to use in the slider

        // Assign the values of the sliders to the values from the file

    }

    void Start() {

    /*/ / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / 
    *
    * When the script instance is loaded, call createFile() to check if the files
    * exist or not and create them if they don't.  After this, call getData() to read
    * the data from the files. If they had not been created before, createFile() 
    * already accounts for this case as it will assign the default values to the files 
    * which are then read from the getData() function
    * 
    / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /*/

        createFile();
        getData();

    }

    void Update() {

        // Adjust the volume of the mixers with the value on the sliders

            SFXMixer.SetFloat("MasterVolSFX", (-80 + Mathf.Log(SFXVolume.value , 10 ) * 40) );
            MusicMixer.SetFloat("MasterVolMusic", (-80 + Mathf.Log(musicVolume.value , 10 ) * 40) );

            writeToFile();
            
            // Save the data each frame

    }

}
