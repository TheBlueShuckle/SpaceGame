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
        #region Variables

        Vector2 speed, pos;
        double totalSpeed = 10, angle;
        Rectangle rectangle;
        Vector2 origin;

        #endregion

        #region Methods

        public Bullet(Vector2 bulletStartPos, Vector2 bulletGoal)
        {
            pos = bulletStartPos;
            SetTrajectory(bulletStartPos, bulletGoal);
        }

        public void Move()
        {
            pos += speed;

            rectangle = new Rectangle((int)pos.X, (int)pos.Y, GlobalConstants.Bullet.Width, GlobalConstants.Bullet.Height);
            origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
        }

        public Vector2 GetPos()
        {
            return pos;
        }

        private Vector2 SetTrajectory(Vector2 bulletStartPos, Vector2 bulletGoal)
        {
            angle = Math.Atan((bulletStartPos.Y - bulletGoal.Y) / (bulletStartPos.X - bulletGoal.X));

            if (bulletGoal.X > bulletStartPos.X)
            {
                speed.X = (float)Math.Round(Math.Cos(angle) * totalSpeed, 7);
                speed.Y = (float)Math.Round(Math.Sin(angle) * totalSpeed, 7);
            }

            else 
            {
                speed.X = (float)Math.Round(Math.Cos(angle + Math.PI) * totalSpeed, 7);
                speed.Y = (float)Math.Round(Math.Sin(angle + Math.PI) * totalSpeed, 7);
            }

            return speed;
        }

        public void Draw()
        {
            GlobalConstants.SpriteBatch.Draw(
                GlobalConstants.Bullet, 
                pos, 
                null, 
                Color.White, 
                (float) angle + (float) (Math.PI / 2), 
                origin, 
                1f, 
                SpriteEffects.None, 
                0);
        }

        public Rectangle GetRectangle()
        {
            return rectangle;
        }

        #endregion
    }
}
