using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using SharpDX.XAudio2;
using SpaceGame.Main;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using System.Windows.Forms;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace SpaceGame.Scenes.Planet
{
    internal class ScenePlanet
    {
        #region Variables

        int scene = GlobalConstants.OnPlanet;
        MouseState mouseState;
        Random rnd = new Random();

        Player player = new Player();

        DateTime leavePlanetCooldown, bulletCooldown, damageCooldown = DateTime.Now.AddMilliseconds(500), enemyIdleTime;
        List<Enemy> enemies = new List<Enemy>();
        List<Bullet> bullets = new List<Bullet>();
        List<Bullet> enemyBullets = new List<Bullet>();
        List<HealthPack> healthPacks = new List<HealthPack>();

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

            CheckIfMoveEnemy();
            UpdateHealthBarPos();

            CheckPlayerDamage();

            CheckIfBulletHitsEnemies();
            CheckCollisionHealthPack();

            player.ChangeSprite();
            player.CheckBounds();

            CheckBulletBounds();

            return scene;
        }

        public void Draw()
        {
            foreach (Enemy enemy in enemies)
            {
                GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, enemy.fieldOfView(), Color.White);
            }

            DrawEnemyMeleeRange();

            DrawEnemies();
            DrawBullets(bullets);
            DrawBullets(enemyBullets);
            DrawHealthPacks(healthPacks);
            DrawHealthBars();

            foreach (Enemy enemy in enemies)
            {
                GlobalConstants.SpriteBatch.DrawString(GlobalConstants.GameFont, "X = " + Math.Round(enemy.GetPosition().X, 0) + " Y = " + Math.Round(enemy.GetPosition().Y, 0), new Vector2(10, 15 * enemies.IndexOf(enemy)), Color.Black);
            }

            GlobalConstants.SpriteBatch.Draw(player.GetSprite(), player.pos, Color.White);
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

        private void CheckIfMoveEnemy()
        {
            foreach (Enemy enemy in enemies)
            {
                if (GlobalMethods.CheckPointIntersects(enemy.MeleeRange(), GlobalMethods.GetCenter(player.pos, player.GetSprite().Width, player.GetSprite().Height)))
                {
                    enemy.Move(player.pos);
                }

                else if (!enemy.fieldOfView().Intersects(player.GetHitBox()))
                {
                    MoveToRandomPos(enemy);
                }
            }
        }

        private void MoveToRandomPos(Enemy enemy)
        {
            if (enemy.GetPosition() == enemy.randomTargetPos)
            {
                if (rnd.Next(1, 3) == 1 && enemyIdleTime <= DateTime.Now)
                {
                    enemy.Move(enemy.GenerateRandomPoint());
                }

                if (enemyIdleTime <= DateTime.Now)
                {
                    enemyIdleTime = DateTime.Now.AddMilliseconds(rnd.Next(1, 5) * 100);
                }
            }

            else
            {
                enemy.Move(enemy.randomTargetPos);
            }
        }

        private void CheckShooting()
        {
            mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && bulletCooldown < DateTime.Now)
            {
                bullets.Add(new Bullet(new Vector2(player.pos.X + player.GetSprite().Width / 2, player.pos.Y + player.GetSprite().Height / 2), new Vector2(mouseState.X, mouseState.Y)));
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
                    player.DamageTaken();

                    CheckIfPlayerIsDead();
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

        private void CheckIfPlayerIsDead()
        {
            if (player.GetHealth() < 1)
            {
                scene = GlobalConstants.InSpace;
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
                    GenerateHealthPack(enemy);
                }
            }
        }

        private void GenerateHealthPack(Enemy enemy)
        {
            Random rand = new Random();

            if (rand.Next(1, 5) == 1)
            {
                healthPacks.Add(new HealthPack(enemy.GetPosition()));
            }
        }

        private void CheckCollisionHealthPack()
        {
            foreach (HealthPack healthPack in healthPacks.ToList())
            {
                if (player.GetHitBox().Intersects(healthPack.GetRectangle()) && player.GetHealth() < 100)
                {
                    player.AddHealth(healthPack.GetValue());
                    healthPacks.Remove(healthPack);
                }
            }
        }

        private void CheckBulletBounds()
        {
            foreach (Bullet bullet in bullets.ToList())
            {
                if (bullet.GetPos().X <= 10 ||
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

        private void CheckPlayerDamage()
        {
            if (damageCooldown <= DateTime.Now)
            {
                CheckIfBulletHitsPlayer();
                CheckEnemyMelee();

                damageCooldown = DateTime.Now.AddMilliseconds(500);
            }
        }

        private void CheckEnemyMelee()
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.GetHitbox().Intersects(player.GetHitBox()))
                {
                    player.DamageTaken();
                    CheckIfPlayerIsDead();
                }
            }
        }

        private void UpdateHealthBarPos()
        {
            player.UpdateHealthBarPos();

            foreach (Enemy enemy in enemies)
            {
                enemy.UpdateHealthBarPos();
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

        private void DrawHealthPacks(List<HealthPack> healthPacks)
        {
            foreach (HealthPack healthPack in healthPacks)
            {
                healthPack.Draw();
            }
        }

        private void DrawHealthBars()
        {
            player.DrawHealthBar();

            foreach (Enemy enemy in enemies)
            {
                enemy.DrawHealthBar();
            }
        }

        #endregion
    }
}
