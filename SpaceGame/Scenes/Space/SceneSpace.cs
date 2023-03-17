using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.WIC;
using SpaceGame.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace SpaceGame.Scenes.Space
{
    internal class SceneSpace
    {
        #region Variables

        Ship ship;
        List<Planet> space = new List<Planet>();
        List<Planet> visitedPlanets = new List<Planet>();
        Random rnd = new Random();
        DateTime nextPlanetTimeStamp;
        int randomNumber;
        Rectangle planetHitbox;
        bool enteringPlanet = false;

        #endregion

        #region MainMethods

        public void LoadContent()
        {
            ship = new Ship();
        }

        public int Update()
        {
            enteringPlanet = false;

            ship.Move();
            ship.CheckBounds();
            ship.Animate();

            SpawnPlanet();
            DeletePlanet();
            CheckIfPlanetDespawned();

            return CheckPlanetCollision();
        }

        public void Draw()
        {
            foreach (Planet planet in space)
            {
                planet.UpdatePos();
                GlobalConstants.SpriteBatch.Draw(planet.GetSize(), planet.GetPos(), planet.GetColor());
            }

            ship.Draw();
        }

        #endregion

        #region Methods


        private void SpawnPlanet()
        {
            if (nextPlanetTimeStamp < DateTime.Now && space.Count < 5)
            {
                space.Add(new Planet());

                randomNumber = rnd.Next(GlobalConstants.PlanetWaitSecondsMin, GlobalConstants.PlanetWaitSecondsMax);
                nextPlanetTimeStamp = DateTime.Now.Add(new TimeSpan(0, 0, randomNumber));
            }
        }

        private void DeletePlanet()
        {
            foreach (Planet planet in space.ToList())
            {
                if (planet.GetPos().Y >= GlobalConstants.ScreenHeight)
                {
                    space.Remove(planet);
                }
            }
        }

        private int CheckPlanetCollision()
        {
            foreach (Planet planet in space.ToList())
            {
                planetHitbox = new Rectangle(
                    (int) (planet.GetPos().X + ship.GetCurrentSprite().Width), 
                    (int) (planet.GetPos().Y + ship.GetCurrentSprite().Height), 
                    planet.GetSize().Width - 2 * ship.GetCurrentSprite().Width, 
                    planet.GetSize().Height - 2 * ship.GetCurrentSprite().Height
                    );

                if (ship.GetHitbox().Intersects(planetHitbox) && Keyboard.GetState().IsKeyDown(Keys.E) && visitedPlanets.Contains(planet) == false)
                {
                    enteringPlanet = true;
                    visitedPlanets.Add(planet);
                    return GlobalConstants.OnPlanet;
                }
            }

            return GlobalConstants.InSpace;
        }

        private void CheckIfPlanetDespawned()
        {
            foreach (Planet planet in visitedPlanets.ToList())
            {
                if (space.Contains(planet) == false)
                {
                    visitedPlanets.Remove(planet);
                }
            }
        }

        public Planet GetCollidedPlanet()
        {
            return visitedPlanets.Last();
        }

        public bool GetEnteringPlanet()
        {
            return enteringPlanet;
        }

        #endregion
    }
}
