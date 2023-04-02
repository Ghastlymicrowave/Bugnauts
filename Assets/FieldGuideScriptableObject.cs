using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FieldGuidePage", menuName = "ScriptableObjects/FieldGuidePage", order = 1)]
[System.Serializable]
public class FieldGuideScriptableObject : ScriptableObject
{
    public string title;
    public string body;
    public string phantomBody;
    public Sprite spr;
}
