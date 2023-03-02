using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Action_enableDisable : ActionBase
{
    [SerializeField] List<GameObject> selected;
    [SerializeField] bool currentlyEnabled;
    [SerializeField] List<NavMeshAgent> agents;
    [SerializeField] List<MonoBehaviour> scripts;

    void Start(){
        for(int i = 0; i < selected.Count; i++){
            selected[i].SetActive(currentlyEnabled);
        }
        for(int i = 0; i < agents.Count; i++){
            agents[i].enabled = currentlyEnabled;
        }
        for(int i = 0; i < scripts.Count; i++){
            scripts[i].enabled = currentlyEnabled;
        }
    }

    public override void Activate()
    {
        base.Activate();
        currentlyEnabled = !currentlyEnabled;
        for(int i = 0; i < selected.Count; i++){
            selected[i].SetActive(currentlyEnabled);
        }
        for(int i = 0; i < agents.Count; i++){
            agents[i].enabled = currentlyEnabled;
        }
        for(int i = 0; i < scripts.Count; i++){
            scripts[i].enabled = currentlyEnabled;
        }
    }
}
