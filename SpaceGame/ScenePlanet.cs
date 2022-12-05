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
        const int InSpace = 1, OnPlanet = 2;
        int windowHeight, windowWidth, scene = OnPlanet;
        DateTime leavePlanetCooldown;

        public ScenePlanet(int windowHeight, int windowWidth)
        {
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;

            leavePlanetCooldown = DateTime.Now.Add(new TimeSpan(0, 0, 5));
        }

        public void Initialize()
        {

        }

        public int Update()
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.E) && DateTime.Now > leavePlanetCooldown)
            {
                scene = InSpace;
            }
            

            return scene;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
