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
        Rectangle enemyMeleeRange;

        Player player = new Player();

        DateTime leavePlanetCooldown, bulletCooldown;
        List<Enemy> enemies = new List<Enemy>();
        List<Bullet> bullets = new List<Bullet>();

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

            MoveBullets();
            MoveEnemies();
            CheckBulletHits();

            player.ChangeProtagonistSprite();
            player.CheckBounds();

            return scene;
        }

        public void Draw()
        {
            DrawEnemyMeleeRange();

            GlobalConstants.SpriteBatch.Draw(player.GetPlayerSprite(), player.playerPos, Color.White);

            DrawEnemies();
            DrawBullets();
        }

        private void SpawnEnemies()
        {
            while (enemies.Count < 5)
            {
                enemies.Add(new Enemy());
            }
        }

        private void MoveBullets()
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
                Texture2D _texture;

                _texture = new Texture2D(GlobalConstants.GraphicsDevice, 1, 1);
                _texture.SetData(new Color[] { Color.DarkSlateGray });

                GlobalConstants.SpriteBatch.Draw(_texture, enemy.EnemyMeleeRange(), Color.White);
            }
        }

        private void DrawBullets()
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.DrawBullet();
            }
        }
    }
}
