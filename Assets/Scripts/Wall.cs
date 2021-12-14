using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the player hits 
        if (other.CompareTag(GameManager.Instance.ballTagName))
        {
            //Animation
            //Disables the wall
            gameObject.SetActive(false);
        }
    }
}
