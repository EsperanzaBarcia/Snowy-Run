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
        transform.position = new Vector3(transform.position.x, ballAnimator.gameObject.transform.position.y + ballAnimator.gameObject.transform.parent.transform.localScale.y/2, transform.position.z);
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
            characterAnimator.SetBool("Defeated", true);
        }
    }

    public void Success()
    {
        if (characterAnimator)
        {
            characterAnimator.SetBool("Success", true);
        }
    }

}
