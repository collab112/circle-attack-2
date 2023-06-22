using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1 : MonoBehaviour
{

    int i = 0;

    // spawnTime = delay between enemy spawns
    // Type 0 - weak enemy, Type 1 - strong enemy
    float spawnTime;
    int spawnType;

    private GameObject levelManager;

    public Button play;
    
    private GameObject cashHandler;


    void Awake() {

        levelManager = gameObject.transform.parent.gameObject;
        play = (GameObject.Find("Play")).GetComponent<Button>();
        cashHandler = GameObject.Find("WorldHandler");

    }


    IEnumerator CallEnemies(float delay, int type) {

        yield return levelManager.GetComponent<Level>().SpawnEnemy(delay, type);

    } 


    IEnumerator Start() {
        
        play.enabled = false; // Disable play button during round

        // condition sets the amount of enemies in a round
        while ( i < 12 ) {

            // Altering spawn delay and type with the i variable to create unique rounds
            if ( i >= 0 && i <= 3) {

                spawnTime = 0.5f;
                spawnType = 0;
            
            } else if ( i > 3 && i < 7) {

                spawnTime = 0.2f;
                spawnType = 0;

            } else if ( i >= 7 ) {

                spawnTime = 0.7f;
                spawnType = 0;

            }
            
            // Call a coroutine that waits <spawnTime> amount of seconds and spawns a weak (0) or strong (1) enemy
            yield return CallEnemies(spawnTime, spawnType);

            // This runs after the coroutine is over
            i++;

        }

        // Turn off the game object after the round is over and re-enable the button and also grant 75 cash

        cashHandler.GetComponent<Cash>().updateCash(75);
        gameObject.SetActive(false);
        play.enabled = true;

    }

}
