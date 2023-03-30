using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceGame.Main;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SpaceGame.Scenes.Menu
{
    internal class Menu
    {
        Button resume, quit;
        int scene = GlobalConstants.InMenu;
        DateTime escCooldown = new DateTime();

        public Menu()
        {
            resume = new Button(new Vector2(50, 100), "Resume");
            quit = new Button(new Vector2(200, 100), "Quit");
        }

        public int Update()
        {
            scene = GlobalConstants.InMenu;

            if (resume.CheckIfClicked() || (Keyboard.GetState().IsKeyDown(Keys.Escape) && escCooldown <= DateTime.Now))
            {
                scene = GlobalConstants.LastScene;
                AddEscCooldown();
            }

            if (quit.CheckIfClicked())
            {
                scene = -1;
            }

            return scene;
        }

        public void AddEscCooldown()
        {
            escCooldown = DateTime.Now.AddMilliseconds(250);
        }

        public DateTime GetEscCooldown()
        {
            return escCooldown;
        }

        public void Draw()
        {
            resume.Draw();
            quit.Draw();

            GlobalConstants.SpriteBatch.DrawString(GlobalConstants.GameFont, "" + new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y - 30), new Vector2(0, 0), Color.White);
        }
    }
}
