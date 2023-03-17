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

        Texture2D size;
        Color planetColor;
        Vector2 location;
        Vector2 planetSpeed;
        Random rnd = new Random();

        #endregion

        #region Methods

        public Planet()
        {
            GenerateColor();
            GenerateSize();
            GeneratePos();
            GenerateSpeed();
        }

        private void GenerateSize()
        {
            int randomSize = rnd.Next(0, GlobalConstants.PlanetSprites.Length);

            size = GlobalConstants.PlanetSprites[randomSize];
        }

        private void GenerateColor()
        {
            Color[] colors = new Color[9] { Color.SandyBrown, Color.Aquamarine, Color.CornflowerBlue, Color.Crimson, Color.Beige, Color.Green, Color.Coral, Color.GreenYellow, Color.RoyalBlue };
            int randomColor = rnd.Next(0, colors.Length);

            planetColor = colors[randomColor];
        }

        private void GeneratePos()
        {
            location.Y = -(rnd.Next(size.Height, GlobalConstants.ScreenHeight));
            location.X = rnd.Next(0 - GetSize().Width / 2, GlobalConstants.ScreenWidth - GetSize().Width / 2);
        }

        public void UpdatePos()
        {
            location += planetSpeed;
        }

        private void GenerateSpeed()
        {
            Random rnd = new Random();
            int randomY = rnd.Next(GlobalConstants.PlanetMinSpeed, GlobalConstants.PlanetMaxSpeed);

            planetSpeed.Y = randomY / 10;
        }

        public Texture2D GetSize()
        {
            return size;
        }

        public Color GetColor()
        {
            return planetColor;
        }

        public Vector2 GetPos()
        {
            return location;
        }

        #endregion
    }
}
