using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    private static int keyCount = 0;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            keyCount++; 
            if (keyCount >= 3)
            {
                GameObject.Find("ExitDoor").GetComponent<ExitDoor>().CanOpen = true;
            }
            Destroy(gameObject); 
        }
    }
}

