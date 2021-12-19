/**
 * 
 * Created by Esperanza Barcia DEC 2021
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to control animations of visual character and ball, also the position
public class CharacterController : MonoBehaviour
{
    /// <summary>
    /// Reference to character animator
    /// </summary>
    Animator characterAnimator;

    /// <summary>
    /// Reference to ball animator
    /// </summary>
    public Animator ballAnimator;

    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //Sets the character position always on top of the ball
        transform.position = new Vector3(transform.position.x,
            ballAnimator.gameObject.transform.position.y + ballAnimator.gameObject.transform.parent.transform.localScale.y / 2,
            transform.position.z);
    }

    /// <summary>
    /// Method to change to movement animations
    /// </summary>
    public void Run()
    {
        if (characterAnimator)
        {
            bool isRunning = true;
            characterAnimator.SetBool("IsRunning", isRunning);

            if (ballAnimator)
                ballAnimator.SetBool("Rotate", isRunning);
        }
    }

    /// <summary>
    /// Method to change to defeat animations
    /// </summary>
    public void Defeat()
    {
        if (characterAnimator)
        {
            characterAnimator.SetBool("Defeated", true);
        }

        if (ballAnimator)
            ballAnimator.SetBool("Rotate", false);
    }

    /// <summary>
    /// Method to change to success animations
    /// </summary>
    public void Success()
    {
        if (characterAnimator)
        {
            characterAnimator.SetBool("Success", true);
        }

        if (ballAnimator)
            ballAnimator.SetBool("Rotate", false);
    }

}
