using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    static bool isPaused = false;
    [SerializeField] GameObject pausedUI;
    public static bool IsPaused => isPaused; //use this to get paused state, everything else is to set the paused state
    private void Start()
    {
        pausedUI.SetActive(isPaused);
    }
    public void Pause()
    {
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        pausedUI.SetActive(isPaused);
    }
    public void Unpause()
    {
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        pausedUI.SetActive(isPaused);
    }
    public void TogglePaused()
    {
        if (isPaused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }
    public void SetPaused(bool paused)
    {
        if (!paused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }
}
