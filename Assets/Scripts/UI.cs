using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject mainTitle;
    public GameObject buttons;
    public GameObject controlsPanel;
    public GameObject pausePanel;


    public void OnPlay()
    {
        SceneManager.LoadScene("Juego");
    }

    public void OnControls()
    {
        buttons.SetActive(false);
        mainTitle.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void OnReturn()
    {
        buttons.SetActive(true);
        mainTitle.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
