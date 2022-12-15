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
        Texture2D player;
        Vector2 playerPos;
        Vector2 playerSpeed;
        Rectangle playerHitBox;
        MouseState mouseState;
        List<Enemy> enemies = new List<Enemy>();
        Random rnd = new Random();

        public ScenePlanet(int windowHeight, int windowWidth)
        {
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;

            player = GlobalConstants.PlayerSprites[0];

            playerPos.Y = (windowHeight - player.Height) / 2;
            playerPos.X = (windowWidth - player.Width) / 2;

            playerSpeed.X = GlobalConstants.PlayerSpeed;
            playerSpeed.Y = GlobalConstants.PlayerSpeed;

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

            CheckMove();
            CheckBounds();

            ChangeProtagonistSprite();

            return scene;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(player, playerPos, Color.White);

            foreach (Enemy enemy in enemies)
            {
                spriteBatch.Draw(enemy.GetCurrentEnemySprite(), enemy.GetEnemyPosition(), Color.Green);
            }
        }

        public void CheckMove()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                playerPos.X -= playerSpeed.X;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                playerPos.X += playerSpeed.X;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                playerPos.Y -= playerSpeed.Y;

                if (player == GlobalConstants.PlayerSprites[2] || player == GlobalConstants.PlayerSprites[1])
                {
                    player = GlobalConstants.PlayerSprites[2];
                }

                else
                {
                    player = GlobalConstants.PlayerSprites[3];
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                playerPos.Y += playerSpeed.Y;

                if (player == GlobalConstants.PlayerSprites[2] || player == GlobalConstants.PlayerSprites[1])
                {
                    player = GlobalConstants.PlayerSprites[1];
                }

                else
                {
                    player = GlobalConstants.PlayerSprites[0];
                }
            }
        }

        public void CheckBounds()
        {
            if ((playerPos.X >= (windowWidth - player.Width) && Keyboard.GetState().IsKeyDown(Keys.D)) || (playerPos.X <= 0 && Keyboard.GetState().IsKeyDown(Keys.A)))
            {
                if (playerPos.X < 0)
                {
                    playerPos.X = 0;
                }

                if (playerPos.X >= windowWidth - player.Width)
                {
                    playerPos.X = windowWidth - player.Width;
                }

                playerSpeed.X = 0;
            }

            else
            {
                playerSpeed.X = GlobalConstants.PlayerSpeed;
            }

            if ((playerPos.Y >= (windowHeight - player.Height) && Keyboard.GetState().IsKeyDown(Keys.S)) || (playerPos.Y <= 0 && Keyboard.GetState().IsKeyDown(Keys.W)))
            {
                if (playerPos.Y < 0)
                {
                    playerPos.Y = 0;
                }

                if (playerPos.Y >= windowHeight - player.Height)
                {
                    playerPos.Y = windowHeight - player.Height;
                }

                playerSpeed.Y = 0;
            }
            else
            {
                playerSpeed.Y = GlobalConstants.PlayerSpeed;
            }
        }

        private void ChangeProtagonistSprite()
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.X >= (playerPos.X + (player.Width / 2)))
            {
                if (player == GlobalConstants.PlayerSprites[2] || player == GlobalConstants.PlayerSprites[3])
                {
                    player = GlobalConstants.PlayerSprites[2];
                }

                else
                {
                    player = GlobalConstants.PlayerSprites[1];
                }
            }

            else if (player == GlobalConstants.PlayerSprites[2] || player == GlobalConstants.PlayerSprites[3])
            {
                player = GlobalConstants.PlayerSprites[3];
            }

            else
            {
                player = GlobalConstants.PlayerSprites[0];
            }
        }
    }
}
