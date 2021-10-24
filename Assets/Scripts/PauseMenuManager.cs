using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenuManager : MonoBehaviour
{
    public PlayerController playerController;

    public bool pause = false;

    public GameObject toggleMenu;
    public GameObject menu;

    public void Pause()
    {
        //audioMixer.SetFloat("Effets", -80f);
        pause = true;
        playerController.runSound.Stop();
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        pause = false;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        UnPause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToScene(string name)
    {
        SceneManager.LoadScene("Scenes/" + name);

    }

    public void ExitGame()
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
                toggleMenu.SetActive(false);
                menu.SetActive(true);
            }
            else
            {
                UnPause();
                toggleMenu.SetActive(true);
                menu.SetActive(false);
            }
        }
    }
}
