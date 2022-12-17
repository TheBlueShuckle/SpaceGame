using Microsoft.Xna.Framework;
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
        Player player = new Player();
        int scene = GlobalConstants.OnPlanet;
        DateTime leavePlanetCooldown;
        Random rnd = new Random();
        List<Enemy> enemies = new List<Enemy>();
        MouseState mouseState;
        Gun gun = new Gun();
        List<Vector2> bullets;

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
                foreach (Vector2 bullet in bullets)
                {
                    spriteBatch.Draw(GlobalConstants.Bullet, bullet, Color.White);
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

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                bullets.Add(gun.SpawnBullet(player.playerPos));
            }
        }
    }
}
