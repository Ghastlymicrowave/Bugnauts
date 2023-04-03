using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_ModfiyHoldBar : ActionBase
{
    [SerializeField] PlayerBulletManager pb;
    [SerializeField] List<Bullet> bullets;
    public override void Activate()
    {
        base.Activate();
        pb.SetBullets(bullets);
    }
}
