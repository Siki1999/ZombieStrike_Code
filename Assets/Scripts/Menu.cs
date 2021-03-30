using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
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
}
