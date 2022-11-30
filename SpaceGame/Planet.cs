using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    internal class Planet
    {
        Texture2D bigPlanet, smallPlanet;

        public Planet(Texture2D bigPlanet, Texture2D smallPlanet)
        {
            this.bigPlanet = bigPlanet;
            this.smallPlanet = smallPlanet;
        }

        public Texture2D GenerateSize()
        {
            Texture2D[] planetSizes = new Texture2D[2] { smallPlanet, bigPlanet };
            Random rnd = new Random();
            int randomSize = rnd.Next(1, planetSizes.Length);

            return planetSizes[randomSize];
        }

        public Color PlanetColor()
        {
            Color[] colors = new Color[9] { Color.SandyBrown, Color.Aquamarine, Color.CornflowerBlue, Color.Crimson, Color.Beige, Color.BlueViolet, Color.Coral, Color.GreenYellow, Color.RoyalBlue };
            Random rnd = new Random();
            int randomColor = rnd.Next(1, colors.Length);

            return colors[randomColor];
        }
    }
}
