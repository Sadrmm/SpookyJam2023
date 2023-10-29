using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public GameObject shopMenu;
    void Start()
    {
        MainMenuButton();
    }

    public void PlayNowButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ShopNowButton()
    {
        mainMenu.SetActive(false);
        shopMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    public void CreditsButton()
    {
        mainMenu.SetActive(false);
        shopMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void MainMenuButton()
    {
        mainMenu.SetActive(true);
        shopMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
