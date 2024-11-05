using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    public float targetTime = 10.0f;

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }


    public void PlayGame()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
    }

}

