using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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
        List<Enemy> enemies = new List<Enemy>();
        Random rnd = new Random();

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

            while (enemies.Count < 5)
            {
                enemies.Add(new Enemy());
            }

            player.CheckMove();
            
            foreach(Enemy enemy in enemies)
            {
                enemy.MoveEnemy(player.playerPos);
            }

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
        }
    }
}
