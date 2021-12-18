using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Multiplier : MonoBehaviour
{
    /// <summary>
    /// The value of this multiplier
    /// </summary>
    public int multiplyValue;

    public Text valueText;

    public int snowballsToRemove;

    private void Start()
    {
        if(valueText)
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
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.Multiplier = multiplyValue;
            //TODO:HARDCODE
            collision.gameObject.GetComponent<Player>().RemoveSnowballs(
                snowballsToRemove);
        }
    }
}
