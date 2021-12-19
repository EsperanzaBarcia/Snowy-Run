/**
 * 
 * Created by Esperanza Barcia DEC2021
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to handle snowballs interaction
public class Ball : MonoBehaviour
{
    /// <summary>
    /// Points this ball gives to the player, can be asigned by inspector
    /// </summary>
    public int points = 10;

    /// <summary>
    /// Boolean to check if the ball is being shot
    /// </summary>
    public bool isBullet;


    private void OnTriggerEnter(Collider other)
    {
        //Adds points to player in the case the ball is not used as bullet
        if(other.CompareTag("Player"))
        {  
            if(!isBullet)
            {
                other.GetComponent<Player>().AddPoints(points, gameObject);
                gameObject.SetActive(false);
            }     
        }
        //disables itself once is shot
        else if(other.CompareTag("Limit"))
        {
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Wall"))
        {
            other.GetComponent<Wall>().DestroyWall();
        }
    }
}
