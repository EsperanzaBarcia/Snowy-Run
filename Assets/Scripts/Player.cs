using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Players horizontal speed, setteable by inspector
    /// </summary>
    int _xSpeed = 0;

    /// <summary>
    /// Players depth speed, setteable by inspector
    /// </summary>
    int _zSpeed = 0;

    /// <summary>
    /// Player transform
    /// </summary>
    Transform playerTransform;

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

    /// <summary>
    /// 
    /// </summary>
    Touch touch;

    /// <summary>
    /// bool to set when the player can move
    /// </summary>
    bool canMove;

    /// <summary>
    /// Direction to move the player
    /// </summary>
    Vector3 direction = Vector3.zero;


    //DEBUG
    public Text debugText;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        //TODO:Empezar con una cierta cantidad de bolas
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal movement by touch
        if (Input.touchCount > 0 && canMove)
        {
            touch = Input.GetTouch(0);
            Vector2 startPosition = Vector2.zero;

            //Otherwise, moves 

            switch (touch.phase)
            {
                //saves the touch if it is the moving area
                case TouchPhase.Began:
                    {
                        if (isTouchingMovingArea())
                            startPosition = touch.position;

                        break;
                    }

                //Calculates the direction of the movement
                case TouchPhase.Moved:
                    {
                        if (isTouchingMovingArea())
                        {
                            if (touch.deltaPosition.x > startPosition.x)//right
                            {
                                direction.x = 1;
                            }
                            else if (touch.deltaPosition.x < startPosition.x)//left
                            {
                                direction.x = -1;
                            }
                        }
                        break;
                    }

                //Stops moving and if is on shooting area shoots
                case TouchPhase.Ended:
                    {
                        direction.x = 0;
                        startPosition = Vector2.zero;

                        if (!isTouchingMovingArea())
                            Shoot();
                        break;
                    }
            }
        }

        //The player always moves forwards
        //if (_zSpeed > 0)
            Move();

    }

    /// <summary>
    /// Method to start moving the player
    /// </summary>
    /// <param name="Zspeed"></param>
    /// <param name="Xspeed"></param>
    public void Initialise(int Zspeed, int Xspeed)
    {
        _zSpeed = Zspeed;
        _xSpeed = Xspeed;
        canMove = true;
    }

    /// <summary>
    /// Method to stop the player
    /// </summary>
    public void Stop()
    {
        _zSpeed = 0;
        _xSpeed = 0;
        canMove = false;
    }

    /// <summary>
    /// Method to move the player always forwards
    /// </summary>
    void Move()
    {
        if (playerTransform)
        {
            //player movement
            playerTransform.Translate((playerTransform.forward * _zSpeed + direction * _xSpeed) * Time.deltaTime);
            debugText.text = "dir: " + direction.x + " " + (playerTransform.forward + direction) + touch.phase.ToString();
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
        debugText.text = "shoot";

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
                    /* if (snowBalls.Count == 0)
                     {
                         GameManager.Instance.GameOver();
                     }*/
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

    /// <summary>
    /// Method to check if the player is touching move area
    /// if it is not, it is touching shooting area
    /// </summary>
    /// <returns></returns>
    bool isTouchingMovingArea()
    {
        return touch.position.y <= Screen.height / 3;
    }
}
