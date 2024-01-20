using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRangedWeapon : BaseWeapon
{
    // CONTEXT: 

    // Shoot variales
    public GameObject BulletType;
    public BulletSpawnerScript BulletSpawner;
    public int NumBullets;
    public float ShootingForce;

    protected override void FollowPointer()
    {
        rb.MoveRotation(Mathf.Atan2(_shootingVector.y, _shootingVector.x) * Mathf.Rad2Deg);

        if (_holder.MousePos.x < _holder.transform.position.x)
        {
            // Weapon is floating left of the player
            this.transform.position = new Vector3(_holder.transform.position.x - HandOffset.x,
                                                  _holder.transform.position.y - HandOffset.y,
                                                  0);
            this.transform.localScale = new Vector3(this.transform.localScale.x,
                                                    _originalYScale * -1,
                                                    this.transform.localScale.z);
        }
        else
        {
            // Weapon is floating right of the player
            this.transform.position = new Vector3(_holder.transform.position.x + HandOffset.x,
                                                  _holder.transform.position.y + HandOffset.y,
                                                  0);
            this.transform.localScale = new Vector3(this.transform.localScale.x,
                                                    _originalYScale,
                                                    this.transform.localScale.z);
        }
    }

    // Spawn a bullet at the tip of the weapon
    public override void Shoot(float dmgMod)
    {
        if (TryShoot())
        {
            BulletSpawner.spawnBullet(BulletType, _shootingVector.normalized, ShootingForce, dmgMod, this.transform.rotation);
        }
    }

    protected bool TryShoot()
    {
        if (NumBullets > 0)
        {
            NumBullets--;
            ui.DisplayNewWNBullets(NumBullets);
            return true;
        }

        return _holder.BorrowBullet();
    }

    protected override void DisplayUp()
    {
        ui.DisplayNewWNBullets(NumBullets);
        ui.EnableWeaponNBullets(true);
    }

    protected override void DisplayDown()
    {
        ui.EnableWeaponNBullets(false);
    }
}
