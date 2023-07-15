using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void EndGame()
    {
        Application.Quit();
    }

    public void GotoOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void GotoMainMenu()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
