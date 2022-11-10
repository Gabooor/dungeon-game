using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject inventory;
    public GameObject shop;
    public GameObject settings;

    public void Play(){
        SceneManager.LoadScene("Game");
    }

    public void LoadInventory(){
        menu.SetActive(false);
        inventory.SetActive(true);
    }

    public void LoadShop(){
        menu.SetActive(false);
        shop.SetActive(true);
    }

    public void LoadSettings(){
        menu.SetActive(false);
        settings.SetActive(true);
    }

    public void Exit(){
        Application.Quit();
    }

    public void BackToMenu(){
        menu.SetActive(true);
        inventory.SetActive(false);
        shop.SetActive(false);
        settings.SetActive(false);
    }
}
