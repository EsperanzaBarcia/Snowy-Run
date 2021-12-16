using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    Animator characterAnimator;
    public Animator ballAnimator;

    bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Run()
    {
        if (characterAnimator)
        {
            isRunning = true;
            characterAnimator.SetBool("IsRunning", isRunning);

            if (ballAnimator)
                ballAnimator.SetBool("Rotate", isRunning);
        }
    }

    public void Defeat()
    {
        if (characterAnimator)
        {

        }
    }

    public void Success()
    {
        if (characterAnimator)
        {

        }
    }

}
