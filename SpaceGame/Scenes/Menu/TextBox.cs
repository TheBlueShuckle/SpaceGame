using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame.Scenes.Menu
{
    internal class TextBox
    {
        string text;
        Point center;
        Vector2 pos;

        public TextBox(string text, Point centerPoint)
        {
            this.text = text;
            center = centerPoint;

            GetTruePositionOfString();
        }

        public void Draw()
        {
            GlobalConstants.SpriteBatch.DrawString(GlobalConstants.Font, text, pos, Color.LightGray, 0, new Vector2(0, 0), new Vector2(2, 2), SpriteEffects.None, 0);
        }

        private void GetTruePositionOfString()
        {
            Vector2 textSize;

            textSize = GlobalConstants.Font.MeasureString(text) * 2;
            pos = new Vector2(center.X - (textSize.X / 2), center.Y - (textSize.Y / 2));
        }
    }
}
