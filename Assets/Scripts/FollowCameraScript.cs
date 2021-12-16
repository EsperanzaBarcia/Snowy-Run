using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;

    public Vector3 offset;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (target)
            Camera.main.transform.position = target.transform.position - offset;
    }
}
