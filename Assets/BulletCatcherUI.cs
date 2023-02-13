using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BulletCatcherUI : MonoBehaviour
{
    List<Bullet> storedBullets;
    int maxBullets = 4;
    [SerializeField] Image primary;
    [SerializeField] Image[] Secondaries;


    void UpdateUI(){
        Bullet[] bullets = storedBullets.ToArray();
        for(int i = 0; i < bullets.Length; i++){
            if(i == 0){
                primary.color = bullets[i].GetColor();
            }else{
                Secondaries[i-1].color = bullets[i].GetColor();
            }
        }
    }

    public void AddBullet(Bullet newBullet){
        if(storedBullets.Count==maxBullets){
            storedBullets.RemoveAt(maxBullets-1);
        }
        storedBullets.Add(newBullet);
        UpdateUI();
    }

    void ShootPrimary(){
        //make some projectile using bullet at index 0
        storedBullets.RemoveAt(0);
        UpdateUI();
    }

    void Start(){
        storedBullets = new List<Bullet>();
        

        UpdateUI();
    }

}

