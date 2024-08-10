using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public void RepalyBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitBtn()
    {
        Application.Quit();
    }
}
