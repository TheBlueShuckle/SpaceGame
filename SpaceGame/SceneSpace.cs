using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace SpaceGame
{
    internal class SceneSpace
    {
        Texture2D myShip, myShipFrame1, myShipFrame2, bigPlanet, smallPlanet;
        Vector2 myShipPos;
        Vector2 myShipSpeed;
        Random rnd = new Random();
        List<Planet> space = new List<Planet>();
        DateTime nextPlanetTimeStamp, nextShipFrame;
        int randomNumber, windowHeight, windowWidth;
        Rectangle myShipHitBox, planetHitBox;
        Planet collidedPlanet;
        bool enteringPlanet = false;

        public SceneSpace(int windowHeight, int windowWidth)
        {
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
            nextShipFrame = DateTime.Now.AddSeconds(0.5);
        }

        public void SetTextures(Texture2D myShipFrame1, Texture2D myShipFrame2, Texture2D bigPlanet, Texture2D smallPlanet)
        {
            this.myShipFrame1 = myShipFrame1;
            this.myShipFrame2 = myShipFrame2;
            this.bigPlanet = bigPlanet;
            this.smallPlanet = smallPlanet;
            this.myShip = myShipFrame1;
        }

        public void Initialize()
        {
            nextPlanetTimeStamp = DateTime.Now.Add(new TimeSpan(0, 0, 3));

            myShipPos.X = (windowWidth - 45) / 2;
            myShipPos.Y = (windowHeight - 48) / 2;
            myShipSpeed.X = 5f;
            myShipSpeed.Y = 5f;
        }

        public int Update()
        {
            enteringPlanet = false;

            if (DateTime.Now > nextShipFrame)
            {
                myShip = myShip == myShipFrame1 ? myShipFrame2 : myShipFrame1;
                nextShipFrame = DateTime.Now.AddSeconds(0.5);
            }

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
            if ((myShipPos.X >= (windowWidth - myShipFrame1.Width) && Keyboard.GetState().IsKeyDown(Keys.D)) || (myShipPos.X <= 0 && Keyboard.GetState().IsKeyDown(Keys.A)))
            {
                if (myShipPos.X < 0)
                {
                    myShipPos.X = 0;
                }

                if (myShipPos.X >= windowWidth - myShipFrame1.Width)
                {
                    myShipPos.X = windowWidth - myShipFrame1.Width;
                }

                myShipSpeed.X = 0;
            }

            else
            {
                myShipSpeed.X = 2.5f;
            }

            if ((myShipPos.Y >= (windowHeight - 100 - myShipFrame1.Height) && Keyboard.GetState().IsKeyDown(Keys.S)) || (myShipPos.Y <= 100 && Keyboard.GetState().IsKeyDown(Keys.W)))
            {
                if (myShipPos.Y < 100)
                {
                    myShipPos.Y = 100;
                }

                if (myShipPos.Y >= windowHeight - 100 - myShipFrame1.Height)
                {
                    myShipPos.Y = windowHeight - 100 - myShipFrame1.Height;
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

                randomNumber = rnd.Next(5, 20);
                nextPlanetTimeStamp = DateTime.Now.Add(new TimeSpan(0, 0, randomNumber));
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
                myShipHitBox = new Rectangle(Convert.ToInt32(myShipPos.X), Convert.ToInt32(myShipPos.Y), myShipFrame1.Width, myShipFrame1.Height);
                planetHitBox = new Rectangle(Convert.ToInt32(planet.GetPlanetLocation().X + myShipFrame1.Width), Convert.ToInt32(planet.GetPlanetLocation().Y + myShipFrame1.Height), planet.GetPlanetSize().Width - myShipFrame1.Width, planet.GetPlanetSize().Height - myShipFrame1.Height);

                if (myShipHitBox.Intersects(planetHitBox) && Keyboard.GetState().IsKeyDown(Keys.E) && collidedPlanet != planet)
                {
                    enteringPlanet = true;
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

        public bool GetEnteringPlanet()
        {
            return enteringPlanet;
        }
    }
}
