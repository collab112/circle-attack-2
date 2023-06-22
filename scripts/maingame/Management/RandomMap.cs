using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomMap : MonoBehaviour
{

    public TileBase pathTile;
    public Tilemap pathMap;

    int randomnessInt; // Handle whether the path will go horizontal or vertical
    int randomDistance; 

    int i = 0;

    public GameObject nodeHandler;
    public GameObject nodeObject;

    void Awake() {

        nodeHandler = GameObject.Find("MapNodes");
        pathMap = GameObject.Find("Path").GetComponent<Tilemap>();

    }

    void Start() {

        drawPath( createNodes() );

    }

    public Vector3Int BetterNormalisedVector(float x, float y) {
		
		float vectorMagnitude = Mathf.Sqrt( x*x + y*y );
		float newX = x/vectorMagnitude;
		float newY = y/vectorMagnitude;
		
		return new Vector3Int((int)newX, (int)newY, 0);

        // See EnemyMove.cs for explanation of why this function exists
		
	}

    public void drawPath(List<Vector3Int> points) {

        int k;
        int l;

        Vector3Int direction;

        bool nextNodeReached;

        for (k = 0; k < points.Count; k++) {

            if ( k != points.Count - 1 ) {
        
                direction = new Vector3Int(points[k + 1].x - points[k].x, points[k + 1].y - points[k].y, 0);
                direction = BetterNormalisedVector(direction.x, direction.y);

                l = 0;
                nextNodeReached = false;

                Debug.Log("Iteration: " + k);

                Debug.Log(points[k]);
                Debug.Log((direction * l));
                Debug.Log(points[k] + (direction * l));

                while ( nextNodeReached != true ) {

                    if ( points[k] + (direction * l) == points[k+1] ) {

                        nextNodeReached = true;

                    } else  {

                        pathMap.SetTile( new Vector3Int( (points[k] + (direction * l)).x + 9, (points[k] + (direction * l)).y + 5, 0), pathTile);

                    }

                    Debug.Log(l);

                    l++;

                }

            }

        }

    }


    public bool checkTop(Vector3Int inputPos) {

        if (inputPos.y >= 4) {

            return true;

        } else {

            return false;

        }

    }

    public bool checkBottom(Vector3Int inputPos) {

        if (inputPos.y <= -5) {

            return true;

        } else {

            return false;

        }

    }


    public List<Vector3Int> createNodes() {

        List<Vector3Int> nodeList = new List<Vector3Int>();

        bool wentVertical = true;

        int j;

        // check these values in the editor later
        nodeList.Add(new Vector3Int(-10, Random.Range(0, 10) - 5, 0));

        while (nodeList[i].x < 7) {    

            randomnessInt = Random.Range(0, 3);

            if ( randomnessInt == 0 ) {

                wentVertical = false;
        
                randomDistance = Random.Range(3, 7);
                nodeList.Add(new Vector3Int(nodeList[i].x + randomDistance, nodeList[i].y, 0)); 
                Debug.Log("0");
        
            } else if ( wentVertical ) {

                continue;

            } else {

                wentVertical = true;
        
                randomDistance = Random.Range(-6, 7);

                // Make sure this value is never 0  
                while (randomDistance == 0) {

                    randomDistance = Random.Range(-6, 7);

                }

                if ( checkTop(nodeList[i]) ) {

                    randomDistance = Random.Range(-7, -4);
                    nodeList.Add(new Vector3Int(nodeList[i].x, nodeList[i].y + randomDistance, 0));

                } else if (checkBottom(nodeList[i])) {

                    randomDistance = Random.Range(5, 7);
                    nodeList.Add(new Vector3Int(nodeList[i].x, nodeList[i].y + randomDistance, 0));

                } else {

                    if ( nodeList[i].y + randomDistance > 4 ) {

                        nodeList.Add(new Vector3Int(nodeList[i].x, 4, 0)); 

                    } else if ( nodeList[i].y + randomDistance < -5 ) {

                        nodeList.Add(new Vector3Int(nodeList[i].x, -5, 0)); 

                    } else {

                        nodeList.Add(new Vector3Int(nodeList[i].x, nodeList[i].y + randomDistance, 0));

                    }

                }

            }

            i++;

        }

        nodeList[nodeList.Count-1] = new Vector3Int(7, nodeList[nodeList.Count-1].y, 0);

        for ( j = 0; j < nodeList.Count; j++ ) {

            var nodeTransformObject = Instantiate(nodeObject, nodeList[j], Quaternion.identity, nodeHandler.transform);

        }

    return nodeList;

    }

}