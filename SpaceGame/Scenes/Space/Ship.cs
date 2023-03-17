using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame.Scenes.Space
{
    internal class Ship
    {
        Texture2D currentShipSprite;
        Vector2 shipPos;
        Vector2 shipSpeed;
        DateTime nextShipFrame;

        public Ship()
        {
            currentShipSprite = GlobalConstants.ShipSprites[0];

            shipPos.X = (GlobalConstants.ScreenWidth - currentShipSprite.Width) / 2;
            shipPos.Y = (GlobalConstants.ScreenHeight - currentShipSprite.Height) / 2;

            shipSpeed = GlobalConstants.ShipSpeed;
        }

        public void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                shipPos.X -= GlobalConstants.ShipSpeed.X;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                shipPos.X += GlobalConstants.ShipSpeed.X;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                shipPos.Y -= GlobalConstants.ShipSpeed.Y;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                shipPos.Y += GlobalConstants.ShipSpeed.Y;
            }
        }

        public void CheckBounds()
        {
            if (shipPos.X >= GlobalConstants.ScreenWidth - currentShipSprite.Width && Keyboard.GetState().IsKeyDown(Keys.D) || shipPos.X <= 0 && Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (shipPos.X < 0)
                {
                    shipPos.X = 0;
                }

                if (shipPos.X >= GlobalConstants.ScreenWidth - currentShipSprite.Width)
                {
                    shipPos.X = GlobalConstants.ScreenWidth - currentShipSprite.Width;
                }

                shipSpeed.X = 0;
            }

            else
            {
                shipSpeed.X = GlobalConstants.ShipSpeed.X;
            }

            if (shipPos.Y >= GlobalConstants.ScreenHeight - 100 - currentShipSprite.Height && Keyboard.GetState().IsKeyDown(Keys.S) || shipPos.Y <= 100 && Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (shipPos.Y < 100)
                {
                    shipPos.Y = 100;
                }

                if (shipPos.Y >= GlobalConstants.ScreenHeight - 100 - currentShipSprite.Height)
                {
                    shipPos.Y = GlobalConstants.ScreenHeight - 100 - currentShipSprite.Height;
                }

                shipSpeed.Y = 0;
            }
            else
            {
                shipSpeed.Y = GlobalConstants.ShipSpeed.Y;
            }
        }

        public void Animate()
        {
            if (DateTime.Now > nextShipFrame)
            {
                currentShipSprite = currentShipSprite == GlobalConstants.ShipSprites[0] ? GlobalConstants.ShipSprites[1] : GlobalConstants.ShipSprites[0];
                nextShipFrame = DateTime.Now.AddSeconds(0.5);
            }
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int) shipPos.X, (int) shipPos.Y, currentShipSprite.Width, currentShipSprite.Height);
        }

        public Texture2D GetCurrentSprite()
        {
            return currentShipSprite;
        }

        public void Draw()
        {
            GlobalConstants.SpriteBatch.Draw(currentShipSprite, shipPos, Color.White);
        }
    }
}
