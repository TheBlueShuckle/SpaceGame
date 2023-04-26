using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation.DirectX;
using System;
using System.Windows.Forms;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace SpaceGame.Scenes.Menu
{
    internal class Menu
    {
        const int Height = 96, MainMenu = 0, HelpMenu = 1;
        Button resume, quit, help, back;
        TextBox text;
        int scene = GlobalConstants.InMenu, page = MainMenu;
        DateTime escCooldown = new DateTime();

        public Menu()
        {
            resume = new Button(new Vector2(GlobalConstants.ScreenWidth / 2, GlobalConstants.ScreenHeight / 3), "Resume");
            help = new Button(new Vector2(GlobalConstants.ScreenWidth / 2, GlobalConstants.ScreenHeight / 3 + Height + 12), "Help");
            quit = new Button(new Vector2(GlobalConstants.ScreenWidth / 2, (GlobalConstants.ScreenHeight / 3 + (2 * Height) + 24)), "Quit");
        }

        public int Update()
        {
            scene = GlobalConstants.InMenu;

            if (page == MainMenu)
            {
                if (resume.CheckIfClicked() || (Keyboard.GetState().IsKeyDown(Keys.Escape) && escCooldown <= DateTime.Now))
                {
                    scene = GlobalConstants.LastScene;
                    AddEscCooldown();
                }

                if (help.CheckIfClicked())
                {
                    page = HelpMenu;
                }

                if (quit.CheckIfClicked())
                {
                    scene = -1;
                }
            }

            if (page == HelpMenu)
            {
                text = new TextBox(
                    "Controls:\n" +
                    "W - Up\n" +
                    "S - Down\n" +
                    "A - Left\n" +
                    "D - Right\n" +
                    "E - Land on planet\n" +
                    "Left Click - Shoot\n" +
                    "\n" +
                    "F3 - Cheat Mode \n", 
                    new Point(GlobalConstants.ScreenWidth / 2, GlobalConstants.ScreenHeight / 2));

                back = new Button(new Vector2(GlobalConstants.ScreenWidth / 2, (GlobalConstants.ScreenHeight / 3) * 2 + 12), "Back");

                if (back.CheckIfClicked() || Keyboard.GetState().IsKeyDown(Keys.Escape) && escCooldown <= DateTime.Now)
                {
                    page = MainMenu;
                    AddEscCooldown();
                }
            }

            return scene;
        }

        public void Draw()
        {
            if (page == MainMenu)
            {
                resume.Draw();
                help.Draw();
                quit.Draw();
            }

            if (page == HelpMenu)
            {
                text.Draw();
                back.Draw();
            }

            GlobalConstants.SpriteBatch.DrawString(GlobalConstants.Font, "" + new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y - 30), new Vector2(0, 0), Color.White);
        }

        public void AddEscCooldown()
        {
            escCooldown = DateTime.Now.AddMilliseconds(250);
        }

        public DateTime GetEscCooldown()
        {
            return escCooldown;
        }
    }
}
