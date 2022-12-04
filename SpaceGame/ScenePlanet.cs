using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    internal class ScenePlanet
    {
        int windowHeight, windowWidth;

        public ScenePlanet(int windowHeight, int windowWidth)
        {
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
        }

        public void Initialize()
        {

        }

        public int Update()
        {
            return 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
