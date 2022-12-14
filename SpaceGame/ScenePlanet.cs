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
        int windowHeight, windowWidth, scene = GlobalConstants.OnPlanet;
        DateTime leavePlanetCooldown;
        Texture2D protagonist;
        Texture2D[] protagonistSprites, enemySprites;
        Vector2 protagonistPos;
        Vector2 protagonistSpeed;
        Rectangle protagonistHitBox;
        MouseState mouseState;
        List<Enemy> enemies = new List<Enemy>();
        Random rnd = new Random();

        public ScenePlanet(int windowHeight, int windowWidth, Texture2D[] protagonistSprites, Texture2D[] enemySprites)
        {
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
            this.protagonistSprites = protagonistSprites;
            this.enemySprites = enemySprites;
            protagonist = protagonistSprites[0];

            protagonistPos.Y = (windowHeight - protagonist.Height) / 2;
            protagonistPos.X = (windowWidth - protagonist.Width) / 2;

            protagonistSpeed.X = GlobalConstants.ProtagonistSpeed;
            protagonistSpeed.Y = GlobalConstants.ProtagonistSpeed;



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
                enemies.Add(new Enemy(enemySprites));
            }

            CheckMove();
            CheckBounds();

            ChangeProtagonistSprite();

            return scene;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(protagonist, protagonistPos, Color.White);

            foreach (Enemy enemy in enemies)
            {
                spriteBatch.Draw(enemy.GetCurrentEnemySprite(), enemy.GetEnemyPosition(), Color.Green);
            }
        }

        public void CheckMove()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                protagonistPos.X -= protagonistSpeed.X;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                protagonistPos.X += protagonistSpeed.X;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                protagonistPos.Y -= protagonistSpeed.Y;

                if (protagonist == protagonistSprites[2] || protagonist == protagonistSprites[1])
                {
                    protagonist = protagonistSprites[2];
                }

                else
                {
                    protagonist = protagonistSprites[3];
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                protagonistPos.Y += protagonistSpeed.Y;

                if (protagonist == protagonistSprites[2] || protagonist == protagonistSprites[1])
                {
                    protagonist = protagonistSprites[1];
                }

                else
                {
                    protagonist = protagonistSprites[0];
                }
            }
        }

        public void CheckBounds()
        {
            if ((protagonistPos.X >= (windowWidth - protagonist.Width) && Keyboard.GetState().IsKeyDown(Keys.D)) || (protagonistPos.X <= 0 && Keyboard.GetState().IsKeyDown(Keys.A)))
            {
                if (protagonistPos.X < 0)
                {
                    protagonistPos.X = 0;
                }

                if (protagonistPos.X >= windowWidth - protagonist.Width)
                {
                    protagonistPos.X = windowWidth - protagonist.Width;
                }

                protagonistSpeed.X = 0;
            }

            else
            {
                protagonistSpeed.X = GlobalConstants.ProtagonistSpeed;
            }

            if ((protagonistPos.Y >= (windowHeight - protagonist.Height) && Keyboard.GetState().IsKeyDown(Keys.S)) || (protagonistPos.Y <= 0 && Keyboard.GetState().IsKeyDown(Keys.W)))
            {
                if (protagonistPos.Y < 0)
                {
                    protagonistPos.Y = 0;
                }

                if (protagonistPos.Y >= windowHeight - protagonist.Height)
                {
                    protagonistPos.Y = windowHeight - protagonist.Height;
                }

                protagonistSpeed.Y = 0;
            }
            else
            {
                protagonistSpeed.Y = GlobalConstants.ProtagonistSpeed;
            }
        }

        private void ChangeProtagonistSprite()
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.X >= (protagonistPos.X + (protagonist.Width / 2)))
            {
                if (protagonist == protagonistSprites[2] || protagonist == protagonistSprites[3])
                {
                    protagonist = protagonistSprites[2];
                }

                else
                {
                    protagonist = protagonistSprites[1];
                }
            }

            else if (protagonist == protagonistSprites[2] || protagonist == protagonistSprites[3])
            {
                protagonist = protagonistSprites[3];
            }

            else
            {
                protagonist = protagonistSprites[0];
            }
        }
    }
}
