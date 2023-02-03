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

        Texture2D currentSprite = GlobalConstants.PlayerSprites[0];
        Vector2 speed;
        MouseState mouseState;
        public Vector2 pos;
        int health;

        #endregion

        #region Methods

        public Player()
        {
            pos.Y = (GlobalConstants.ScreenHeight - currentSprite.Height) / 2;
            pos.X = (GlobalConstants.ScreenWidth - currentSprite.Width) / 2;

            speed.X = GlobalConstants.PlayerSpeed;
            speed.Y = GlobalConstants.PlayerSpeed;
        }

        public void ChangeSprite()
        {
            mouseState = Mouse.GetState();

            if (mouseState.X >= (pos.X + (currentSprite.Width / 2)))
            {
                if (currentSprite == GlobalConstants.PlayerSprites[2] || currentSprite == GlobalConstants.PlayerSprites[3])
                {
                    currentSprite = GlobalConstants.PlayerSprites[2];
                }

                else
                {
                    currentSprite = GlobalConstants.PlayerSprites[1];
                }
            }

            else if (currentSprite == GlobalConstants.PlayerSprites[2] || currentSprite == GlobalConstants.PlayerSprites[3])
            {
                currentSprite = GlobalConstants.PlayerSprites[3];
            }

            else
            {
                currentSprite = GlobalConstants.PlayerSprites[0];
            }
        }

        public void CheckMove()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                pos.X -= speed.X;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                pos.X += speed.X;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                pos.Y -= speed.Y;

                if (currentSprite == GlobalConstants.PlayerSprites[2] || currentSprite == GlobalConstants.PlayerSprites[1])
                {
                    currentSprite = GlobalConstants.PlayerSprites[2];
                }

                else
                {
                    currentSprite = GlobalConstants.PlayerSprites[3];
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                pos.Y += speed.Y;

                if (currentSprite == GlobalConstants.PlayerSprites[2] || currentSprite == GlobalConstants.PlayerSprites[1])
                {
                    currentSprite = GlobalConstants.PlayerSprites[1];
                }

                else
                {
                    currentSprite = GlobalConstants.PlayerSprites[0];
                }
            }
        }

        public void CheckBounds()
        {
            if ((pos.X >= (GlobalConstants.ScreenWidth - currentSprite.Width) && Keyboard.GetState().IsKeyDown(Keys.D)) || (pos.X <= 0 && Keyboard.GetState().IsKeyDown(Keys.A)))
            {
                if (pos.X < 0)
                {
                    pos.X = 0;
                }

                if (pos.X >= GlobalConstants.ScreenWidth - currentSprite.Width)
                {
                    pos.X = GlobalConstants.ScreenWidth - currentSprite.Width;
                }

                speed.X = 0;
            }

            else
            {
                speed.X = GlobalConstants.PlayerSpeed;
            }

            if ((pos.Y >= (GlobalConstants.ScreenHeight - currentSprite.Height) && Keyboard.GetState().IsKeyDown(Keys.S)) || (pos.Y <= 0 && Keyboard.GetState().IsKeyDown(Keys.W)))
            {
                if (pos.Y < 0)
                {
                    pos.Y = 0;
                }

                if (pos.Y >= GlobalConstants.ScreenHeight - currentSprite.Height)
                {
                    pos.Y = GlobalConstants.ScreenHeight - currentSprite.Height;
                }

                speed.Y = 0;
            }
            else
            {
                speed.Y = GlobalConstants.PlayerSpeed;
            }
        }

        public Texture2D GetSprite()
        {
            return currentSprite;
        }

        public Rectangle GetHitBox()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, currentSprite.Width, currentSprite.Height);
        }

        #endregion
    }
}
