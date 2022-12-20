using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{

    internal class Player
    {
        #region Variables

        Texture2D[] playerSprites = GlobalConstants.PlayerSprites;
        Texture2D player = GlobalConstants.PlayerSprites[0];
        Vector2 playerSpeed;
        Rectangle playerHitbox;
        MouseState mouseState;
        public Vector2 playerPos;

        #endregion

        #region Methods

        public Player()
        {
            playerPos.Y = (GlobalConstants.ScreenHeight - player.Height) / 2;
            playerPos.X = (GlobalConstants.ScreenWidth - player.Width) / 2;

            playerSpeed.X = GlobalConstants.PlayerSpeed;
            playerSpeed.Y = GlobalConstants.PlayerSpeed;
        }

        public void ChangeProtagonistSprite()
        {
            mouseState = Mouse.GetState();

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
            if ((playerPos.X >= (GlobalConstants.ScreenWidth - player.Width) && Keyboard.GetState().IsKeyDown(Keys.D)) || (playerPos.X <= 0 && Keyboard.GetState().IsKeyDown(Keys.A)))
            {
                if (playerPos.X < 0)
                {
                    playerPos.X = 0;
                }

                if (playerPos.X >= GlobalConstants.ScreenWidth - player.Width)
                {
                    playerPos.X = GlobalConstants.ScreenWidth - player.Width;
                }

                playerSpeed.X = 0;
            }

            else
            {
                playerSpeed.X = GlobalConstants.PlayerSpeed;
            }

            if ((playerPos.Y >= (GlobalConstants.ScreenHeight - player.Height) && Keyboard.GetState().IsKeyDown(Keys.S)) || (playerPos.Y <= 0 && Keyboard.GetState().IsKeyDown(Keys.W)))
            {
                if (playerPos.Y < 0)
                {
                    playerPos.Y = 0;
                }

                if (playerPos.Y >= GlobalConstants.ScreenHeight - player.Height)
                {
                    playerPos.Y = GlobalConstants.ScreenHeight - player.Height;
                }

                playerSpeed.Y = 0;
            }
            else
            {
                playerSpeed.Y = GlobalConstants.PlayerSpeed;
            }
        }

        public Texture2D GetPlayerSprite()
        {
            return player;
        }

        public Rectangle GetPlayerHitbox()
        {
            return playerHitbox = new Rectangle((int)playerPos.X, (int)playerPos.Y, player.Width, player.Height);
        }

        #endregion
    }
}
