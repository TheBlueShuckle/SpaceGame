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

        public void MoveEnemy(Vector2 playerPos)
        {
            double angle;

            angle = Math.Atan((enemyPos.Y - playerPos.Y) / (enemyPos.X - playerPos.X));

            if (enemyPos.X > playerPos.X)
            {
                SetEnemySpeed(angle + Math.PI);
            }

            else
            {
                SetEnemySpeed(angle);
            }

            enemyPos += enemySpeed;
        }

        public Rectangle EnemyMeleeRange()
        {
            return enemyMeleeRange = new Rectangle((int)GetEnemyPosition().X - 50, (int)GetEnemyPosition().Y - 50, GetCurrentEnemySprite().Width + 100, GetCurrentEnemySprite().Height + 100);
        }

        public Rectangle EnemyVision()
        {
            return enemyVision = new Rectangle((int)GetEnemyPosition().X - 50, (int)GetEnemyPosition().Y + currentEnemySprite.Height + 50, GetCurrentEnemySprite().Width + 100, 300);
        }

        public Bullet Shoot(Vector2 playerPos)
        {
            shootCooldown = DateTime.Now.AddMilliseconds(1000);
            return new Bullet(enemyPos, playerPos);
        }

        public bool ShootCooldown()
        {
            if (DateTime.Now >= shootCooldown)
            {
                return false;
            }

            return true;
        }

        private void SetEnemySpeed(double angle)
        {
            enemySpeed.X = (float)Math.Round(Math.Cos(angle) * enemyTotalSpeed, 7);
            enemySpeed.Y = (float)Math.Round(Math.Sin(angle) * enemyTotalSpeed, 7);
        }

        public Vector2 GetEnemyPosition()
        {
            return enemyPos;
        }

        public Texture2D GetCurrentEnemySprite()
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