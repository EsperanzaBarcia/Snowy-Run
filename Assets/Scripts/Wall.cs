/**
 * 
 * Created by Esperanza Barcia DEC 2021
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to control wall behaviour
public class Wall : MonoBehaviour
{
    /// <summary>
    /// Method to destroy wall
    /// </summary>
    public void DestroyWall()
    {
        //Disables the wall
        gameObject.SetActive(false);
    }
}
