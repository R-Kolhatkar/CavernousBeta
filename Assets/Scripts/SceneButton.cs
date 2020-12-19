using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("3DMine");
    }

    public void RenderControls()
    {

    }
}
