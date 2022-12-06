using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using SharpDX.XAudio2;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace SpaceGame
{
    public class Main : Game
    {
        const int InSpace = 1, OnPlanet = 2;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D myShipFrame1, myShipFrame2, bigPlanet, smallPlanet;
        int scene = 1;
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

            sceneSpace = new SceneSpace(Window.ClientBounds.Height, Window.ClientBounds.Width);

            switch (scene)
            {
                case InSpace:
                    sceneSpace.Initialize();
                    break;

                case OnPlanet:
                    scenePlanet.Initialize();
                    break;

                default:
                    break;
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            myShipFrame1 = Content.Load<Texture2D>("Sprites/myShipFrame1");
            myShipFrame2 = Content.Load<Texture2D>("Sprites/myShipFrame2");
            bigPlanet = Content.Load<Texture2D>("Sprites/bigPlanet");
            smallPlanet = Content.Load<Texture2D>("Sprites/smallPlanet");

            sceneSpace.SetTextures(myShipFrame1, myShipFrame2, bigPlanet, smallPlanet); 
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            switch (scene)
            {
                case InSpace:
                    scene = sceneSpace.Update();

                    if (sceneSpace.GetEnteringPlanet())
                    {
                        scenePlanet = new ScenePlanet(Window.ClientBounds.Height, Window.ClientBounds.Width);
                    }

                    break;

                case OnPlanet:
                    scene = scenePlanet.Update();
                    break;

                default:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (scene)
            {
                case InSpace:
                    GraphicsDevice.Clear(Color.Black);
                    break;

                case OnPlanet:
                    GraphicsDevice.Clear(sceneSpace.GetCollidedPlanet().GetPlanetColor());
                    break;

                default :
                    break;
            }

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            switch (scene)
            {
                case InSpace:
                    sceneSpace.Draw(_spriteBatch);
                    break;

                case OnPlanet:
                    scenePlanet.Draw(_spriteBatch);
                    break;

                default:
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}