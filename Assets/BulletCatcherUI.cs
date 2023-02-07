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

    void AddBullet(Bullet newBullet){
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
        storedBullets.Add(new Bullet(Bullet.bulletTypes.Red));
        storedBullets.Add(new Bullet(Bullet.bulletTypes.Green));
        storedBullets.Add(new Bullet(Bullet.bulletTypes.Blue));
        storedBullets.Add(new Bullet(Bullet.bulletTypes.Yellow));

        UpdateUI();
    }

}

public class Bullet{
    public enum bulletTypes{
        Red,
        Green,
        Blue,
        Yellow
    }
    public Bullet(bulletTypes b){
        type = b;
    }
    bulletTypes type;

    public bulletTypes GetBulletType() => type;
    public Color GetColor(){
        switch(type){
            case bulletTypes.Red:
                return Color.red;
            case bulletTypes.Green:
                return Color.green;
            case bulletTypes.Blue:
                return Color.blue;
            case bulletTypes.Yellow:
                return Color.yellow;
            default: return Color.black;
        }
    }

}
