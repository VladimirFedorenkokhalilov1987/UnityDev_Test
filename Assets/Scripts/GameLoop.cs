using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    // Start is called before the first frame update
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Playground");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Playground");
    }

    public void ExitToMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
