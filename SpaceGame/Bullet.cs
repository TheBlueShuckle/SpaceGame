using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    internal class Bullet
    {
        MouseState mouseState;
        Vector2 bulletSpeed, bulletPos;
        double bulletTotalSpeed = 10;

        public Bullet(Vector2 bulletStartPos, Vector2 bulletGoal)
        {
            mouseState = Mouse.GetState();
            bulletPos = bulletStartPos;
            SetBulletTrajectory(bulletStartPos, bulletGoal);
        }

        public void MoveBullet()
        {
            bulletPos += bulletSpeed;
        }

        public Vector2 GetBulletPos()
        {
            return bulletPos;
        }

        private Vector2 SetBulletTrajectory(Vector2 bulletStartPos, Vector2 bulletGoal)
        {
            double angle;

            angle = Math.Atan((bulletStartPos.Y - bulletGoal.Y) / (bulletStartPos.X - bulletGoal.X));

            if (bulletGoal.X > bulletStartPos.X)
            {
                bulletSpeed.X = (float)Math.Round(Math.Cos(angle) * bulletTotalSpeed);
                bulletSpeed.Y = (float)Math.Round(Math.Sin(angle) * bulletTotalSpeed);
            }

            else 
            {
                bulletSpeed.X = (float)Math.Round(Math.Cos(angle + Math.PI) * bulletTotalSpeed);
                bulletSpeed.Y = (float)Math.Round(Math.Sin(angle + Math.PI) * bulletTotalSpeed);
            }

            return bulletSpeed;
        }
    }
}
