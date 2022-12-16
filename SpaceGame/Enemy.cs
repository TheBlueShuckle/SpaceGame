using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    internal class Enemy
    {
        Vector2 enemyPos;
        Vector2 enemySpeed;
        double enemyTotalSpeed = 2.5;
        Rectangle enemyHitBox;
        Texture2D[] enemySprites;
        Texture2D currentEnemySprite;
        Random rnd = new Random();
        int health;

        // Lägg till health, damage mm.

        public Enemy()
        {
            this.enemySprites = GlobalConstants.EnemySprites;
            this.currentEnemySprite = enemySprites[0];
            enemyPos.X = rnd.Next(0, (int) Math.Round(GlobalConstants.ScreenWidth));
            enemyPos.Y = rnd.Next(0, (int) Math.Round(GlobalConstants.ScreenHeight));
        }

        public void MoveEnemy(Vector2 protagonistPos)
        {
            double angle;

            angle = Math.Atan((protagonistPos.Y - enemyPos.Y) / (protagonistPos.X - enemyPos.X));

            SetEnemySpeed(angle, enemyTotalSpeed);

            enemyPos += enemySpeed;
        }

        public Vector2 GetEnemyPosition()
        {
            return enemyPos;
        }

        public Texture2D GetCurrentEnemySprite()
        {
            return currentEnemySprite;
        }

        private void SetEnemySpeed(double angle, double movementInDir)
        {
            enemySpeed.X = (float) Math.Round(Math.Cos(angle + Math.PI) * movementInDir);
            enemySpeed.Y = (float) Math.Round(Math.Tan(angle + Math.PI) * enemySpeed.X);
        }
    }
}