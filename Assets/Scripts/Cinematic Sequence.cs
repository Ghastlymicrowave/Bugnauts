using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CinematicSequence
{
    [SerializeField] string activeCameraName = "";
    [SerializeField] Dialogue_scriptableObj dialouge = null;
    [SerializeField] string popupText = "";
    [SerializeField] string customTriggerID = "";
    [SerializeField] int setBugsLeft = -1;
    [SerializeField] int indexOfAction_basetoTrigger = 1;
    [SerializeField] bool pauseGame =true;
    [SerializeField] bool startSequenceOnDialougeEnd = false;
    [SerializeField] bool playerCanUnpause = true;
    [SerializeField] bool overrideShowPopup = false;

    public string triggerID => customTriggerID;
    public string ActiveCameraName => activeCameraName;
    public Dialogue_scriptableObj Dialouge => dialouge;
    public string PopupText => popupText;
    public int SetBugsLeft => setBugsLeft;
    public bool PauseGame => pauseGame;
    public bool StartSequenceOnDialougeEnd => startSequenceOnDialougeEnd;
    public int IndexOfAction_basetoTrigger => indexOfAction_basetoTrigger;
    public bool PlayerCanUnpause => playerCanUnpause;
    public bool OverrideShowPopup => overrideShowPopup;
}
