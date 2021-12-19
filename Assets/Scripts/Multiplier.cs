/**
 * 
 * Created by Esperanza Barcia DEC 2021
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///Class to handle multiplier behaviour
public class Multiplier : MonoBehaviour
{
    /// <summary>
    /// The value of this multiplier
    /// </summary>
    public int multiplyValue;

    /// <summary>
    /// Visual text
    /// </summary>
    public Text valueText;

    /// <summary>
    /// Balls to remove to the player when is colliding
    /// </summary>
    public int snowballsToRemove;

    private void Start()
    {
        if (valueText)
        {
            valueText.text = "x " + multiplyValue;
        }
        else
        {
            Debug.LogError("Text is not asigned");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Sets the multiplier and removes balls from the player
            GameManager.Instance.Multiplier = multiplyValue;

            collision.gameObject.GetComponent<Player>().RemoveSnowballs(
                snowballsToRemove);
        }
    }
}
