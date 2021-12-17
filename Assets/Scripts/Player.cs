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
    /// Reference to character
    /// </summary>
    public GameObject visualBall;

    /// <summary>
    /// 
    /// </summary>
    CharacterController characterScript;

    //DEBUG
    public Text debugText;

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

                        //if its on play mode and not moving shoots
                        if (!isTouchingMovingArea() && GameManager.Instance.currentPhase == GameManager.GamePhase.Gameplay)
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
            debugText.text = "dir: " + direction.x + " " + (playerTransform.forward + direction) + touch.phase.ToString();
        }
    }

    /// <summary>
    /// Method to increase score and balls count
    /// </summary>
    /// <param name="pointsToAdd"></param>
    public void AddPoints(int pointsToAdd, GameObject ball)
    {
        _score += pointsToAdd;

        if (ball)
        {
            //Now the player is the parent
            ball.transform.SetParent(playerTransform);

            //Position from which is going to be shot
            ball.transform.localPosition = new Vector3(0, 0, 1);

            //is added to the snowballs list
            snowBalls.Add(ball);

            if (visualBall)
            {
                if (snowBalls.Count % 5 == 0)
                    //TODO:HARDCODE
                    StartCoroutine(IncreaseBall(.1f));
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

                    StartCoroutine(DecreaseBall(.05f));

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
                    StartCoroutine(DecreaseBall(.1f));
                }

            }
            else if (snowBalls.Count <= count)
            {
                snowBalls.Clear();

                //TODO:hardCODE
                //TODO:CAMBIAR VALOR
                StartCoroutine(DecreaseBall(1f));

                GameManager.Instance.EndGame();
            }
        }
    }

    /* private void DecreaseBall(float scaleToDecrease)
     {
         int scalingFrames = 10;
         //TODO:HARDCODE
         if (visualBall.transform.localScale.x - scaleToDecrease > .1f)
         {
             // visualBall.transform.localScale -= new Vector3(scaleToDecrease, scaleToDecrease, scaleToDecrease);
             Vector3.Lerp(visualBall.transform.localScale, visualBall.transform.localScale - new Vector3(scaleToDecrease, scaleToDecrease, scaleToDecrease),);

         }
         //TODO:HARDCODE
         // visualBall.transform.localScale -= new Vector3(scaleToDecrease, scaleToDecrease, scaleToDecrease);
         else
             //visualBall.transform.localScale = new Vector3(.1f, .1f, .1f);
             //Vector3.Lerp(visualBall.transform.localScale, new Vector3(.1f, .1f, .1f), 1);
             //visualBall.transform.localScale = new Vector3(.1f, .1f, .1f);
     }*/

    //TODO: HACER SCRIPT BALL
    IEnumerator IncreaseBall(float scaleToIncrease)
    {
        float elapsedTime = 0;
        float seconds = 1;

        Vector3 currentScale = visualBall.transform.localScale;
        Vector3 finalScale;

        if (currentScale.x + scaleToIncrease < 6f)
        {
            finalScale = currentScale + new Vector3(scaleToIncrease, scaleToIncrease, scaleToIncrease);
        }
        else
        {
            //minimun size
            finalScale = new Vector3(6f, 6f, 6f);
        }

        while (elapsedTime < 1)
        {
            visualBall.transform.localScale = Vector3.Lerp(currentScale, finalScale, elapsedTime / seconds);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }


    IEnumerator DecreaseBall(float scaleToDecrease)
    {
        float elapsedTime = 0;
        float seconds = 1;

        Vector3 currentScale = visualBall.transform.localScale;
        Vector3 finalScale;

        if (currentScale.x - scaleToDecrease > 1f)
        {
            finalScale = currentScale - new Vector3(scaleToDecrease, scaleToDecrease, scaleToDecrease);
        }
        else
        {
            //minimun size
            //TODO:HARDCODE
            finalScale = new Vector3(.1f, .1f, .1f);
        }

        while (elapsedTime < 1)
        {
            visualBall.transform.localScale = Vector3.Lerp(currentScale, finalScale, elapsedTime / seconds);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }


}
