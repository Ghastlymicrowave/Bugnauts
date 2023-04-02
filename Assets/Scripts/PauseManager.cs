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
    [SerializeField] GameObject cursorGroup;
    public static bool IsPaused => isPaused; //use this to get paused state, everything else is to set the paused state
    private void Start()
    {
        pausedUI.SetActive(isPaused);
        thisPM = this;
    }
    public static void SetReticleEnabled(bool t)
    {
        thisPM.setReticleEnabled(t);
    }

    public void setReticleEnabled(bool t)
    {
        cursorGroup.SetActive(t);
    }
    public void Pause()
    {
        Debug.Log("Paused");
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        pausedUI.SetActive(isPaused && showUI);
    }
    public void Unpause()
    {
        Debug.Log("Unpaused");
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
