using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour
{
    public GameObject instructionsPanel;
    public GameObject premiseCanvas;

    public void PlayGame()
    {
        SceneManager.LoadScene("3DMine");
    }

    public void Premise()
    {
        premiseCanvas.SetActive(true);
    }

    public void ClosePremise()
    {
        premiseCanvas.SetActive(false);
    }

    public void HowToPlay()
    {
        instructionsPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CloseHowToPlay()
    {
        instructionsPanel.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
