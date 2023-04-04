using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBlank : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {

        Debug.Log(other.name+ "stay");
        if (other.tag =="EnemyBullet")
        {
            Debug.Log("ScreenBlank");
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "enter");
    }
}
