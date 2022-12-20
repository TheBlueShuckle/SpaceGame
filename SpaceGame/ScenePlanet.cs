using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace SpaceGame
{
    internal class ScenePlanet
    {
        int scene = GlobalConstants.OnPlanet;
        MouseState mouseState;

        Player player = new Player();

        DateTime leavePlanetCooldown, bulletCooldown, enemyBulletCooldown;
        List<Enemy> enemies = new List<Enemy>();
        List<Bullet> bullets = new List<Bullet>();
        List<Bullet> enemyBullets = new List<Bullet>();

        public ScenePlanet()
        {
            leavePlanetCooldown = DateTime.Now.Add(new TimeSpan(0, 0, GlobalConstants.PlanetWaitSecondsMin));
        }

        public int Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.E) && DateTime.Now > leavePlanetCooldown)
            {
                scene = GlobalConstants.InSpace;
            }

            SpawnEnemies();

            player.CheckMove();
            CheckShooting();

            MoveBullets(bullets);
            MoveBullets(enemyBullets);
            MoveEnemies();

            if(enemyBulletCooldown < DateTime.Now)
            {
                foreach (Enemy enemy in enemies)
                {
                    enemyBullets.Add(enemy.Shoot(player.playerPos));
                }

                enemyBulletCooldown = DateTime.Now.AddMilliseconds(1000);
            }

            CheckBulletHits();

            player.ChangeProtagonistSprite();
            player.CheckBounds();

            CheckBulletBounds();

            return scene;
        }

        public void Draw()
        {
            DrawEnemyMeleeRange();

            GlobalConstants.SpriteBatch.Draw(player.GetPlayerSprite(), player.playerPos, Color.White);

            DrawEnemies();
            DrawBullets(bullets);
            DrawBullets(enemyBullets);
        }

        private void SpawnEnemies()
        {
            while (enemies.Count < 5)
            {
                enemies.Add(new Enemy());
            }
        }

        private void MoveBullets(List<Bullet> bullets)
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.MoveBullet();
            }
        }

        private void MoveEnemies()
        {            
            foreach (Enemy enemy in enemies)
            {
                if (player.GetPlayerHitbox().Intersects(enemy.EnemyMeleeRange()))
                {
                    enemy.MoveEnemy(player.playerPos);
                }
            }
        }

        private void CheckShooting()
        {
            mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && bulletCooldown < DateTime.Now)
            {
                bullets.Add(new Bullet(new Vector2(player.playerPos.X + (player.GetPlayerSprite().Width / 2), player.playerPos.Y + (player.GetPlayerSprite().Height / 2)), new Vector2(mouseState.X, mouseState.Y)));
                bulletCooldown = DateTime.Now.AddMilliseconds(500);
            }
        }

        private void CheckBulletHits()
        {
            foreach (Bullet bullet in bullets.ToList())
            {
                foreach (Enemy enemy in enemies.ToList())
                {
                    if (bullet.GetRectangle().Intersects(enemy.GetHitbox()))
                    {
                        bullets.Remove(bullet);
                        enemies.Remove(enemy);
                    }
                }
            }
        }

        private void CheckBulletBounds()
        {
            foreach (Bullet bullet in bullets.ToList())
            {
                if(bullet.GetBulletPos().X <= 10 || 
                   bullet.GetBulletPos().Y <= 10 ||
                   bullet.GetBulletPos().X >= GlobalConstants.ScreenWidth || 
                   bullet.GetBulletPos().Y >= GlobalConstants.ScreenHeight)
                {
                    bullets.Remove(bullet);
                }
            }

            foreach (Bullet bullet in enemyBullets.ToList())
            {
                if (bullet.GetBulletPos().X <= 10 ||
                   bullet.GetBulletPos().Y <= 10 ||
                   bullet.GetBulletPos().X >= GlobalConstants.ScreenWidth - 10 ||
                   bullet.GetBulletPos().Y >= GlobalConstants.ScreenHeight - 10)
                {
                    enemyBullets.Remove(bullet);
                }
            }
        }

        private void DrawEnemies()
        {
            foreach (Enemy enemy in enemies)
            {
                GlobalConstants.SpriteBatch.Draw(enemy.GetCurrentEnemySprite(), enemy.GetEnemyPosition(), Color.Green);
            }
        }

        private void DrawEnemyMeleeRange()
        {
            foreach (Enemy enemy in enemies)
            {
                GlobalConstants.SpriteBatch.Draw(GlobalConstants.EnemyMeleeRange, enemy.EnemyMeleeRange(), Color.White);
            }
        }

        private void DrawBullets(List<Bullet> bullets)
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.DrawBullet();
            }
        }
    }
}
