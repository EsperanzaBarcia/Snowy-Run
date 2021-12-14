using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    /// <summary>
    /// Points this ball gives to the player
    /// </summary>
    int points = 10;

    public int Points { get => points; set => points = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        //Adds points to player
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().AddPoints(points);
            gameObject.SetActive(false);
        }

        /*else if(other.CompareTag("Floor"))
        {
            gameObject.SetActive(false);
        }*/
    }
}
