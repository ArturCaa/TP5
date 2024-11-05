    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public bool CanOpen = false;
    private void LateUpdate()
    {
        if (CanOpen)
            GetComponent<Animator>().enabled = true;
        }
}
