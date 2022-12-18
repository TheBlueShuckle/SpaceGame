using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
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
        double bulletTotalSpeed = 10, angle;
        Rectangle bulletRectangle;
        Vector2 bulletOrigin;

        public Bullet(Vector2 bulletStartPos, Vector2 bulletGoal)
        {
            mouseState = Mouse.GetState();
            bulletPos = bulletStartPos;
            SetBulletTrajectory(bulletStartPos, bulletGoal);
        }

        public void MoveBullet()
        {
            bulletPos += bulletSpeed;

            bulletRectangle = new Rectangle((int)bulletPos.X, (int)bulletPos.Y, GlobalConstants.Bullet.Width, GlobalConstants.Bullet.Height);
            bulletOrigin = new Vector2(bulletRectangle.Width / 2, bulletRectangle.Height / 2);
        }

        public Vector2 GetBulletPos()
        {
            return bulletPos;
        }

        private Vector2 SetBulletTrajectory(Vector2 bulletStartPos, Vector2 bulletGoal)
        {
            angle = Math.Atan((bulletStartPos.Y - bulletGoal.Y) / (bulletStartPos.X - bulletGoal.X));

            if (bulletGoal.X > bulletStartPos.X)
            {
                bulletSpeed.X = (float)Math.Round(Math.Cos(angle) * bulletTotalSpeed, 7);
                bulletSpeed.Y = (float)Math.Round(Math.Sin(angle) * bulletTotalSpeed, 7);
            }

            else 
            {
                bulletSpeed.X = (float)Math.Round(Math.Cos(angle + Math.PI) * bulletTotalSpeed, 7);
                bulletSpeed.Y = (float)Math.Round(Math.Sin(angle + Math.PI) * bulletTotalSpeed, 7);
            }

            return bulletSpeed;
        }

        public void DrawBullet()
        {
            GlobalConstants.SpriteBatch.Draw(GlobalConstants.Bullet, bulletPos, null, Color.White, (float) angle + (float) (Math.PI / 2), bulletOrigin, 1f, SpriteEffects.None, 0);
        }

        public Rectangle GetRectangle()
        {
            return bulletRectangle;
        }
    }
}
