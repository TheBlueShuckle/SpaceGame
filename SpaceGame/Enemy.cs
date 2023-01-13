using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceGame
{
    internal class Enemy
    {
        #region Variables

        Vector2 pos;
        Vector2 speed;
        double totalSpeed = 2.5;
        Texture2D currentSprite;
        Random rnd = new Random();
        int health;
        DateTime shootCooldown;

        #endregion

        // Lägg till health, damage mm.

        #region Methods

        public Enemy()
        {
            this.currentSprite = GlobalConstants.EnemySprites[0];

            pos.X = rnd.Next(0, (int)Math.Round(GlobalConstants.ScreenWidth));
            pos.Y = rnd.Next(0, (int)Math.Round(GlobalConstants.ScreenHeight));
        }

        public void Move(Vector2 playerPos)
        {
            double angle;

            angle = Math.Atan((pos.Y - playerPos.Y) / (pos.X - playerPos.X));

            if (pos.X > playerPos.X)
            {
                SetSpeed(angle + Math.PI);
            }

            else
            {
                SetSpeed(angle);
            }

            pos += speed;
        }

        public Rectangle MeleeRange()
        {
            return new Rectangle((int)GetPosition().X - 50, (int)GetPosition().Y - 50, GetCurrentSprite().Width + 100, GetCurrentSprite().Height + 100);
        }

        public Rectangle fieldOfView()
        {
            return new Rectangle((int)GetPosition().X - 50, (int)GetPosition().Y + currentSprite.Height + 50, GetCurrentSprite().Width + 100, currentSprite.Height + 350);
        }

        public Bullet Shoot(Vector2 playerPos)
        {
            shootCooldown = DateTime.Now.AddMilliseconds(1000);
            return new Bullet(new Vector2(pos.X + (currentSprite.Width / 2), pos.Y + (currentSprite.Height / 2)), playerPos);
        }

        public bool ShootCooldown()
        {
            if (shootCooldown > DateTime.Now)
            {
                return false;
            }

            return true;
        }

        private void SetSpeed(double angle)
        {
            speed.X = (float)Math.Round(Math.Cos(angle) * totalSpeed, 7);
            speed.Y = (float)Math.Round(Math.Sin(angle) * totalSpeed, 7);
        }

        public Vector2 GetPosition()
        {
            return pos;
        }

        public Texture2D GetCurrentSprite()
        {
            return currentSprite;
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, currentSprite.Width, currentSprite.Height);
        }

        #endregion
    }
}