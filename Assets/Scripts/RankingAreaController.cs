/**
 * 
 * Created by Esperanza Barcia DEC 2021
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Class to change colors of ranking area
public class RankingAreaController : MonoBehaviour
{
    /// <summary>
    /// Materials to assign
    /// </summary>
    public Material[] materials;

    /// <summary>
    /// Material previously assigned
    /// </summary>
    Material previousMaterial;


    // Start is called before the first frame update
    void Start()
    {
        //Selects a random color for the multiplier
        for (int i = 0; i < transform.childCount; i++)
        {
            Material randomMaterial;

            //Checks that is not repeating the previous color
            do
            {
                randomMaterial = materials[Random.Range(0, materials.Length)];

            } while (previousMaterial == randomMaterial);

            //Sets it
            MeshRenderer currentMeshRenderer = transform.GetChild(i).GetComponent<MeshRenderer>();

            if (currentMeshRenderer)
            {
                transform.GetChild(i).GetComponent<MeshRenderer>().material = randomMaterial;

                previousMaterial = randomMaterial;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the player is colliding, ssets the game phase
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.StartRankingArea();
        }
    }

}
