using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool showUI = true;
    static bool isPaused = false;
    public static bool stopAnims = true;
    [SerializeField] GameObject pausedUI;
    static PauseManager thisPM;
    public static bool playerCanUnpause = false;
    public static bool IsPaused => isPaused; //use this to get paused state, everything else is to set the paused state
    private void Start()
    {
        pausedUI.SetActive(isPaused);
        thisPM = this;
    }

    public void Pause()
    {
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        pausedUI.SetActive(isPaused && showUI);
    }
    public void Unpause()
    {
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        pausedUI.SetActive(isPaused && showUI);
    }
    public static void TogglePaused()
    {
        if (isPaused)
        {
            thisPM.Unpause();
        }
        else
        {
            thisPM.Pause();
        }
    }
    public static void SetPaused(bool paused)
    {
        if (!paused)
        {
            thisPM.Unpause();
        }
        else
        {
            thisPM.Pause();
        }
    }
}
