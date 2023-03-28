using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AphidSetAnim : MonoBehaviour
{
    [SerializeField] Animator anim;
    void Start()
    {
        anim.SetBool("Eating", true);
    }
}
