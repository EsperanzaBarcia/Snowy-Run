/**
 * 
 * Created by Esperanza Barcia DEC 2021
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to make a follow target camera
public class FollowCameraScript : MonoBehaviour
{
    /// <summary>
    /// Target to follow
    /// </summary>
    public Transform target;

    /// <summary>
    /// offset to apply to position
    /// </summary>
    public Vector3 offset;


    private void LateUpdate()
    {
        //Sets the position
        if (target)
            transform.position = target.transform.position - offset;
    }
}
