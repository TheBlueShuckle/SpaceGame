using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Dynamic;

namespace SpaceGame.Scenes.Planet
{
    internal class Enemy
    {
        #region Variables

        const int MaxHealth = 75, HealthBarWidth = 11 * PixelSize, HealthBarHeight = 2 * PixelSize, PixelSize = 3;
        const float criticalHealth = 0.2f;

        public int enemystate;
        public Vector2 randomTargetPos;
        Vector2 pos, healthBarPos;
        Vector2 speed = new Vector2(2.5f, 2.5f);
        Texture2D currentSprite;
        Random rnd = new Random();
        int health = MaxHealth;
        DateTime shootCooldown;

        #endregion

        #region Methods

        public Enemy()
        {
            currentSprite = GlobalConstants.EnemySprites[0];

            pos.X = rnd.Next(0, GlobalConstants.ScreenWidth);
            pos.Y = rnd.Next(0, GlobalConstants.ScreenHeight);
            GenerateRandomPoint();
        }

        public void Move(Vector2 goal)
        {
            Vector2 direction;

            if (goal == pos)
            {
                direction = new Vector2(0, 0);
            }

            else
            {
                direction = Vector2.Normalize(goal - pos);
            }

            pos += direction * speed;

            if (Math.Abs(Vector2.Dot(direction, Vector2.Normalize(goal - pos)) + 1) < 0.1f)
            {
                pos = goal;
            }
        }

        public Vector2 GenerateRandomPoint()
        {
            return randomTargetPos = new Vector2(rnd.Next(0, GlobalConstants.ScreenWidth), rnd.Next(0, GlobalConstants.ScreenHeight));
        }

        public Vector2 GetPosition()
        {
            return pos;
        }

        public Rectangle MeleeRange()
        {
            return new Rectangle((int)GetPosition().X - 100, (int)GetPosition().Y - 100, GetCurrentSprite().Width + 200, GetCurrentSprite().Height + 200);
        }

        public Rectangle fieldOfView()
        {
            return new Rectangle((int)GetPosition().X - 300, (int)GetPosition().Y - 300, GetCurrentSprite().Width + 600, currentSprite.Height + 600);
        }

        public Bullet Shoot(Vector2 playerPos)
        {
            shootCooldown = DateTime.Now.AddMilliseconds(1000);
            return new Bullet(new Vector2(pos.X + currentSprite.Width / 2, pos.Y + currentSprite.Height / 2), playerPos, 20);
        }

        public bool ShootCooldown()
        {
            if (shootCooldown > DateTime.Now)
            {
                return false;
            }

            return true;
        }

        public Texture2D GetCurrentSprite()
        {
            return currentSprite;
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, currentSprite.Width, currentSprite.Height);
        }

        public void ChangeHealth(int damageTaken)
        {
            health -= damageTaken;
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

        public void Draw()
        {
            GlobalConstants.SpriteBatch.Draw(GetCurrentSprite(), GetPosition(), Color.Green);
        }

        public void DrawMeleeRange()
        {
            GlobalConstants.SpriteBatch.Draw(GlobalConstants.EnemyMeleeRange, MeleeRange(), Color.White);
        }

        public void DrawHealthBar()
        {
            GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, new Rectangle((int)healthBarPos.X - PixelSize, (int)healthBarPos.Y - PixelSize, HealthBarWidth + 2 * PixelSize, HealthBarHeight + 2 * PixelSize), Color.Black);

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

        public void ChangeToWanderingSpeed()
        {
            speed = new Vector2(1.5f, 1.5f);
        }

        public void ChangeToChasingSpeed()
        {
            speed = new Vector2(4f, 4f);
        }

        #endregion
    }
}