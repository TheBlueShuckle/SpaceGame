﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace SpaceGame
{
    internal class ScenePlanet
    {
        #region Variables

        int scene = GlobalConstants.OnPlanet;
        MouseState mouseState;

        Player player = new Player();

        DateTime leavePlanetCooldown, bulletCooldown;
        List<Enemy> enemies = new List<Enemy>();
        List<Bullet> bullets = new List<Bullet>();
        List<Bullet> enemyBullets = new List<Bullet>();

        #endregion

        #region MainMethods

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

            CheckEnemyShooting();

            MoveBullets(bullets);
            MoveBullets(enemyBullets);
            MoveEnemies();

            CheckIfBulletHitsEnemies();
            CheckIfBulletHitsPlayer();

            player.ChangeSprite();
            player.CheckBounds();

            CheckBulletBounds();

            return scene;
        }

        public void Draw()
        {
            DrawEnemyMeleeRange();

            GlobalConstants.SpriteBatch.Draw(player.GetSprite(), player.pos, Color.White);

            DrawEnemies();
            DrawBullets(bullets);
            DrawBullets(enemyBullets);
        }

        #endregion

        #region Methods

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
                bullet.Move();
            }
        }

        private void MoveEnemies()
        {            
            foreach (Enemy enemy in enemies)
            {
                if(GlobalMethods.CheckPointIntersects(enemy.MeleeRange(), GlobalMethods.GetCenter(player.pos, player.GetSprite().Width, player.GetSprite().Height)))
                {
                    enemy.Move(player.pos);
                }
            }
        }

        private void CheckShooting()
        {
            mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && bulletCooldown < DateTime.Now)
            {
                bullets.Add(new Bullet(new Vector2(player.pos.X + (player.GetSprite().Width / 2), player.pos.Y + (player.GetSprite().Height / 2)), new Vector2(mouseState.X, mouseState.Y)));
                bulletCooldown = DateTime.Now.AddMilliseconds(500);
            }
        }

        private void CheckEnemyShooting()
        {
            foreach (Enemy enemy in enemies)
            {
                if (player.GetHitBox().Intersects(enemy.fieldOfView()) && enemy.ShootCooldown())
                {
                    enemyBullets.Add(enemy.Shoot(GlobalMethods.GetCenter(player.pos, player.GetSprite().Width, player.GetSprite().Height)));
                }
            }
        }

        private void CheckIfBulletHitsPlayer()
        {
            foreach (Bullet bullet in enemyBullets.ToList())
            {
                if (bullet.GetRectangle().Intersects(player.GetHitBox()))
                {
                    enemyBullets.Remove(bullet);
                    player.ChangeHealth();

                    if (player.GetHealth() < 1)
                    {
                        scene = GlobalConstants.InSpace;
                    }
                }
            }
        }

        private void CheckIfBulletHitsEnemies()
        {
            foreach (Bullet bullet in bullets.ToList())
            {
                foreach (Enemy enemy in enemies.ToList())
                {
                    IfBulletHit(enemy, bullet);
                }
            }
        }

        private void IfBulletHit(Enemy enemy, Bullet bullet)
        {
            if (GlobalMethods.CheckPointIntersects(enemy.GetHitbox(), GlobalMethods.GetCenter(bullet.GetPos(), GlobalConstants.Bullet.Width, GlobalConstants.Bullet.Height)))
            {
                bullets.Remove(bullet);
                enemy.ChangeHealth();

                if (enemy.GetHealth() < 1)
                {
                    enemies.Remove(enemy);
                }
            }
        }

        private void CheckBulletBounds()
        {
            foreach (Bullet bullet in bullets.ToList())
            {
                if(bullet.GetPos().X <= 10 || 
                   bullet.GetPos().Y <= 10 ||
                   bullet.GetPos().X >= GlobalConstants.ScreenWidth || 
                   bullet.GetPos().Y >= GlobalConstants.ScreenHeight)
                {
                    bullets.Remove(bullet);
                }
            }

            foreach (Bullet bullet in enemyBullets.ToList())
            {
                if (bullet.GetPos().X <= 10 ||
                   bullet.GetPos().Y <= 10 ||
                   bullet.GetPos().X >= GlobalConstants.ScreenWidth - 10 ||
                   bullet.GetPos().Y >= GlobalConstants.ScreenHeight - 10)
                {
                    enemyBullets.Remove(bullet);
                }
            }
        }

        private void DrawEnemies()
        {
            foreach (Enemy enemy in enemies)
            {
                GlobalConstants.SpriteBatch.Draw(enemy.GetCurrentSprite(), enemy.GetPosition(), Color.Green);
            }
        }

        private void DrawEnemyMeleeRange()
        {
            foreach (Enemy enemy in enemies)
            {
                GlobalConstants.SpriteBatch.Draw(GlobalConstants.EnemyMeleeRange, enemy.MeleeRange(), Color.White);
            }
        }
        
        private void DrawBullets(List<Bullet> bullets)
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw();
            }
        }

        #endregion
    }
}
