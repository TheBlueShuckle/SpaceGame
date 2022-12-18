﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;

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

            foreach (Bullet bullet in bullets)
            {
                bullet.MoveBullet();
            }
            MoveEnemies();

            player.ChangeProtagonistSprite();
            player.CheckBounds();

            return scene;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(player.GetPlayerSprite(), player.playerPos, Color.White);

            foreach (Enemy enemy in enemies)
            {
                spriteBatch.Draw(enemy.GetCurrentEnemySprite(), enemy.GetEnemyPosition(), Color.Green);
            }

            if(bullets != null)
            {
                foreach (Bullet bullet in bullets)
                {
                    spriteBatch.Draw(GlobalConstants.Bullet, bullet.GetBulletPos(), Color.White);
                }
            }
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

        private void CheckShooting()
        {
            mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && bulletCooldown < DateTime.Now)
            {
                bullets.Add(new Bullet(player.playerPos, new Vector2(mouseState.X, mouseState.Y)));
                bulletCooldown = DateTime.Now.AddMilliseconds(500);
            }
        }
    }
}
