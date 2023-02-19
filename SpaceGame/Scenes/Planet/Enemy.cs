using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceGame.Scenes.Planet
{
    internal class Enemy
    {
        #region Variables

        const int MaxHealth = 75, HealthBarWidth = 50, HealthBarHeight = 10;
        const float criticalHealth = 0.2f;

        Vector2 pos, healthBarPos;
        public Point randomTargetPos;
        Vector2 speed;
        double totalSpeed = 2.5;
        float currentDir = 0;
        Texture2D currentSprite;
        Random rnd = new Random();
        int health = MaxHealth;
        DateTime shootCooldown;

        #endregion

        // Lägg till health, damage mm.

        #region Methods

        public Enemy()
        {
            currentSprite = GlobalConstants.EnemySprites[0];

            pos.X = rnd.Next(0, GlobalConstants.ScreenWidth);
            pos.Y = rnd.Next(0, GlobalConstants.ScreenHeight);
            randomTargetPos = GenerateRandomPoint();
        }

        public void Move(Point targetPos)
        {
            double angle;

            angle = Math.Atan((pos.Y - targetPos.Y) / (pos.X - targetPos.X));

            if (pos.X > targetPos.X)
            {
                SetSpeed(angle + Math.PI);
            }

            else
            {
                SetSpeed(angle);
            }

            pos += speed;

            pos = new Vector2((int)pos.X, (int)pos.Y);
        }

        public Point GenerateRandomPoint()
        {
            return randomTargetPos = new Point(rnd.Next(0, GlobalConstants.ScreenWidth), rnd.Next(0, GlobalConstants.ScreenHeight));
        }

        public Rectangle MeleeRange()
        {
            return new Rectangle((int)GetPosition().X - 75, (int)GetPosition().Y - 75, GetCurrentSprite().Width + 150, GetCurrentSprite().Height + 150);
        }

        public Rectangle fieldOfView()
        {
            return new Rectangle((int)GetPosition().X - 150, (int)GetPosition().Y - 150, GetCurrentSprite().Width + 300, currentSprite.Height + 300);
        }

        public Bullet Shoot(Vector2 playerPos)
        {
            shootCooldown = DateTime.Now.AddMilliseconds(1000);
            return new Bullet(new Vector2(pos.X + currentSprite.Width / 2, pos.Y + currentSprite.Height / 2), playerPos);
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
            speed.X = (float)Math.Cos(angle) * (float)totalSpeed;
            speed.Y = (float)Math.Sin(angle) * (float)totalSpeed;
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

        public void ChangeHealth()
        {
            health -= 20;
        }

        public int GetHealth()
        {
            return health;
        }

        public void UpdateHealthBarPos()
        {
            healthBarPos = new Vector2(pos.X + currentSprite.Width / 2 - HealthBarWidth / 2, pos.Y - HealthBarHeight - 5);
        }

        private int UpdatedHealthBarWidth()
        {
            double value = HealthBarWidth * health / MaxHealth;
            return (int)value;
        }

        public void DrawHealthBar()
        {
            GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, new Rectangle((int)healthBarPos.X, (int)healthBarPos.Y, HealthBarWidth, HealthBarHeight), Color.Gray);

            if (health <= MaxHealth * criticalHealth)
            {
                GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, new Rectangle((int)healthBarPos.X, (int)healthBarPos.Y, UpdatedHealthBarWidth(), HealthBarHeight), Color.Red);
            }

            else
            {
                GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, new Rectangle((int)healthBarPos.X, (int)healthBarPos.Y, UpdatedHealthBarWidth(), HealthBarHeight), Color.Green);
            }
        }

        #endregion
    }
}