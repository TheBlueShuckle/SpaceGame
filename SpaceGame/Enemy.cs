using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceGame
{
    internal class Enemy
    {
        Vector2 enemyPos;
        Vector2 enemySpeed;
        double enemyTotalSpeed = 2.5;
        Rectangle enemyHitBox, enemyMeleeRange;
        Texture2D currentEnemySprite;
        Random rnd = new Random();
        int health;

        // Lägg till health, damage mm.

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

        public Rectangle EnemyMeleeRange()
        {
            return enemyMeleeRange = new Rectangle((int)GetEnemyPosition().X - 50, (int)GetEnemyPosition().Y - 50, GetCurrentEnemySprite().Width + 100, GetCurrentEnemySprite().Height + 100);
        }

        public Bullet Shoot(Vector2 playerPos)
        {
            return new Bullet(enemyPos, playerPos);
        }
    }
}