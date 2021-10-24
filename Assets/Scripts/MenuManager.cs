using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public GameObject togglePause;
    public GameObject menu;

    void Start()
    {
        
    }
    public void GoScene(string name)
    {
        SceneManager.LoadScene("Scenes/" + name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf == false)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
    }

    public void Restart()
    {
        UnPause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        togglePause.SetActive(false);
        menu.SetActive(true);
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        togglePause.SetActive(true);
        menu.SetActive(false);
        Time.timeScale = 1;
    }


}
