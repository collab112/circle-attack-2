using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level9 : MonoBehaviour
{

    int i = 0;

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
        
        play.enabled = false;

        while ( i < 42 ) {

            if ( i >= 0 && i <= 13) {

                spawnTime = 0.1f;
                spawnType = 0;
            
            } else if ( i > 13 && i < 27) {

                spawnTime = 0.2f;
                spawnType = 1;

            } else if ( i >= 27 ) {

                spawnTime = 0.1f;
                spawnType = 0;

            }
            
            yield return CallEnemies(spawnTime, spawnType);
            i++;

        }

        cashHandler.GetComponent<Cash>().updateCash(75);
        gameObject.SetActive(false);
        play.enabled = true;

    }

}
