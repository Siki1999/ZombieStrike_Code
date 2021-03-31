using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject DifficultyMenu;
    public GameObject WaveMenu;

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void Start3Waves()
    {
        WaveSystem.waves = new WaveSystem.Wave[3];
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Start5Waves()
    {
        WaveSystem.waves = new WaveSystem.Wave[5];
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Start7Waves()
    {
        WaveSystem.waves = new WaveSystem.Wave[7];
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartWaves()
    {
        WaveSystem.waves = new WaveSystem.Wave[100];
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void EasyGoToWaves()
    {
        Player.multy = 2;
        DifficultyMenu.SetActive(false);
        WaveMenu.SetActive(true);
    }

    public void MediumGoToWaves()
    {
        Player.multy = 1.5f;
        DifficultyMenu.SetActive(false);
        WaveMenu.SetActive(true);
    }

    public void HardGoToWaves()
    {
        Player.multy = 1;
        DifficultyMenu.SetActive(false);
        WaveMenu.SetActive(true);
    }
}
