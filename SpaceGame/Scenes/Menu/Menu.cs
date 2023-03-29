using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceGame.Main;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SpaceGame.Scenes.Menu
{
    internal class Menu
    {
        List<Button> buttons = new List<Button>();

        public Menu()
        {
            buttons.Add(new Button(new Vector2(50, 100), "Resume"));
            buttons.Add(new Button(new Vector2(200, 100), "Quit"));
        }

        public int Update()
        {
            return 0;
        }

        public void Draw()
        {
            foreach(Button button in buttons)
            {
                if (GlobalMethods.CheckPointIntersects(button.GetHitBox(), new Vector2 (Mouse.GetState().Position.X, Mouse.GetState().Position.Y)))
                {
                    button.DrawSelectedButton();
                }

                else
                {
                    button.DrawButton();
                }
            }
        }
    }
}
