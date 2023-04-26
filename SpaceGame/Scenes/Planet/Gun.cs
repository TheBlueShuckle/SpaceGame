using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    internal class Gun
    {
        MouseState mouseState;
        static int damage, shootCooldown;

        public Gun(int damage, int shootCooldown)
        {
            Damage = damage;
            ShootCooldown = shootCooldown;
        }

        public static int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public static int ShootCooldown
        {
            get { return shootCooldown; }
            set { shootCooldown = value; }
        }
    }
}