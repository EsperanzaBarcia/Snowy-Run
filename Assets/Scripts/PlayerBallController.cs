/**
 * 
 * Created by Esperanza Barcia DEC 2021
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to handle all visual and colliders settings of the ball
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

    /// <summary>
    /// At this size, big ball mode starts
    /// </summary>
    public float bigSize;

    /// <summary>
    /// Reference to player collider
    /// </summary>
    public SphereCollider playerCollider;

    /// <summary>
    /// Minimum size of player collider
    /// </summary>
    public float minimunColliderSize;

    /// <summary>
    /// Gameobject mesh renderer to change material
    /// </summary>
    MeshRenderer meshRenderer;

    /// <summary>
    /// default material
    /// </summary>
    Material defaultBallMaterial;

    /// <summary>
    /// Material when ball is on big mode
    /// </summary>
    public Material bigBallMaterial;

    // Start is called before the first frame update
    void Start()
    {
        //sets references 

        playerCollider = transform.parent.parent.GetComponent<SphereCollider>();

        if (playerCollider)
        {
            minimunColliderSize = playerCollider.radius;
        }

        meshRenderer = GetComponent<MeshRenderer>();
        defaultBallMaterial = meshRenderer.material;

    }

    /// <summary>
    /// Method to get parents gameobject size
    /// Only one parameter because it grows as a sphere
    /// </summary>
    /// <returns></returns>
    public float GetParentSize()
    {
        return transform.parent.localScale.x;
    }

    /// <summary>
    /// Method to start increasing coroutine
    /// </summary>
    /// <param name="scaleToIncrease">scale to add</param>
    public void StartIncreasingBall(float scaleToIncrease)
    {
        StartCoroutine(IncreaseBall(scaleToIncrease));
    }

    /// <summary>
    ///  Method to start decreasing coroutine
    /// </summary>
    /// <param name="scaleToDecrease">scale to quit</param>
    public void StartDecreasingBall(float scaleToDecrease)
    {
        StartCoroutine(DecreaseBall(scaleToDecrease));
    }

    /// <summary>
    /// Coroutine to show ball increasing visually
    /// </summary>
    /// <param name="scaleToIncrease"></param>
    /// <returns></returns>
    IEnumerator IncreaseBall(float scaleToIncrease)
    {
        float elapsedTime = 0;

        //Duration of transition
        float seconds = .5f;

        //initial scale
        Vector3 currentScale = transform.parent.localScale;

        //target scale
        Vector3 finalScale;

        //if the increasing is not bigger than the max size
        if (currentScale.x + scaleToIncrease <= maxSize)
        {
            finalScale = currentScale + new Vector3(scaleToIncrease, scaleToIncrease, scaleToIncrease);
        }        
        else
        {
            finalScale = new Vector3(maxSize, maxSize, maxSize);
        }

        //interpolates the sizes gradually
        while (elapsedTime < seconds)
        {
            transform.parent.localScale = Vector3.Lerp(currentScale, finalScale, elapsedTime / seconds);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //if the ball is on big ball mode, changes material
        if (transform.parent.localScale.x >= bigSize)
        {       
            meshRenderer.material = bigBallMaterial;
        }

        //Modifies player collider to fit with visual feedback
        if (playerCollider)
        {
            //TODO:HARDCODE
            if (playerCollider.radius < GetParentSize() + scaleToIncrease)
            {
                playerCollider.radius = (GetParentSize() + .5f) / 2;
                playerCollider.center = new Vector3(0, GetParentSize() / 2, 0);
                //GetComponent<CapsuleCollider>().center += new Vector3(0, .05f, 0);
            }
        }

    }


    /// <summary>
    /// Coroutine to show ball decreasing visually
    /// </summary>
    /// <param name="scaleToDecrease"></param>
    /// <returns></returns>
    IEnumerator DecreaseBall(float scaleToDecrease)
    {  
       
        float elapsedTime = 0;
        //Duration of transition
        float seconds = .5f;

        //initial scale
        Vector3 currentScale = transform.parent.localScale;

        //target scale
        Vector3 finalScale;

        //if the decreasing is bigger than the min size
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

        //interpolates the sizes gradually
        while (elapsedTime < seconds)
        {
            transform.parent.localScale = Vector3.Lerp(currentScale, finalScale, elapsedTime / seconds);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //if the ball is on big ball mode, changes material
        if (transform.parent.localScale.x < bigSize)
        {
            meshRenderer.material = defaultBallMaterial;
        }

        //Modifies player collider to fit with visual feedback
        if (playerCollider)
        {
            //TODO:HARDCODE
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
