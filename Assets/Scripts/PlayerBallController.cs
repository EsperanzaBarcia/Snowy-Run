using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallController : MonoBehaviour
{
    /// <summary>
    /// Max possible size of the ball, setted by inspector
    /// </summary>
    public float maxSize;
    /// <summary>
    /// Min possible size of the ball, setted by inspector
    /// </summary>
    public float minSize;

    public float bigSize;

    public SphereCollider playerCollider;

    public float minimunColliderSize;
    public Vector3 ColliderDefaultCenter;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = transform.parent.parent.GetComponent<SphereCollider>();

        if(playerCollider)
        {
            minimunColliderSize = playerCollider.radius;
            ColliderDefaultCenter = playerCollider.center;
        }
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetParentSize()
    {
        return transform.parent.localScale.x;
    }

    public void StartIncreasingBall(float scaleToIncrease)
    {
        StartCoroutine(IncreaseBall(scaleToIncrease));
    }

    public void StartDecreasingBall(float scaleToDecrease)
    {
        StartCoroutine(DecreaseBall(scaleToDecrease));
    }


    IEnumerator IncreaseBall(float scaleToIncrease)
    {
        Debug.Log("Increasing");
        float elapsedTime = 0;
        float seconds = .5f;

        Vector3 currentScale = transform.parent.localScale;
        Vector3 finalScale;

        if (currentScale.x + scaleToIncrease <= maxSize)
        {
            finalScale = currentScale + new Vector3(scaleToIncrease, scaleToIncrease, scaleToIncrease);
        }
        else
        {
            //max size
            finalScale = new Vector3(maxSize, maxSize, maxSize);
        }

        while (elapsedTime < 1)
        {
            transform.parent.localScale = Vector3.Lerp(currentScale, finalScale, elapsedTime / seconds);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (playerCollider)
        {
            if (playerCollider.radius < GetParentSize() + scaleToIncrease)
            {
                playerCollider.radius = (GetParentSize() + .5f) / 2;
                playerCollider.center = new Vector3(0, GetParentSize() / 2, 0);
                //GetComponent<CapsuleCollider>().center += new Vector3(0, .05f, 0);
            }
        }

    }


    IEnumerator DecreaseBall(float scaleToDecrease)
    {
        float elapsedTime = 0;
        float seconds = .5f;

        Vector3 currentScale = transform.parent.localScale;
        Vector3 finalScale;

        if (currentScale.x - scaleToDecrease >= minSize)
        {
            finalScale = currentScale - new Vector3(scaleToDecrease, scaleToDecrease, scaleToDecrease);
        }
        else
        {
            //minimun size
            //TODO:HARDCODE
            finalScale = new Vector3(minSize, minSize, minSize);
        }

        while (elapsedTime < 1)
        {
            transform.parent.localScale = Vector3.Lerp(currentScale, finalScale, elapsedTime / seconds);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }


        if (playerCollider)
        {
            if (playerCollider.radius > GetParentSize() - scaleToDecrease && GetParentSize() - scaleToDecrease > minimunColliderSize)
            {
                playerCollider.radius = (GetParentSize() - scaleToDecrease) / 2;
                playerCollider.center = new Vector3(0, GetParentSize() / 2, 0);
            }
            else
            {
                playerCollider.radius = minimunColliderSize;
                playerCollider.center = new Vector3(0, GetParentSize() / 2, 0);
            }
        }

    }

}
