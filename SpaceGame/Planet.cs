using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    internal class Planet
    {
        Texture2D bigPlanet, smallPlanet;
        Texture2D size;
        Color planetColor;
        Vector2 location;

        public Planet(Texture2D bigPlanet, Texture2D smallPlanet)
        {
            this.bigPlanet = bigPlanet;
            this.smallPlanet = smallPlanet;

            GenerateColor();
            GenerateLocation(600, 800);
            GenerateSize();
        }

        public void GenerateSize()
        {
            Texture2D[] planetSizes = new Texture2D[2] { smallPlanet, bigPlanet };
            Random rnd = new Random();
            int randomSize = rnd.Next(1, planetSizes.Length);

            size = planetSizes[randomSize];
        }

        public void GenerateColor()
        {
            Color[] colors = new Color[9] { Color.SandyBrown, Color.Aquamarine, Color.CornflowerBlue, Color.Crimson, Color.Beige, Color.Green, Color.Coral, Color.GreenYellow, Color.RoyalBlue };
            Random rnd = new Random();
            int randomColor = rnd.Next(1, colors.Length);

            planetColor = colors[randomColor];
        }

        public void GenerateLocation(int height, int width)
        {
            Random rnd = new Random();
            int randomHeight = rnd.Next(0, height), randomWidth = rnd.Next(0, width);

            location.Y = randomHeight;
            location.X = randomWidth;
        }
    }
}
