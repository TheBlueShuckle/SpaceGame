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
        float desiredDuration = 3f, elapsedTime = 0;
        Vector2 startPos;

        // Lägg till health, damage mm.

        public Enemy()
        {
            this.enemySprites = GlobalConstants.EnemySprites;
            this.currentEnemySprite = enemySprites[0];
            enemyPos.X = rnd.Next(0, (int) Math.Round(GlobalConstants.ScreenWidth));
            enemyPos.Y = rnd.Next(0, (int) Math.Round(GlobalConstants.ScreenHeight));
            startPos = enemyPos;
        }

        public void MoveEnemy(Vector2 playerPos, GameTime gameTime)
        {
            double angle;
            float percentageComplete;

            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            percentageComplete = elapsedTime / desiredDuration;

            enemyPos = Vector2.Lerp(startPos, playerPos, percentageComplete);
            /*
            angle = Math.Atan((playerPos.Y - enemyPos.Y) / (playerPos.X - enemyPos.X));
            
            if(enemyPos.X > playerPos.X)
            {
                SetEnemySpeed(angle + Math.PI, enemyTotalSpeed);
            }
            else
            {
                SetEnemySpeed(angle, enemyTotalSpeed);
            }

            if(enemyPos.X == playerPos.X)
            {
                enemySpeed.X = 0;

                if(enemyPos.Y > playerPos.Y)
                {
                    enemySpeed.Y = 2.5f;
                }

                if (enemyPos.Y < playerPos.Y)
                {
                    enemySpeed.Y = 2.5f;
                }

                else
                {
                    enemySpeed = new Vector2(0, 0);
                }
            }
            */
        }
        private void SetEnemySpeed(double angle, double movementInDir)
        {
            enemySpeed.X = (float)Math.Round(Math.Cos(angle) * movementInDir);
            enemySpeed.Y = (float)Math.Round(Math.Tan(angle) * enemySpeed.X);
        }

        public Vector2 GetEnemyPosition()
        {
            return enemyPos;
        }

        public Texture2D GetCurrentEnemySprite()
        {
            return currentEnemySprite;
        }
    }
}