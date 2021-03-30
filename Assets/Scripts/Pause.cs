using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject PauseUI;
    public FPSController playerMovement;
    public Player player;
    private bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            PauseUI.SetActive(isPaused);
            AudioListener.pause = isPaused;
            Cursor.visible = isPaused;
            playerMovement.enabled = !isPaused;
            player.dead = isPaused;
            if (isPaused)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        PauseUI.SetActive(false);
        AudioListener.pause = false;
        Cursor.visible = false;
        playerMovement.enabled = true;
        player.dead = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
