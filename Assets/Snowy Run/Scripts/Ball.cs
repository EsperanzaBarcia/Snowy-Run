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
        if(other.CompareTag(GameManager.Instance.playerTagName))
        {  
            if(!isBullet)
            {
                other.GetComponent<Player>().AddPoints(points, gameObject);
                DisableMyself();
            }     
        }
        //disables itself once is shot
        else if(other.CompareTag(GameManager.Instance.limitTagName))
        {
            DisableMyself();
        }
        else if (other.CompareTag(GameManager.Instance.wallTagName))
        {
            DisableMyself();
            other.GetComponent<Wall>().DestroyWall();
        }
    }

    void DisableMyself()
    {
        gameObject.SetActive(false);
    }
}
