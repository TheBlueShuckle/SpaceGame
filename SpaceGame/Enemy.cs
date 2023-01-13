using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceGame
{
    internal class Enemy
    {
        #region Variables

        Vector2 enemyPos;
        Vector2 enemySpeed;
        double enemyTotalSpeed = 2.5;
        Rectangle enemyHitBox, enemyMeleeRange, enemyVision;
        Texture2D currentEnemySprite;
        Random rnd = new Random();
        int health;
        DateTime shootCooldown;

        #endregion

        // Lägg till health, damage mm.

        #region Methods

        public Enemy()
        {
            this.currentEnemySprite = GlobalConstants.EnemySprites[0];

            enemyPos.X = rnd.Next(0, (int)Math.Round(GlobalConstants.ScreenWidth));
            enemyPos.Y = rnd.Next(0, (int)Math.Round(GlobalConstants.ScreenHeight));
        }

        public void Move(Vector2 playerPos)
        {
            double angle;

            angle = Math.Atan((enemyPos.Y - playerPos.Y) / (enemyPos.X - playerPos.X));

            if (enemyPos.X > playerPos.X)
            {
                SetSpeed(angle + Math.PI);
            }

            else
            {
                SetSpeed(angle);
            }

            enemyPos += enemySpeed;
        }

        public Rectangle MeleeRange()
        {
            return enemyMeleeRange = new Rectangle((int)GetPosition().X - 50, (int)GetPosition().Y - 50, GetCurrentSprite().Width + 100, GetCurrentSprite().Height + 100);
        }

        public Rectangle Vision()
        {
            return enemyVision = new Rectangle((int)GetPosition().X - 50, (int)GetPosition().Y + currentEnemySprite.Height + 50, GetCurrentSprite().Width + 100, 300);
        }

        public Bullet Shoot(Vector2 playerPos)
        {
            shootCooldown = DateTime.Now.AddMilliseconds(1000);
            return new Bullet(new Vector2(enemyPos.X + (currentEnemySprite.Width / 2), enemyPos.Y + (currentEnemySprite.Height / 2)), playerPos);
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
            enemySpeed.X = (float)Math.Round(Math.Cos(angle) * enemyTotalSpeed, 7);
            enemySpeed.Y = (float)Math.Round(Math.Sin(angle) * enemyTotalSpeed, 7);
        }

        public Vector2 GetPosition()
        {
            return enemyPos;
        }

        public Texture2D GetCurrentSprite()
        {
            return currentEnemySprite;
        }

        public Rectangle GetHitbox()
        {
            return enemyHitBox = new Rectangle((int)enemyPos.X, (int)enemyPos.Y, currentEnemySprite.Width, currentEnemySprite.Height);
        }

        #endregion
    }
}