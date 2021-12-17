using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingAreaController : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public Material[] materials;

    /// <summary>
    /// 
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

            transform.GetChild(i).GetComponent<MeshRenderer>().material = randomMaterial;

            previousMaterial = randomMaterial;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            GameManager.Instance.startRankingArea();
        }
    }

}
