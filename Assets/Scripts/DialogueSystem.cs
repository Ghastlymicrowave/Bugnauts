using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Animatext;
public class DialogueSystem : MonoBehaviour
{
    [SerializeField]Text txt;
    public Dialogue_scriptableObj obj;
    [SerializeField] PlayerControls player;
    [SerializeField] GameObject box;
    Action_openDialouge opener;
    [SerializeField] AnimatextUGUI anima;
    [SerializeField] GameTracker tracker;
    [SerializeField] GameObject EbuttonPrompt;

    int line;

    public void StartDialogue(Dialogue_scriptableObj scrptObj, Action_openDialouge nopener = null)
    {
        obj = scrptObj;
        line = 0; 
        UpdateText();
        opener = nopener;
        player.DialougeOpen = true;
    }
    
    public void NextLine(InputAction.CallbackContext ctx)
    {
        if(obj==null || !ctx.started){return;}
        if (anima.effects[0].state == Animatext.EffectState.End || anima.effects[0].time>0.04f*obj.text[line].Length)
        {
            line++;
            if (line >= obj.text.Count)
            {
                End();
            }
            UpdateText();
        }
        else
        {
            anima.EndEffect(0);
        }

        
    }
    public void UpdateText(){
        if(obj !=null){
            box.SetActive(true);
            txt.text = obj.text[line];
        }else{
            txt.text = "";
            box.SetActive(false);
        }
    }

    public void End(){
        line = 0;
        obj = null;
        Invoke("ClearPlayer",0.01f);
        
    }
    public void ClearPlayer(){
        player.DialougeOpen = false;
        PauseManager.SetPaused(false);
        if (tracker != null)
        {
            tracker.EndDialouge();
        }
        if (opener != null)
        {
            opener.Trigger();
            opener = null;
        }
    }

    private void Update()
    {
        if (obj != null)
        {
            if(anima.effects[0].state == Animatext.EffectState.End || anima.effects[0].time > 0.04f * obj.text[line].Length)
            {
                if (!EbuttonPrompt.activeSelf) 
                { 
                    EbuttonPrompt.SetActive(true); 
                }
            }
            else if (EbuttonPrompt.activeSelf)
            {
                EbuttonPrompt.SetActive(false);
            }
        }
    }
}
