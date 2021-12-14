using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Players speed, setteable by inspector
    /// </summary>
    int speed = 0;

    /// <summary>
    /// Player transform
    /// </summary>
    Transform playerTransform;

    public int Speed { get => speed; set => speed = value; }

    int score;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (speed > 0)
            MovePlayer();
    }

    /// <summary>
    /// Method to move the player always forwards
    /// </summary>
    void MovePlayer()
    {
        if (playerTransform)
        {
            //player movement
            playerTransform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameManager.Instance.goalTagName))
        {
            //TODO:zona de yard, quitar end game
            GameManager.Instance.EndGame();
        }
       
        /*//PRueba
        else if( other.CompareTag("Death"))
        {
            GameManager.Instance.GameOver();
        }*/
    }


    public void AddPoints(int pointsToAdd)
    {
        score = pointsToAdd;
    }
}
