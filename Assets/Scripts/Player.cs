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

    /// <summary>
    /// Player score
    /// </summary>
    int score;

    /// <summary>
    /// Force to impulse the bullets
    /// </summary>
    int bulletForce = 15;

    /// <summary>
    /// List of ready bullets to shoot
    /// </summary>
    List<GameObject> snowBalls = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        //TODO:Empezar con una cierta cantidad de bolas

    }

    // Update is called once per frame
    void Update()
    {
        //TODO:Cambiar por click
        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();

        if (Input.GetKeyDown(KeyCode.A))
            playerTransform.Translate(Vector3.left+ Vector3.forward * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.D))
            playerTransform.Translate(Vector3.right + Vector3.forward * speed * Time.deltaTime);

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

    /// <summary>
    /// Method to increase score and balls count
    /// </summary>
    /// <param name="pointsToAdd"></param>
    public void AddPoints(int pointsToAdd, GameObject ball)
    {
        score += pointsToAdd;

        if (ball)
        {
            //Now the player is the parent
            ball.transform.SetParent(playerTransform);

            //Position from witch is going to be shot
            ball.transform.localPosition = new Vector3(0, 0, 1);

            //is added to the snowballs list
            snowBalls.Add(ball);
        }
        else
        {
            Debug.LogError("Ball is null");
        }

    }

    /// <summary>
    /// Method to shoot
    /// </summary>
    public void Shoot()
    {
        Debug.Log("shoot");

        if (snowBalls.Count > 0)
        {
            //Gets the first ball available
            GameObject tempBullet = snowBalls[0];

            if (tempBullet)
            {
                //Shows the ball
                tempBullet.SetActive(true);

                Rigidbody tempRb = tempBullet.GetComponent<Rigidbody>();

                if (tempRb)
                {
                    //Applies the impulse
                    tempRb.AddForce(bulletForce * tempBullet.transform.forward, ForceMode.Impulse);
                    tempRb.useGravity = true;

                    //Removes the ball from the list and sorts it to use next
                    snowBalls.Remove(tempBullet);

                    //if the player is out of balls dies
                    if (snowBalls.Count == 0)
                    {
                        GameManager.Instance.GameOver();
                    }
                }

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameManager.Instance.goalTagName))
        {
            //TODO:zona de yard, quitar end game
            GameManager.Instance.EndGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag(GameManager.Instance.wallTagName))
        {
            GameManager.Instance.GameOver();
        }
    }

}
