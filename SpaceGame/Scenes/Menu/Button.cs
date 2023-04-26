using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using SpaceGame.Main;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace SpaceGame.Scenes.Menu
{
    internal class Button
    {
        const int Width = 100 * PixelSize, Height = 24 * PixelSize, PixelSize = 4;
        Vector2 center, pos, textPos;
        Rectangle size;
        string text;

        public Button(Vector2 center, string text)
        {
            this.center = center;
            this.text = text;

            GetTruePosition();
            GetTruePositionOfString();
            size = new Rectangle((int)pos.X, (int)pos.Y, Width, Height);;
        }

        public bool CheckIfClicked()
        {
            if(GlobalMethods.CheckPointIntersects(size, new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y - 30)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                return true;
            }

            return false;
        }

        public void Draw()
        {
            if (GlobalMethods.CheckPointIntersects(size, new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y - 30)))
            {
                DrawSelectedButton();
            }

            else
            {
                DrawUnselectedButton();
            }
        }

        private void DrawUnselectedButton()
        {
            GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, size, Color.White);
            GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, new Rectangle((int)pos.X + PixelSize, (int)pos.Y + PixelSize, Width - 2 * PixelSize, Height - 2 * PixelSize), Color.Black);

            GlobalConstants.SpriteBatch.DrawString(GlobalConstants.Font, text, textPos, Color.White, 0, new Vector2(0, 0), new Vector2(2, 2), SpriteEffects.None, 0);
        }

        private void DrawSelectedButton()
        {
            GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, size, Color.LightGray);
            GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, new Rectangle((int)pos.X + PixelSize, (int)pos.Y + PixelSize, Width - 2 * PixelSize, Height - 2 * PixelSize), Color.Gray);

            GlobalConstants.SpriteBatch.DrawString(GlobalConstants.Font, text, textPos, Color.LightGray, 0, new Vector2(0, 0), new Vector2(2, 2), SpriteEffects.None, 0);
        }

        private void GetTruePosition()
        {
            pos = new Vector2(center.X - Width / 2, center.Y - Height / 2);
        }

        private void GetTruePositionOfString()
        {
            Vector2 textSize;

            textSize = GlobalConstants.Font.MeasureString(text) * 2;
            textPos = new Vector2(center.X - (textSize.X / 2), center.Y - (textSize.Y / 2));
        }
    }
}
