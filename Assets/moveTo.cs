using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTo : MonoBehaviour
{
    [SerializeField]Transform to;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = to.position;
        transform.rotation = to.rotation;
    }
}
