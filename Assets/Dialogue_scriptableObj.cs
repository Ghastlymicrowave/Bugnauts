using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
[System.Serializable]
public class Dialogue_scriptableObj : ScriptableObject
{
    public List<string> text;
    //TODO maybe add space for flags or references to actions? idfk
}
