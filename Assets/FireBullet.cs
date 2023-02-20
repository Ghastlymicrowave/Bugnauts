using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField]PlayerControls pc;
    public void Shoot(){
        pc.Fire();
    }
}
