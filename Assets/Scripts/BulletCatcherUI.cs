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
    }

    public void AddBullet(Bullet newBullet){
        if(storedBullets.Count==maxBullets){
            storedBullets.RemoveAt(maxBullets-1);
        }
        storedBullets.Add(newBullet);
        UpdateUI();
    }

    public Bullet ShootPrimary(){
        //make some projectile using bullet at index 0
        if (storedBullets.Count<1 || storedBullets[0] == null)
        {
            return null;
        }
        Bullet b = storedBullets[0];
        storedBullets.RemoveAt(0);
        UpdateUI();
        return b;
    }

    void Start(){
        storedBullets = new List<Bullet>();
        

        UpdateUI();
    }

}

