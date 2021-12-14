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
    /// Balls collected //when its 5 it grows
    /// </summary>
    int ballsCollected;

    /// <summary>
    /// Bullet
    /// </summary>
    GameObject bullet;

    /// <summary>
    /// Bullet rigidbody reference
    /// </summary>
    Rigidbody bulletRb;

    int bulletForce = 15;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();

        //bullet settings
        if(GameManager.Instance.bulletPrefab)
        {
            bullet = Instantiate(GameManager.Instance.bulletPrefab,transform);

            //Set te position so the ball doesnt touch the player
            bullet.transform.localPosition = new Vector3(0, 0, .5f);
            bullet.SetActive(false);

            bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.useGravity = true;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (speed > 0)
            MovePlayer();

        //TODO:Cambiar por click
        if (Input.GetKeyDown(KeyCode.Space))
        Shoot();
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
    public void AddPoints(int pointsToAdd)
    {
        score = pointsToAdd;
        ballsCollected += 1;
    }

    /// <summary>
    /// Method to shoot
    /// </summary>
    public void Shoot()
    {
        Debug.Log("shoot");
        if(ballsCollected > 0)
        {
            bullet.SetActive(true);
            bulletRb.AddForce(new Vector3(0, 0, bulletForce), ForceMode.Impulse);
            ballsCollected -= 1;

            if(ballsCollected==0)
                GameManager.Instance.GameOver();

           /* bullet.SetActive(false);
            bullet.transform.localPosition = new Vector3(0, 0, .5f);*/

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
