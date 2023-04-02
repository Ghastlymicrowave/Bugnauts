using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FieldGuide : MonoBehaviour
{
    [SerializeField] TextMeshPro title;
    [SerializeField] TextMeshPro body;
    [SerializeField] TextMeshPro phantomBody;
    [SerializeField] SpriteRenderer spr;

    [SerializeField] List<FieldGuideScriptableObject> pages;

    int pageIndex = 0;
    public void PageRight()
    {
        pageIndex = Mathf.Min(pages.Count - 1, pageIndex + 1);
        DisplayPage();
    }
    public void PageLeft()
    {
        pageIndex = Mathf.Max(0, pageIndex - 1);
        DisplayPage();
    }
    public void DisplayPage()
    {
        FieldGuideScriptableObject p = pages[pageIndex];
        title.text = p.title;
        body.text = p.body;
        phantomBody.text = p.phantomBody;
        title.ForceMeshUpdate();
        body.ForceMeshUpdate();
        phantomBody.ForceMeshUpdate();
    }
}
