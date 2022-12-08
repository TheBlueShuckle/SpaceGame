using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace SpaceGame
{
    internal class ScenePlanet
    {
        int windowHeight, windowWidth, scene = Constants.OnPlanet;
        DateTime leavePlanetCooldown;
        Texture2D protagonist;
        Texture2D[] protagonistSprites;
        Vector2 protagonistPos;
        Vector2 protagonistSpeed;
        Rectangle protagonistHitBox;

        public ScenePlanet(int windowHeight, int windowWidth, Texture2D[] protagonistSprites)
        {
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
            this.protagonistSprites = protagonistSprites;
            protagonist = protagonistSprites[0];

            protagonistPos.Y = (windowHeight - protagonist.Height) / 2;
            protagonistPos.X = (windowWidth - protagonist.Width) / 2;

            protagonistSpeed.X = Constants.ProtagonistSpeedConst;
            protagonistSpeed.Y = Constants.ProtagonistSpeedConst;

            leavePlanetCooldown = DateTime.Now.Add(new TimeSpan(0, 0, 5));
        }

        public void Initialize()
        {

        }

        public int Update()
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.E) && DateTime.Now > leavePlanetCooldown)
            {
                scene = Constants.InSpace;
            }

            CheckMove();
            CheckBounds();

            return scene;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(protagonist, protagonistPos, Color.White);
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
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                protagonistPos.Y += protagonistSpeed.Y;
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
                protagonistSpeed.X = Constants.ProtagonistSpeedConst;
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
                protagonistSpeed.Y = Constants.ProtagonistSpeedConst;
            }
        }
    }
}
