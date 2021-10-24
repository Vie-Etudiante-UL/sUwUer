using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{

    void Start()
    {
        Time.timeScale = 1;
    }
    public void GoScene(string name)
    {
        SceneManager.LoadScene("Scenes/" + name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
