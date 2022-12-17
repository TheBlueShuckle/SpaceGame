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
        Vector2 bulletSpeed;
        Vector2 bulletPos;
        double bulletTotalSpeed = 10;

        public Gun()
        {

        }

        public Vector2 SpawnBullet(Vector2 playerPos)
        {
            bulletPos = playerPos;

            return bulletPos;
        }

        public Vector2 MoveBullets(Vector2 playerPos)
        {
            double angle;

            mouseState = Mouse.GetState();

            bulletPos = playerPos;
            angle = Math.Atan((bulletPos.Y - mouseState.Y) / (bulletPos.X - mouseState.X));

            if (mouseState.X > playerPos.X)
            {
                bulletSpeed.X = (float)Math.Round(Math.Cos(angle + Math.PI) * bulletTotalSpeed);
                bulletSpeed.Y = (float)Math.Round(Math.Sin(angle + Math.PI) * bulletTotalSpeed);
            }

            else
            {
                bulletSpeed.X = (float)Math.Round(Math.Cos(angle) * bulletTotalSpeed);
                bulletSpeed.Y = (float)Math.Round(Math.Sin(angle) * bulletTotalSpeed);
            }

            return bulletSpeed;
        }
    }
}
