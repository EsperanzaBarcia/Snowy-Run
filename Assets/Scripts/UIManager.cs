/**
 * 
 * Created by Esperanza Barcia DEC 2021
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class to control UI
public class UIManager : MonoBehaviour
{
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
    /// Reference to Tutorial canvas
    /// </summary>
    public GameObject TutorialUI;

    /// <summary>
    /// Reference to tutorial animator
    /// </summary>
    Animator tutorialAnimator;

    [Header("Visual texts")]
    public Text ScoreText;

    /// <summary>
    /// Snowballs count text
    /// </summary>
    public Text ballsText;

    /// <summary>
    /// Points at the end of the game text
    /// </summary>
    public Text finalPointsText;

    #region Singleton pattern

    static UIManager _instance;

    public static UIManager Instance { get => _instance; }

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
        if(TutorialUI)
        {
            //sets references
            tutorialAnimator = TutorialUI.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Tutorial UI not assigned");
        }

       

    }

    /// <summary>
    /// Method to show/hide moving tutorial
    /// </summary>
    /// <param name="show"></param>
    public void ToggleMovingTutorial(bool show)
    {
        if (tutorialAnimator)
        {
            int currentIndex = 0;

            if (show)
                currentIndex = 1;

            tutorialAnimator.SetInteger("TutorialIndex", currentIndex);
        }
        else
        {
            Debug.LogError("Tutorial animator not assigned");
        }
    }

    /// <summary>
    ///  Method to show/hide shooting tutorial
    /// </summary>
    /// <param name="show"></param>
    public void ToggleShootingTutorial(bool show)
    {
        if (tutorialAnimator)
        {
            int currentIndex = 0;

            if (show)
                currentIndex = 2;

            tutorialAnimator.SetInteger("TutorialIndex", currentIndex);
        }
        else
        {
            Debug.LogError("Tutorial UI not assigned");
        }
    }

    /// <summary>
    ///  Method to show/hide initial canvas
    /// </summary>
    /// <param name="show"></param>
    public void ToggleTitleUI(bool show)
    {

        if (titleUI)
        {
            if (show)
            {
                titleUI.SetActive(true);
                //hides texts
                ScoreText.transform.parent.gameObject.SetActive(false);
                ballsText.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                titleUI.SetActive(false);
                //hides texts
                ScoreText.transform.parent.gameObject.SetActive(true);
                ballsText.transform.parent.gameObject.SetActive(true);

            }

        }
        else
        {
            Debug.LogError("Title UI not assigned");
        }
    }

    /// <summary>
    ///  Method to show/hide  end ui
    /// </summary>
    /// <param name="show"></param>
    public void ToggleEndUI(bool show)
    {
        if (EndUI)
        {
            if (show)
                EndUI.SetActive(true);
            else
                EndUI.SetActive(false);

        }
        else
        {
            Debug.LogError("End UI not assigned");
        }
    }

    /// <summary>
    ///  Method to show/hide Game Over ui
    /// </summary>
    /// <param name="show"></param>
    public void ToggleGameOverUI(bool show)
    {
        if (GameOverUI)
        {
            if (show)
                GameOverUI.SetActive(true);
            else
                GameOverUI.SetActive(false);

        }
        else
        {
            Debug.LogError("Game over UI not assigned");
        }
    }

    /// <summary>
    /// Method to update score text
    /// </summary>
    /// <param name="newScore"></param>
    public void updateScore(int newScore)
    {
        ScoreText.text = newScore.ToString();
    }

    /// <summary>
    /// Method to update snowballs count text
    /// </summary>
    /// <param name="newBalls"></param>
    public void updateBalls(int newBalls)
    {
        ballsText.text = newBalls.ToString();
    }

    /// <summary>
    /// Method to update final points text
    /// </summary>
    /// <param name="points"></param>
    public void SetFinalPoints(int points)
    {
        finalPointsText.text = points.ToString();
    }
}
