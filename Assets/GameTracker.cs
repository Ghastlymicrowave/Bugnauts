using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameTracker : MonoBehaviour
{
    [SerializeField] int bugsLeft = 0;
    [SerializeField] Text trackerText;
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
        bugsLeft = amt;
        UpdateText();
    }
}
