using UnityEngine;
using System.Collections;
using System;

namespace Shooter3D
{
    public class Gun : Weapons
    {
        public override void Fire(Ammunition ammunition)
        {
            if (_fire)
            {
                if (ammunition)
                {
                    Bullet tempbullet = Instantiate(ammunition, _gun.position, _gun.rotation) as Bullet;

                    if (tempbullet)
                    {
                        tempbullet.GetRigidBody.AddForce(_gun.forward * _force);
                        //tempbullet.GetRigidBody.AddForce(Vector3.Scale(_gun.forward, Vector3.forward) * _force);
                        tempbullet.Name = "Bullet";
                        _fire = false;
                        _recharge.Start(_rechargeTime);
                    }
                }
            }
        }

    }
}
