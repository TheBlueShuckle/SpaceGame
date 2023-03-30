using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace SpaceGame.Scenes.Menu
{
    internal class Button
    {
        const int Width = 128, Height = 64;
        Vector2 pos;
        Rectangle size;
        string text;

        public Button(Vector2 pos, string text)
        {
            this.pos = pos;
            this.text = text;

            size = new Rectangle((int)pos.X, (int)pos.Y, Width, Height);
        }

        public Rectangle GetHitBox()
        {
            return size;
        }

        public void DrawButton()
        {
            GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, size, Color.White);
            GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, new Rectangle((int)pos.X + 4, (int)pos.Y + 4, Width - 8, Height - 8), Color.Black);

            GlobalConstants.SpriteBatch.DrawString(GlobalConstants.GameFont, text, pos, Color.White, 0, new Vector2(0, 0), new Vector2(2, 2), SpriteEffects.None, 0);
        }

        public void DrawSelectedButton()
        {
            GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, size, Color.LightGray);
            GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, new Rectangle((int)pos.X + 4, (int)pos.Y + 4, Width - 8, Height - 8), Color.Gray);

            GlobalConstants.SpriteBatch.DrawString(GlobalConstants.GameFont, text, pos, Color.LightGray, 0, new Vector2(0, 0), new Vector2(2, 2), SpriteEffects.None, 0);
        }
    }
}
