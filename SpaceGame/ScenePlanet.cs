using Microsoft.VisualBasic.Devices;
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
        int windowHeight, windowWidth, isInSpace = 2;
        bool checkKeyDown;
        DateTime leavePlanetCooldown;

        public ScenePlanet(int windowHeight, int windowWidth)
        {
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
        }

        public void Initialize()
        {
            checkKeyDown = false;
            leavePlanetCooldown = DateTime.Now;
            leavePlanetCooldown = leavePlanetCooldown.Add(new TimeSpan(0, 0, 5));
        }

        public int Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.E) && leavePlanetCooldown < DateTime.Now)
            {
                switch (checkKeyDown)
                {
                    case true:
                        checkKeyDown = false;
                        break;

                    case false:
                        checkKeyDown = true;
                        break;
                }

                switch (checkKeyDown)
                {
                    case true:
                        isInSpace = 1;
                        break;

                    case false:
                        isInSpace = 2;
                        break;
                }
            }

            return isInSpace;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
