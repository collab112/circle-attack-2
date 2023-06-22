using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    private Vector3 spawnLocation = new Vector3(100, 100, 100);


    public IEnumerator SpawnEnemy(float delay, int enemyType) {

        if (enemyType == 0) {

            GameObject enemy = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);

        } else if (enemyType == 1) {

            GameObject enemy = Instantiate(enemyPrefab2, spawnLocation, Quaternion.identity);
        
        }

        yield return new WaitForSeconds(delay);
        
    }

}

  
