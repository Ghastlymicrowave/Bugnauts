using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBlank : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        EnemyProjectile eP = other.gameObject.GetComponent<EnemyProjectile>();

        if (eP != null)
        {
            Debug.Log("ScreenBlank");
            Destroy(eP.gameObject);
        }
    }
}
