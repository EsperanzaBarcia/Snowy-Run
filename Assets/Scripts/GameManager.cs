using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Player settings")]
    /// <summary>
    /// Reference to player, setted by inspector
    /// </summary>
    public Player player;

    [SerializeField]
    /// <summary>
    /// Players speed, setted by inspector
    /// </summary>
    int playerZSpeed;

    [SerializeField]
    /// <summary>
    ///
    /// </summary>
    int playerXSpeed;

    [Header("UI references")]
    /// <summary>
    /// Reference to Title canvas
    /// </summary>
    public GameObject titleUI;

    /// <summary>
    /// Reference to Title canvas
    /// </summary>
    public GameObject EndUI;

    /// <summary>
    /// Reference to Title canvas
    /// </summary>
    public GameObject GameOverUI;

    /// <summary>
    /// Tag asigned to goal gameobject
    /// </summary>
    public string goalTagName;

    /// <summary>
    /// Tag asigned to collected gameobject
    /// </summary>
    public string ballTagName;

    /// <summary>
    /// Tag asigned to wall gameobject
    /// </summary>
    public string wallTagName;

    #region Singleton pattern

    static GameManager _instance;

    public static GameManager Instance { get => _instance; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
    }

    /// <summary>
    /// Method to start playing, called by button
    /// </summary>
    public void StartGame()
    {
        if (titleUI)
        {
            //disables canvas
            titleUI.SetActive(false);

            if (player)
            {
                //sets the speed
                player.Initialise(playerZSpeed,playerXSpeed);
            }
            else
            {
                Debug.LogError("Player is not asigned");
            }

        }
        else
        {
            Debug.LogError("Title UI is not asigned");
        }


    }

    /// <summary>
    /// Method to call when the player dies
    /// </summary>
    public void GameOver()
    {
        if (GameOverUI)
        {
            //shows game over
            GameOverUI.SetActive(true);

            if (player)
            {
                //Stops the player
                player.Stop();

                //TODO: animation¿?
            }
            else
            {
                Debug.LogError("Player is not asigned");
            }
        }
        else
        {
            Debug.LogError("Game over UI is not asigned");
        }
    }

    /// <summary>
    /// Method to call at the end of the level
    /// </summary>
    public void EndGame()
    {
        if(EndUI)
        {
            //shows end menu
            EndUI.SetActive(true);

            if (player)
            {
                //Stops the player
                player.Stop();

                //TODO: CANVAS bueno
            }
            else
            {
                Debug.LogError("Player is not asigned");
            }
        }
        else
        {
            Debug.LogError("End level UI is not asigned");
        }
    }

    /// <summary>
    /// Method to restart the game, called by button
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
