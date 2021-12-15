using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /// <summary>
    /// Position from where the spawner starts
    /// </summary>
    Vector3 startPosition;

    /// <summary>
    /// Spawned items list
    /// </summary>
    List<GameObject> spawnedItems = new List<GameObject>();

    /// <summary>
    /// probabilities for each item 
    /// </summary>
    [Header("Probabilities")]
    public int goodItemProb;
    public int badItemProb;

    /// <summary>
    /// Goal reference
    /// </summary>
    public GameObject goal;

    /// <summary>
    /// Prefabs to spawn
    /// </summary>
    [Header("Prefabs")]
    public GameObject goodItemPrefab;
    public GameObject badItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Saves the start position
        startPosition = transform.position;
        // SpawnRandomItems();

        SpawnItems();
    }

    void SpawnItems()
    {
        //max of iterations
        int maxTimes =3;
        int times = 0;

        //from start to goal position
        for(int i = (int)startPosition.z; i < goal.transform.position.z && times < maxTimes; i++)
        {
           
            GameObject objectToSpawn;
            int randomNumber = Random.Range(1, 101);

            //Probabilities
            if (randomNumber <= goodItemProb)
            {
                objectToSpawn = Instantiate(goodItemPrefab, transform.position, Quaternion.identity, null);
                spawnedItems.Add(objectToSpawn);
            }

            else if (randomNumber > goodItemProb && randomNumber <= goodItemProb + badItemProb)
            {
                objectToSpawn = Instantiate(badItemPrefab, transform.position, Quaternion.identity, null);
                spawnedItems.Add(objectToSpawn);
            }

            //Moves to next position
            transform.position = new Vector3(transform.position.x, transform.position.y, i);

            //resets the position
            if(transform.position.z == (int)(goal.transform.position.z - 1))
            {
                transform.position = new Vector3(transform.position.x + 2, startPosition.y, startPosition.z);
                i = (int)startPosition.z;
                times++;
            }

        }
    }

}
