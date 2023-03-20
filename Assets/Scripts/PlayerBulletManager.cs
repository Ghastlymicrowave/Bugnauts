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

    public bool notHit = true;

    public float GetDamageMult() => damageMultiplier;

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

    public Bullet PhantomizeBullets()
    {
        if (storedBullets.Count < maxBullets)
            return new Bullet(Bullet.bulletTypes.White);

        //RGBY color mapped to prime #s. Code for buff is determined by multiplication of them
        int pCode = 1;
        for (int i = 1; i < storedBullets.Count; i++)
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
            case 8:
                buff = RedBuff_AttackAndNet();
                break;
            case 27:
                buff = GreenBuff_MultiShot();
                break;
            case 125:
                buff = BlueBuff_ScreenNuke();
                break;
            case 343:
                buff = YellowBuff_BulletAbsorption();
                break;
            case 30:
                buff = TriBuff_SuperBullet();
                break;
            case 12: case 20: case 28: case 18: case 45: case 63:
            case 50: case 75: case 175: case 98: case 147:
            case 245:
                buff = DualBuff_Heal();
                break;
            default:
                break;
        }

        if (buff != null)
        {
            Bullet retBullet = storedBullets[1];
            storedBullets.RemoveRange(1, 3);
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
        //faster net swing?

        while(notHit)
        {
            yield return null;
        }

        damageMultiplier = 1f;

        yield return null;
    }

    IEnumerator GreenBuff_MultiShot()
    {
        //Multi-Shot
        //need clearer specs
        notHit = true;

        while (notHit)
        {
            yield return null;
        }

        yield return null;
    }

    IEnumerator BlueBuff_ScreenNuke()
    {
        ScreenBlankTrigger.SetActive(true);
        BulletSpawner.canFire = false;
        playerControls.SetInvincibility(true);

        yield return new WaitForSeconds(0.1f);
        ScreenBlankTrigger.SetActive(false);
        playerControls.SetInvincibility(false);

        yield return new WaitForSeconds(3);
        BulletSpawner.canFire = true;
        yield return null;
    }

    IEnumerator YellowBuff_BulletAbsorption()
    {
        playerControls.FireAbsorptionBullet();
        yield return null;
    }

    IEnumerator TriBuff_SuperBullet()
    {
        playerControls.FireTriBullet();

        playerControls.SetInvincibility(true);
        yield return new WaitForSeconds(1.0f);
        playerControls.SetInvincibility(false);

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
        AddBullet(new Bullet(Bullet.bulletTypes.Blue));
        AddBullet(new Bullet(Bullet.bulletTypes.Blue));
        AddBullet(new Bullet(Bullet.bulletTypes.Green));
        AddBullet(new Bullet(Bullet.bulletTypes.Red));

        bulletUI.UpdateUI(storedBullets.ToArray());
    }
}
