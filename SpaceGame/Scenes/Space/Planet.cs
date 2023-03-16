using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using SpaceGame.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace SpaceGame.Scenes.Space
{
    public class Planet
    {
        #region Variables

        Texture2D bigPlanet, smallPlanet;
        Texture2D size;
        Color planetColor;
        Vector2 location;
        Vector2 planetSpeed;

        #endregion

        #region Methods

        public Planet(Texture2D bigPlanet, Texture2D smallPlanet)
        {
            this.bigPlanet = bigPlanet;
            this.smallPlanet = smallPlanet;

            GeneratePlanetColor();
            GeneratePlanetSize();
            GeneratePlanetLocation();
            GeneratePlanetSpeed();
        }

        private void GeneratePlanetSize()
        {
            Texture2D[] planetSizes = new Texture2D[2] { smallPlanet, bigPlanet };
            Random rnd = new Random();
            int randomSize = rnd.Next(0, planetSizes.Length);

            size = planetSizes[randomSize];
        }

        private void GeneratePlanetColor()
        {
            Color[] colors = new Color[9] { Color.SandyBrown, Color.Aquamarine, Color.CornflowerBlue, Color.Crimson, Color.Beige, Color.Green, Color.Coral, Color.GreenYellow, Color.RoyalBlue };
            Random rnd = new Random();
            int randomColor = rnd.Next(0, colors.Length);

            planetColor = colors[randomColor];
        }

        private void GeneratePlanetLocation()
        {
            Random rnd = new Random();
            int randomHeight = rnd.Next(size.Height, GlobalConstants.ScreenHeight);
            int randomWidth = rnd.Next(0 - GetPlanetSize().Width / 2, GlobalConstants.ScreenWidth - GetPlanetSize().Width / 2);

            location.Y = -randomHeight;
            location.X = randomWidth;
        }

        public void UpdatePlanetLocation()
        {
            location += planetSpeed;
        }

        private void GeneratePlanetSpeed()
        {
            Random rnd = new Random();
            int randomY = rnd.Next(GlobalConstants.PlanetMinSpeed, GlobalConstants.PlanetMaxSpeed);

            planetSpeed.Y = randomY / 10;
        }

        public Texture2D GetPlanetSize()
        {
            return size;
        }

        public Color GetPlanetColor()
        {
            return planetColor;
        }

        public Vector2 GetPlanetLocation()
        {
            return location;
        }

        #endregion
    }
}
