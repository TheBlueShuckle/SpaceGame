using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace SpaceGame
{
    public class Main : Game
    {
        const bool InSpace = true, OnPlanet = false;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D myShip, bigPlanet, smallPlanet;
        Color planetColor;
        bool isInSpace = InSpace;
        ScenePlanet scenePlanet;
        SceneSpace sceneSpace;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            scenePlanet = new ScenePlanet();
            sceneSpace = new SceneSpace(Window.ClientBounds.Height, Window.ClientBounds.Width);

            if (isInSpace)
            {
                sceneSpace.Initialize();
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            myShip = Content.Load<Texture2D>("Sprites/myShip");
            bigPlanet = Content.Load<Texture2D>("Sprites/bigPlanet");
            smallPlanet = Content.Load<Texture2D>("Sprites/smallPlanet");

            sceneSpace.SetTextures(myShip, bigPlanet, smallPlanet); 
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (isInSpace)
            {
                isInSpace = sceneSpace.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (isInSpace)
            {
                GraphicsDevice.Clear(Color.Black);
            }

            else
            {
                GraphicsDevice.Clear(sceneSpace.GetCollidedPlanetColor());
            }

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (isInSpace)
            {
                foreach (Planet planet in sceneSpace.GetPlanets())
                {
                    planet.UpdatePlanetLocation();
                    _spriteBatch.Draw(planet.GetPlanetSize(), planet.GetPlanetLocation(), planet.GetPlanetColor());
                }

                _spriteBatch.Draw(myShip, sceneSpace.GetMyShipPos(), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}