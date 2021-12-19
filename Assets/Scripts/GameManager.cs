/**
 * 
 * Created by Esperanza Barcia DEC 2021
 * 
 */
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
    /// Players Zspeed, setted by inspector
    /// </summary>
    int playerZSpeed;

    [SerializeField]
    /// <summary>
    ///Players Xspeed, setted by inspector
    /// </summary>
    int playerXSpeed;

    /// <summary>
    /// player score, from player
    /// </summary>
    int playerScore;

    /// <summary>
    /// score multiplier
    /// </summary>
    int multiplier = 0;

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

    /// <summary>
    /// Enum to check the current game phase
    /// </summary>
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
        //Shows the title
        UIManager.Instance.ToggleTitleUI(true);
    }

    /// <summary>
    /// Method to start playing, called by button
    /// </summary>
    public void StartGame()
    {
        //disables initial canvas
        UIManager.Instance.ToggleTitleUI(false);

        //enables moving tutorial canvas
        UIManager.Instance.ToggleMovingTutorial(true);

        if (player)
        {
            //sets the speed
            player.Initialise(playerZSpeed, playerXSpeed);
            currentPhase = GamePhase.Gameplay;

        }
        else
        {
            Debug.LogError("Player is not asigned");
        }

    }

    /// <summary>
    /// Method to call when the player is defeated
    /// </summary>
    public void GameOver()
    {
        //shows game over
        UIManager.Instance.ToggleGameOverUI(true);

        if (player)
        {
            //Stops the player
            player.Stop();
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

            //Sets the score of the player
            playerScore += CalculateScore();

            //Shows points
            UIManager.Instance.SetFinalPoints(playerScore);

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

    /// <summary>
    /// Method to change game phase
    /// </summary>
    public void StartRankingArea()
    {
        currentPhase = GamePhase.RankingArea;
    }

    /// <summary>
    /// Method to calculate the player score
    /// </summary>
    int CalculateScore()
    {
        return player.Score * multiplier;
    }
}
