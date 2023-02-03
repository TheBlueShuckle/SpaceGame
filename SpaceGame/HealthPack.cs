using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection.Metadata;

namespace SpaceGame
{
    internal class HealthPack
    {
        Vector2 pos;
        int healthPackValue;

        public HealthPack(Vector2 centerPos)
        {
            pos = new Vector2(centerPos.X - (GlobalConstants.HealthPack.Width / 2), centerPos.Y - (GlobalConstants.HealthPack.Height / 2));
            GenerateValue();
        }

        private void GenerateValue()
        {
            Random rand = new Random();

            healthPackValue = rand.Next(1, 10) * 10;
        }

        public int GetValue()
        {
            return healthPackValue;
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, 32, 16);
        }

        public void Draw()
        {
            GlobalConstants.SpriteBatch.Draw(
                GlobalConstants.HealthPack,
                GetRectangle(),
                new Rectangle(0, 0, GlobalConstants.HealthPack.Width, GlobalConstants.HealthPack.Height),
                Color.White);
        }
    }
}
