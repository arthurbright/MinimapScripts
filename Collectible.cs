using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{

    public int collectibles = 0;
   

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("collect"))
        {
            Destroy(other.gameObject);
            collectibles += 1;
        }
    }
}
