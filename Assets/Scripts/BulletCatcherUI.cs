using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BulletCatcherUI : MonoBehaviour
{
    [SerializeField] Image primary;
    [SerializeField] Image[] Secondaries;
    [SerializeField] Image reticle;

    public void UpdateUI(Bullet[] bullets){
        for(int i = 0; i < 4; i++){
            if(i == 0){
                if(i < bullets.Length)
                {
                    primary.color = bullets[i].GetColor();
                }
                else
                {
                    primary.color = Color.white;
                }
                
            }else{
                if (i < bullets.Length)
                {
                    Secondaries[i - 1].color = bullets[i].GetColor();
                }
                else
                {
                    Secondaries[i - 1].color = Color.white;
                }
            }
        }
        Color c = primary.color;
        c.a = 1f;
        reticle.color = c;
    }
}

