using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace SpaceGame
{
    internal class SceneSpace
    {
        Texture2D myShip, bigPlanet, smallPlanet;
        Vector2 myShipPos;
        Vector2 myShipSpeed;
        Random rnd = new Random();
        List<Planet> space = new List<Planet>();
        DateTime nextPlanetTimeStamp;
        int randomNumber, windowHeight, windowWidth;
        Rectangle myShipHitBox, planetHitBox;
        Planet collidedPlanet;

        public SceneSpace(int windowHeight, int windowWidth)
        {
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
        }

        public void SetTextures(Texture2D myShip, Texture2D bigPlanet, Texture2D smallPlanet)
        {
            this.myShip = myShip;
            this.bigPlanet = bigPlanet;
            this.smallPlanet = smallPlanet;
        }

        public void Initialize()
        {
            nextPlanetTimeStamp = DateTime.Now;
            nextPlanetTimeStamp = nextPlanetTimeStamp.Add(new TimeSpan(0, 0, 5));

            myShipPos.X = (windowWidth - 45) / 2;
            myShipPos.Y = (windowHeight - 48) / 2;
            myShipSpeed.X = 2.5f;
            myShipSpeed.Y = 2.5f;
        }

        public int Update()
        {
            SpawnPlanet();
            DeletePlanet();

            CheckMove();
            CheckBounds();

            return CheckPlanetCollision();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Planet planet in space)
            {
                planet.UpdatePlanetLocation();
                spriteBatch.Draw(planet.GetPlanetSize(), planet.GetPlanetLocation(), planet.GetPlanetColor());
            }

            spriteBatch.Draw(myShip, myShipPos, Color.White);
        }

        public void CheckMove()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                myShipPos.X -= myShipSpeed.X;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                myShipPos.X += myShipSpeed.X;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                myShipPos.Y -= myShipSpeed.Y;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                myShipPos.Y += myShipSpeed.Y;
            }
        }

        public void CheckBounds()
        {
            if ((myShipPos.X >= (windowWidth - myShip.Width) && Keyboard.GetState().IsKeyDown(Keys.D)) || (myShipPos.X <= 0 && Keyboard.GetState().IsKeyDown(Keys.A)))
            {
                if (myShipPos.X < 0)
                {
                    myShipPos.X = 0;
                }

                if (myShipPos.X >= windowWidth - myShip.Width)
                {
                    myShipPos.X = windowWidth - myShip.Width;
                }

                myShipSpeed.X = 0;
            }

            else
            {
                myShipSpeed.X = 2.5f;
            }

            if ((myShipPos.Y >= (windowHeight - 100 - myShip.Height) && Keyboard.GetState().IsKeyDown(Keys.S)) || (myShipPos.Y <= 100 && Keyboard.GetState().IsKeyDown(Keys.W)))
            {
                if (myShipPos.Y < 100)
                {
                    myShipPos.Y = 100;
                }

                if (myShipPos.Y >= windowHeight - 100 - myShip.Height)
                {
                    myShipPos.Y = windowHeight - 100 - myShip.Height;
                }

                myShipSpeed.Y = 0;
            }
            else
            {
                myShipSpeed.Y = 2.5f;
            }
        }

        private void SpawnPlanet()
        {
            if (nextPlanetTimeStamp < DateTime.Now && space.Count < 5)
            {
                space.Add(new Planet(bigPlanet, smallPlanet, windowHeight, windowWidth));

                randomNumber = rnd.Next(5, 30);
                nextPlanetTimeStamp = DateTime.Now;
                nextPlanetTimeStamp = nextPlanetTimeStamp.Add(new TimeSpan(0, 0, randomNumber));
            }
        }

        private void DeletePlanet()
        {
            foreach (Planet planet in space.ToList())
            {
                if (planet.GetPlanetLocation().Y == windowHeight)
                {
                    space.Remove(planet);
                }
            }
        }

        private int CheckPlanetCollision()
        {
            foreach (Planet planet in space.ToList())
            {
                myShipHitBox = new Rectangle(Convert.ToInt32(myShipPos.X), Convert.ToInt32(myShipPos.Y), myShip.Width, myShip.Height);
                planetHitBox = new Rectangle(Convert.ToInt32(planet.GetPlanetLocation().X + myShip.Width), Convert.ToInt32(planet.GetPlanetLocation().Y + myShip.Height), planet.GetPlanetSize().Width - myShip.Width, planet.GetPlanetSize().Height - myShip.Height);

                if ((myShipHitBox.Intersects(planetHitBox) && Keyboard.GetState().IsKeyDown(Keys.E)) && collidedPlanet != planet)
                {
                    collidedPlanet = planet;
                    return 2;
                }
            }

            return 1;
        }

        public Planet GetCollidedPlanet()
        {
            return collidedPlanet;
        }
    }
}
