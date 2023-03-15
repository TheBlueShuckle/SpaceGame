﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceGame.Scenes.Planet
{
    internal class Boss
    {
        const int HealthBarWidth = 50, HealthBarHeight = 10;
        const float criticalHealth = 0.2f;

        public int enemystate, maxHealth;
        public Vector2 randomTargetPos;
        Vector2 pos, healthBarPos;
        Texture2D currentSprite;
        Random rnd = new Random();
        int health, damage;
        DateTime shootCooldown;

        public Boss()
        {
            currentSprite = GlobalConstants.EnemySprites[0];
            health = maxHealth = rnd.Next(5, 11) * 100;

            if (health < 700)
            {
                damage = rnd.Next(20, 36);
            }

            if (health < 900)
            {
                damage = rnd.Next(15, 26);
            }

            if (health == 1000)
            {
                damage = rnd.Next(10, 20);
            }
        }

        public Vector2 GetPosition()
        {
            return pos;
        }

        public Rectangle MeleeRange()
        {
            return new Rectangle((int)GetPosition().X - 75, (int)GetPosition().Y - 75, GetCurrentSprite().Width + 150, GetCurrentSprite().Height + 150);
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
            double value = HealthBarWidth * health / maxHealth;
            return (int)value;
        }

        public void DrawHealthBar()
        {
            GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, new Rectangle((int)healthBarPos.X, (int)healthBarPos.Y, HealthBarWidth, HealthBarHeight), Color.Gray);

            if (health <= maxHealth * criticalHealth)
            {
                GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, new Rectangle((int)healthBarPos.X, (int)healthBarPos.Y, UpdatedHealthBarWidth(), HealthBarHeight), Color.Red);
            }

            else
            {
                GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, new Rectangle((int)healthBarPos.X, (int)healthBarPos.Y, UpdatedHealthBarWidth(), HealthBarHeight), Color.Green);
            }
        }

        public int GetDamage()
        {
            return damage;
        }
    }
}
