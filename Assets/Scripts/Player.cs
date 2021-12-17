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
    int _score;

    /// <summary>
    /// Force to impulse the bullets
    /// </summary>
    int bulletForce = 15;

    /// <summary>
    /// List of ready bullets to shoot
    /// </summary> //TODO:PONER PRIVADA
    public List<GameObject> snowBalls = new List<GameObject>();

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

    /// <summary>
    /// Reference to character
    /// </summary>
    public GameObject visualCharacter;

    /// <summary>
    /// Reference to ball
    /// </summary>
    public PlayerBallController visualBall;

    /// <summary>
    /// 
    /// </summary>
    CharacterController characterScript;

    /// <summary>
    /// Variable to check if this are the first touches of the user
    /// </summary>
    int touches;

    bool firstShot;

    public int Score { get => _score; }

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        //TODO:Empezar con una cierta cantidad de bolas

        if (visualCharacter)
        {
            characterScript = visualCharacter.GetComponent<CharacterController>();
        }

        if (!visualBall)
            Debug.LogError("Visual ball is not asigned");

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
                        startPosition = touch.position;

                        break;
                    }
                //Stops moving and if is on shooting area shoots
                case TouchPhase.Ended:
                    {
                        direction.x = 0;
                        startPosition = Vector2.zero;

                        if (GameManager.Instance.currentPhase == GameManager.GamePhase.Gameplay && touch.position.y > Screen.width / 2)
                            Shoot();
                        //if its on play mode and not moving shoots

                        break;
                    }

                case TouchPhase.Stationary:
                    {
                        if (touches < 2)
                        {
                            touches++;
                        }
                        else
                        {
                            UIManager.Instance.ToggleMovingTutorial(false);
                        }

                        if (touch.position.x > Screen.width / 2 && touch.position.y <= Screen.width / 2)//right
                        {
                            direction.x = 1;
                        }
                        else if (touch.position.x < Screen.width / 2 && touch.position.y <= Screen.width / 2)//left
                        {
                            direction.x = -1;
                        }

                        break;
                    }
            }
        }

        //The player always moves forwards
        //if (_zSpeed > 0)
        Move();

    }

    private void OnMouseDown()
    {
        /*if (GameManager.Instance.currentPhase == GameManager.GamePhase.Gameplay)
            Shoot();*/
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

        if (characterScript)
        {
            characterScript.Run();
        }

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
            // debugText.text = "dir: " + direction.x + " " + (playerTransform.forward + direction) + touch.phase.ToString();
        }
    }

    /// <summary>
    /// Method to increase score and balls count
    /// </summary>
    /// <param name="pointsToAdd"></param>
    public void AddPoints(int pointsToAdd, GameObject ball)
    {
        if (!firstShot)
        {
            UIManager.Instance.ToggleShootingTutorial(true);
            firstShot = true;
        }

        _score += pointsToAdd;

        if (ball)
        {
            //Now the player is the parent
            ball.transform.SetParent(playerTransform);

            //Position from which is going to be shot
            ball.transform.localPosition = new Vector3(0, 1, 1);

            //is added to the snowballs list
            snowBalls.Add(ball);

            if (visualBall)
            {
                //if (snowBalls.Count % 5 == 0)
                //TODO:HARDCODE
                visualBall.StartIncreasingBall(.5f);

                if (GetComponent<SphereCollider>().radius < visualBall.GetParentSize() + .5f)
                {
                    GetComponent<SphereCollider>().radius = (visualBall.GetParentSize() + .5f) / 2;
                    GetComponent<SphereCollider>().center = new Vector3(0, visualBall.GetParentSize() / 2, 0);
                    //GetComponent<CapsuleCollider>().center += new Vector3(0, .05f, 0);
                }

            }
            else
            {
                Debug.LogError("Visual ball is not asigned");
            }

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

                    visualBall.StartDecreasingBall(.05f);

                    if (GetComponent<SphereCollider>().radius > visualBall.GetParentSize() - .05f)
                    {
                        GetComponent<SphereCollider>().radius = (visualBall.GetParentSize() - .05f) / 2;
                        GetComponent<SphereCollider>().center = new Vector3(0, visualBall.GetParentSize() / 2, 0);
                    }
                }

            }

            UIManager.Instance.ToggleShootingTutorial(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameManager.Instance.goalTagName))
        {
            GameManager.Instance.EndGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag(GameManager.Instance.wallTagName))
        {
            //TODO:HARDCODE
            if (visualBall.GetParentSize() >= visualBall.bigSize)
            {
                collision.gameObject.GetComponent<Wall>().DestroyWall();
                //TODO:HARDCODE
                RemoveSnowballs(10);
            }
            else
            {
                GameManager.Instance.GameOver();
            }

        }
    }

    public void RemoveSnowballs(int count)
    {
        if (GameManager.Instance.currentPhase == GameManager.GamePhase.RankingArea)
        {
            if (snowBalls.Count >= count)
            {
                //TODO: HARDCODE
                snowBalls.RemoveRange(0, count);
                _zSpeed--;
                Debug.Log("Ahora" + snowBalls.Count);

                for (int i = 0; i < count; i++)
                {
                    //TODO:hardCODE
                    visualBall.StartDecreasingBall(.5f);
                }

            }
            else if (snowBalls.Count <= count && snowBalls.Count > 0)
            {
                snowBalls.RemoveAt(0);
                visualBall.StartDecreasingBall(.5f);
            }
            else
            {
                snowBalls.Clear();

                //TODO:hardCODE
                //TODO:CAMBIAR VALOR
                visualBall.StartDecreasingBall(1f);

                GameManager.Instance.EndGame();
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                //TODO:hardCODE
                visualBall.StartDecreasingBall(.5f);
            }
        }
    }

}
