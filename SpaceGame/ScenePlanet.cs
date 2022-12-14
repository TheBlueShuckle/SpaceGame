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
        int windowHeight, windowWidth, scene = Constants.OnPlanet;
        DateTime leavePlanetCooldown;
        Texture2D protagonist, enemy;
        Texture2D[] protagonistSprites, enemySprites;
        Vector2 protagonistPos, enemyPos;
        Vector2 protagonistSpeed, enemySpeed;
        Rectangle protagonistHitBox, enemyHitBox;
        MouseState mouseState;
        List<Vector2> enemyPosList = new List<Vector2>();
        Random rnd = new Random();

        public ScenePlanet(int windowHeight, int windowWidth, Texture2D[] protagonistSprites, Texture2D[] enemySprites)
        {
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
            this.protagonistSprites = protagonistSprites;
            protagonist = protagonistSprites[0];

            protagonistPos.Y = (windowHeight - protagonist.Height) / 2;
            protagonistPos.X = (windowWidth - protagonist.Width) / 2;

            protagonistSpeed.X = Constants.ProtagonistSpeed;
            protagonistSpeed.Y = Constants.ProtagonistSpeed;



            leavePlanetCooldown = DateTime.Now.Add(new TimeSpan(0, 0, Constants.PlanetWaitSecondsMin));
        }

        public int Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.E) && DateTime.Now > leavePlanetCooldown)
            {
                scene = Constants.InSpace;
            }

            while (enemyPosList.Count <= 5)
            {
                enemyPos.X = rnd.Next(0, windowWidth);
                enemyPos.Y = rnd.Next(0, windowHeight);
                enemyPosList.Add(enemyPos);
            }

            foreach (Vector2 enemy in enemyPosList.ToList())
            {
                if(enemy.X > protagonistPos.X)
                {
                    enemySpeed.X = 1.5f;
                    enemySpeed.Y = 0;

                    enemyPosList[enemyPosList.IndexOf(enemy)] -= enemySpeed;
                }

                if (protagonistPos.X > enemy.X)
                {
                    enemySpeed.X = 1.5f;
                    enemySpeed.Y = 0;

                    enemyPosList[enemyPosList.IndexOf(enemy)] += enemySpeed;
                }

                if (enemy.Y > protagonistPos.Y)
                {
                    enemySpeed.X = 0;
                    enemySpeed.Y = 1.5f;

                    enemyPosList[enemyPosList.IndexOf(enemy)] -= enemySpeed;
                }

                if (protagonistPos.X > enemy.X)
                {
                    enemySpeed.X = 0;
                    enemySpeed.Y = 1.5f;

                    enemyPosList[enemyPosList.IndexOf(enemy)] += enemySpeed;
                }
            }

            CheckMove();
            CheckBounds();

            ChangeProtagonistSprite();

            return scene;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(protagonist, protagonistPos, Color.White);

            foreach (Vector2 enemy in enemyPosList)
            {
                spriteBatch.Draw(this.enemy, enemy, Color.Green);
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
                protagonistSpeed.X = Constants.ProtagonistSpeed;
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
                protagonistSpeed.Y = Constants.ProtagonistSpeed;
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
