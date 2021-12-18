using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    /// 
    /// </summary>
    public GameObject TutorialUI;

    /// <summary>
    /// 
    /// </summary>
    Animator tutorialAnimator;

    public Text ScoreText;
    public Text ballText;

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
        tutorialAnimator = TutorialUI.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

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
            Debug.LogError("Tutorial UI not assigned");
        }
    }

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

    public void ToggleTitleUI(bool show)
    {
        ToggleMovingTutorial(false);
        ToggleShootingTutorial(false);

        if (titleUI)
        {
            if (show)
                titleUI.SetActive(true);
            else
                titleUI.SetActive(false);

        }
        else
        {
            Debug.LogError("Title UI not assigned");
        }
    }


    public void ToggleEndUI(bool show)
    {
        ToggleMovingTutorial(false);
        ToggleShootingTutorial(false);

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


    public void ToggleGameOverUI(bool show)
    {
        ToggleMovingTutorial(false);
        ToggleShootingTutorial(false);

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


    public void updateScore(int newScore)
    {
        ScoreText.text = newScore.ToString();
    }

    public void updateBalls(int newBalls)
    {
        ballText.text = newBalls.ToString();
    }
}
