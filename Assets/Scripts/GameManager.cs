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

    int playerScore;

    int multiplier = 0;

    //TODO: UI CONTROLLER¿?
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

    public enum GamePhase
    {
        Gameplay,
        RankingArea
    }

    [Header("Game logic")]
    public GamePhase currentPhase;

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

    public int Multiplier { set => multiplier = value; }


    // Start is called before the first frame update
    void Start()
    {
    }

    /// <summary>
    /// Method to start playing, called by button
    /// </summary>
    public void StartGame()
    {
        //disables canvas

        UIManager.Instance.ToggleTitleUI(false);

        if (player)
        {
            //sets the speed
            player.Initialise(playerZSpeed, playerXSpeed);
            currentPhase = GamePhase.Gameplay;

            UIManager.Instance.ToggleMovingTutorial(true);
        }
        else
        {
            Debug.LogError("Player is not asigned");
        }
    }

    /// <summary>
    /// Method to call when the player dies
    /// </summary>
    public void GameOver()
    {
        //shows game over
        UIManager.Instance.ToggleGameOverUI(true);

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

    /// <summary>
    /// Method to call at the end of the level
    /// </summary>
    public void EndGame()
    {
        //shows end menu
        UIManager.Instance.ToggleEndUI(true);

        if (player)
        {
            //Stops the player
            player.Stop();

            //TODO: CANVAS bueno

            //Sets the score of the player
            playerScore += CalculateScore();
            Debug.Log("PlayerScore " + playerScore);
        }
        else
        {
            Debug.LogError("Player is not asigned");
        }
    }

    /// <summary>
    /// Method to restart the game, called by button
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void startRankingArea()
    {
        currentPhase = GamePhase.RankingArea;
        Debug.Log("Entrando en ranking con: " + player.snowBalls.Count);
    }

    /// <summary>
    /// Method to set the player score
    /// </summary>
    int CalculateScore()
    {
        return player.Score * multiplier;
    }
}
