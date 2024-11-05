using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField]  // Attribut correctement écrit
    private int startCountDown = 10;

    [SerializeField]  // Attribut correctement écrit
    private TextMeshProUGUI TxtCountDown;

    [SerializeField]
    private GameObject _gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        TxtCountDown.text = "TimeLeft : " + startCountDown;
        StartCoroutine(Pause());
       
    }

    IEnumerator Pause()
    {
        while (startCountDown > 0)
        {
            yield return new WaitForSeconds(1f);
            startCountDown--;
            TxtCountDown.text = "TimeLeft : " + startCountDown;
        }
        StartCoroutine(ShowGameOverPanel());
    }
    IEnumerator ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
        Debug.Log("Game over");

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(0);
    }


}