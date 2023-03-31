using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class GameTracker : MonoBehaviour
{
    //a way to sequence events, 2 parts
    //current thing, trigger to next thing


    //current thing has:
    //>active camera (referenced via name) - string
    //either
    //>textboxes - text asset
    //>popups - one string

    private void Start()
    {
        Invoke("ActivateSequence", 0.1f);
    }

    [SerializeField] List<CinematicSequence> sequences;

    [SerializeField] int bugsLeft = 0;
    [SerializeField] Text trackerText;
    [SerializeField] DialogueSystem ds;

    [SerializeField] GameObject popupText;
    [SerializeField] TMPro.TextMeshProUGUI popupTextText;
    [SerializeField] List<ActionBase> Actions;
    [SerializeField] GameObject bugsLeftDisplay;
    CinemachineVirtualCamera activeCam;
    int activeSequence = 0;
    public void KillBug()
    {
        bugsLeft = Mathf.Max(0, bugsLeft - 1);
        UpdateText();
    }
    public void UpdateText()
    {
        trackerText.text = bugsLeft.ToString();
    }
    public void SetBugs(int amt)
    {
        if(amt == -2)
        {
            amt = 0;
            bugsLeftDisplay.SetActive(false);
        }
        bugsLeft = amt;
        UpdateText();
    }

    public void TriggerIDAtIndex(int i)
    {
        activeSequence = i;
        ActivateSequence();
    }

    public bool SendCustomTrigger(string triggerID, bool canRunOutOfOrder)
    {
        if (activeSequence >= sequences.Count) { return false; }
        if (canRunOutOfOrder)
        {
            for(int i = 0; i < sequences.Count; i++)
            {
                if (sequences[i].triggerID == triggerID)
                {
                    TriggerIDAtIndex(i);
                    return true;
                }
            }
        }
        else
        {
            if (sequences[activeSequence].triggerID == triggerID)
            {
                NextSequence();
                return true;
            }
        }
        
        return false;
    }

    public void NextSequence()
    {
        ActivateSequence();
    }
    public void ActivateSequence()
    {
        Debug.Log("active sequence:" + activeSequence.ToString() + " sequences count:" + sequences.Count.ToString());
        if (activeSequence >= sequences.Count)
        {
            return;
        }
        CinematicSequence s = sequences[activeSequence];
        #region activeCam
        if (s.ActiveCameraName != "")
        {
            GameObject g = GameObject.Find(s.ActiveCameraName);
            if (g != null)
            {
                CinemachineVirtualCamera vc = g.GetComponent<CinemachineVirtualCamera>();
                if (vc != null)
                {
                    ChangeActiveCam(vc);
                }
                else
                {
                    Debug.LogWarning("No virtual camera found for active camera name gameobject");
                }
            }
            else
            {
                Debug.LogWarning("No gameobject found for active camera name");
            }
        }
        else
        {
            ResetCam();
        }
        #endregion
        if (s.Dialouge != null)
        {
            ds.StartDialogue(s.Dialouge);
        }
        if (s.SetBugsLeft != -1)
        {
            SetBugs(s.SetBugsLeft);
        }


        if (s.IndexOfAction_basetoTrigger != -1)
        {
            if (Actions.Count > 0)
            {
                Actions[s.IndexOfAction_basetoTrigger].Activate();
            }
            else
            {
                Debug.LogWarning("No actions to trigger");
            }
        }

        if (s.PopupText != "")
        {
            popupText.SetActive(true);
            popupTextText.text = s.PopupText;
        }
        else
        {
            popupText.SetActive(false);
        }

        if (s.PauseGame)
        {
            PauseManager.showUI = false;
        }
        PauseManager.SetPaused(s.PauseGame);
        if (!s.PauseGame)
        {
            PauseManager.playerCanUnpause = true;
        }
        else
        {
            PauseManager.playerCanUnpause = false;
        }
        activeSequence++;
    }

    public void EndDialouge()
    {
        Debug.Log("Dialogue Ended");
        PauseManager.SetPaused(false);
        if (sequences[activeSequence-1].StartSequenceOnDialougeEnd)
        {
            ActivateSequence();
        }
    }

    public void ResetCam()
    {
        if (activeCam != null)
        {
            activeCam.Priority = 0;
            Debug.Log("reset cam");
        }
    }

    public void ChangeActiveCam(CinemachineVirtualCamera newCam)
    {
        ResetCam();
        newCam.Priority = 100;
        activeCam = newCam;
    }
}


