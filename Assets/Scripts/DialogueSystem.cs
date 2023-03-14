using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class DialogueSystem : MonoBehaviour
{
    [SerializeField]Text txt;
    public Dialogue_scriptableObj obj;
    [SerializeField] PlayerControls player;
    [SerializeField] GameObject box;
    Action_openDialouge opener;

    int line;

    public void StartDialogue(Dialogue_scriptableObj scrptObj, Action_openDialouge nopener)
    {
        obj = scrptObj;
        line = 0; 
        UpdateText();
        opener = nopener;
    }
    
    public void NextLine(InputAction.CallbackContext ctx)
    {
        if(obj==null || !ctx.started){return;}
        line++;
        if(line >= obj.text.Count){
            End();
        }
        UpdateText();
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
        opener.Trigger();
        opener = null;
    }
    public void ClearPlayer(){
        player.DialougeOpen = false;
    }
}
