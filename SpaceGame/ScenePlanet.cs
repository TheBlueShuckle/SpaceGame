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

        List<Enemy> enemies = new List<Enemy>();
        DateTime leavePlanetCooldown, bulletCooldown;
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

            player.ChangeProtagonistSprite();
            player.CheckBounds();

            return scene;
        }

        public void Draw()
        {
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

        private void MoveEnemies()
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.MoveEnemy(player.playerPos);
            }
        }

        private void MoveBullets()
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.MoveBullet();
            }
        }

        private void CheckShooting()
        {
            mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && bulletCooldown < DateTime.Now)
            {
                bullets.Add(new Bullet(player.playerPos, new Vector2(mouseState.X, mouseState.Y)));
                bulletCooldown = DateTime.Now.AddMilliseconds(500);
            }
        }

        private void DrawEnemies()
        {
            foreach (Enemy enemy in enemies)
            {
                GlobalConstants.SpriteBatch.Draw(enemy.GetCurrentEnemySprite(), enemy.GetEnemyPosition(), Color.Green);
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
