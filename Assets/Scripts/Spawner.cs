using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //de momento solo los wall
    public GameObject wallToInstantiate;

    /// <summary>
    /// 
    /// </summary>
    public int wallMaxCount;

    /// <summary>
    /// 
    /// </summary>
    List<GameObject> walls = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < wallMaxCount; i++)
        {
            GameObject wall = Instantiate(wallToInstantiate, transform,false);
            wall.transform.position = Vector3.zero;
            walls.Add(wall);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
