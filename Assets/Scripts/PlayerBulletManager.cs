using Lightbug.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletManager : MonoBehaviour
{
    List<Bullet> storedBullets;
    public List<Bullet> getBullets => storedBullets;
    int maxBullets = 4;

    [SerializeField] BulletCatcherUI bulletUI;
    [SerializeField] PlayerControls playerControls;

    [SerializeField] GameObject ScreenBlankTrigger;
    private float damageMultiplier = 1.0f;
    private float speedMultiplier = 1.0f;

    private bool multiShot = false;

    public bool notHit = true;

    public Buff currentBuff;

    public enum Buff
    {
        None,
        Red,
        Green,
        Blue,
        Yellow,
        Quad
    }

    public bool GetMultiShot() => multiShot;
    public float GetDamageMult() => damageMultiplier;
    public float GetSpeedMult() => speedMultiplier;

    public void AddBullet(Bullet newBullet)
    {
        if (storedBullets.Count == maxBullets)
        {
            storedBullets.RemoveAt(maxBullets - 1);
        }
        storedBullets.Add(newBullet);
        bulletUI.UpdateUI(storedBullets.ToArray());
    }

    public Bullet ShootPrimary()
    {
        //make some projectile using bullet at index 0
        if (storedBullets.Count < 1 || storedBullets[0] == null)
        {
            return null;
        }
        Bullet b = storedBullets[0];
        storedBullets.RemoveAt(0);
        bulletUI.UpdateUI(storedBullets.ToArray());
        return b;
    }

    public void SetBullets(List<Bullet> nbullets)
    {
        storedBullets = nbullets;
        bulletUI.UpdateUI(storedBullets.ToArray());
    }

    public Bullet PhantomizeBullets()
    {
        if (storedBullets.Count < maxBullets)
            return new Bullet(Bullet.bulletTypes.White);

        //RGBY color mapped to prime #s. Code for buff is determined by multiplication of them
        int pCode = 1;
        for (int i = 0; i < storedBullets.Count; i++)
        {
            switch (storedBullets[i].GetBulletType())
            {
                case Bullet.bulletTypes.Red:
                    pCode *= 2;
                    break;
                case Bullet.bulletTypes.Green:
                    pCode *= 3;
                    break;
                case Bullet.bulletTypes.Blue:
                    pCode *= 5;
                    break;
                case Bullet.bulletTypes.Yellow:
                    pCode *= 7;
                    break;
                default:
                    break;
            }
        }

        //Based on the code, activate the correct coroutine.
        IEnumerator buff = null;
        switch (pCode)
        {
            case 16:
                buff = RedBuff_AttackAndNet();
                break;
            case 81:
                buff = GreenBuff_MultiShot();
                break;
            case 625:
                buff = BlueBuff_ScreenNuke();
                break;
            case 2401:
                buff = YellowBuff_BulletAbsorption();
                break;
            case 210:
                buff = TriBuff_SuperBullet();
                break;
            case 36: case 100: case 196: case 225: case 441:
            case 1225:
                buff = DualBuff_Heal();
                break;
            default:
                break;
        }

        if (buff != null)
        {
            Bullet retBullet = storedBullets[1];
            storedBullets.Clear();
            bulletUI.UpdateUI(storedBullets.ToArray());
            StartCoroutine(buff);
            return retBullet;
        }
        return new Bullet(Bullet.bulletTypes.White);
    }

    IEnumerator RedBuff_AttackAndNet()
    {
        notHit = true;
        damageMultiplier = 3.0f;
        speedMultiplier = 1.5f;
        currentBuff = Buff.Red;

        while (notHit)
        {
            yield return null;
        }

        damageMultiplier = 1.0f;
        speedMultiplier = 1.0f;
        currentBuff = Buff.None;
        yield return null;
    }

    IEnumerator GreenBuff_MultiShot()
    {
        notHit = true;
        multiShot = true;
        currentBuff = Buff.Green;
        while (notHit)
        {
            yield return null;
        }

        multiShot = false;
        currentBuff = Buff.None;
        yield return null;
    }

    IEnumerator BlueBuff_ScreenNuke()
    {
        currentBuff = Buff.Blue;

        ScreenBlankTrigger.SetActive(true);
        BulletSpawner.canFire = false;
        playerControls.SetInvincibility(true);

        yield return new WaitForSeconds(0.1f);
        ScreenBlankTrigger.SetActive(false);
        playerControls.SetInvincibility(false);

        yield return new WaitForSeconds(3);
        BulletSpawner.canFire = true;
        currentBuff = Buff.None;

        yield return null;
    }

    IEnumerator YellowBuff_BulletAbsorption()
    {
        playerControls.FireAbsorptionBullet();
        yield return null;
    }

    IEnumerator TriBuff_SuperBullet()
    {
        currentBuff = Buff.Quad;
        playerControls.FireTriBullet();

        playerControls.SetInvincibility(true);
        yield return new WaitForSeconds(1.0f);
        playerControls.SetInvincibility(false);
        currentBuff = Buff.None;
        yield return null;
    }

    IEnumerator DualBuff_Heal()
    {
        //Heal the player
        //Figure out how that is set up later.
        yield return null;
    }

    private void Awake()
    {
        ScreenBlankTrigger.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        storedBullets = new List<Bullet>();
        /*
        AddBullet(new Bullet(Bullet.bulletTypes.Green));
        AddBullet(new Bullet(Bullet.bulletTypes.Green));
        AddBullet(new Bullet(Bullet.bulletTypes.Green));
        AddBullet(new Bullet(Bullet.bulletTypes.Green));*/

        bulletUI.UpdateUI(storedBullets.ToArray());
    }
}
