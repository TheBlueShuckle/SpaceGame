using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceGame.Scenes.Menu;
using SpaceGame.Scenes.Planet;
using SpaceGame.Scenes.Space;
using System;
using System.Drawing.Printing;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace SpaceGame.Main
{
    public class Main : Game
    {
        #region Variables

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        int scene = GlobalConstants.InMenu;
        DateTime buttonCooldown = new DateTime(), menuCooldown = new DateTime();
        ScenePlanet scenePlanet;
        SceneSpace sceneSpace;
        Menu menu;

        #endregion

        #region MainMethods

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            GlobalConstants.ScreenWidth = Window.ClientBounds.Width;
            GlobalConstants.ScreenHeight = Window.ClientBounds.Height;

            sceneSpace = new SceneSpace();
            GlobalConstants.LastScene = GlobalConstants.InSpace;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GlobalConstants.SpriteBatch = spriteBatch;

            // TODO: use this.Content to load your game content here
            GlobalConstants.ShipSprites[0] = Content.Load<Texture2D>("Sprites/myShipFrame1");
            GlobalConstants.ShipSprites[1] = Content.Load<Texture2D>("Sprites/myShipFrame2");

            GlobalConstants.PlanetSprites[0] = Content.Load<Texture2D>("Sprites/bigPlanet");
            GlobalConstants.PlanetSprites[1] = Content.Load<Texture2D>("Sprites/smallPlanet");

            GlobalConstants.PlayerSprites[0] = Content.Load<Texture2D>("Sprites/protagonistStandingLeft");
            GlobalConstants.PlayerSprites[1] = Content.Load<Texture2D>("Sprites/protagonistStandingRight");
            GlobalConstants.PlayerSprites[2] = Content.Load<Texture2D>("Sprites/protagonistBackStandingRight");
            GlobalConstants.PlayerSprites[3] = Content.Load<Texture2D>("Sprites/protagonistBackStandingLeft");

            GlobalConstants.EnemySprites[0] = Content.Load<Texture2D>("Sprites/protagonistStandingLeft");
            GlobalConstants.EnemySprites[1] = Content.Load<Texture2D>("Sprites/protagonistStandingRight");
            GlobalConstants.EnemySprites[2] = Content.Load<Texture2D>("Sprites/protagonistBackStandingRight");
            GlobalConstants.EnemySprites[3] = Content.Load<Texture2D>("Sprites/protagonistBackStandingLeft");

            GlobalConstants.Bullet = Content.Load<Texture2D>("Sprites/bullet");
            GlobalConstants.HealthPack = Content.Load<Texture2D>("Sprites/Health-pack prot");

            GlobalConstants.EnemyMeleeRange = new Texture2D(GraphicsDevice, 1, 1);
            GlobalConstants.EnemyMeleeRange.SetData(new Color[] { Color.DarkSlateGray });

            GlobalConstants.HealthBar = new Texture2D(GraphicsDevice, 1, 1);
            GlobalConstants.HealthBar.SetData(new Color[] { Color.White });

            GlobalConstants.Font = Content.Load<SpriteFont>("Print/GameFont");

            menu = new Menu();
            sceneSpace.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            ToggleDebugMode();

            switch (scene)
            {
                case -1:
                    Exit();
                    break;

                case GlobalConstants.InMenu:
                    scene = menu.Update();

                    break;

                case GlobalConstants.InSpace:
                    GlobalConstants.LastScene = GlobalConstants.InSpace;
                    scene = sceneSpace.Update();

                    if (sceneSpace.GetEnteringPlanet())
                    {
                        scenePlanet = new ScenePlanet();
                    }

                    CheckIfOpenMenu();

                    break;

                case GlobalConstants.OnPlanet:
                    GlobalConstants.LastScene = GlobalConstants.OnPlanet;
                    scene = scenePlanet.Update();
                    CheckIfOpenMenu();

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
                case GlobalConstants.InMenu:
                    GraphicsDevice.Clear(Color.Black);
                    break;

                case GlobalConstants.InSpace:
                    GraphicsDevice.Clear(Color.Black);
                    break;

                case GlobalConstants.OnPlanet:
                    GraphicsDevice.Clear(sceneSpace.GetCollidedPlanet().GetColor());
                    break;

                default:
                    break;
            }

            // TODO: Add your drawing code herex

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);


            switch (scene)
            {
                case GlobalConstants.InMenu:
                    menu.Draw();
                    break;

                case GlobalConstants.InSpace:
                    sceneSpace.Draw();
                    break;

                case GlobalConstants.OnPlanet:
                    scenePlanet.Draw();
                    break;

                default:
                    break;
            }

            if (GlobalConstants.CheatMode)
            {
                GlobalConstants.SpriteBatch.DrawString(GlobalConstants.Font, "" + GlobalConstants.LevelsBeaten, new Vector2(0, 0), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion

        #region Methods

        private void ToggleDebugMode()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F3) && buttonCooldown <= DateTime.Now)
            {
                if (GlobalConstants.CheatMode)
                {
                    GlobalConstants.CheatMode = false;
                }

                else
                {
                    GlobalConstants.CheatMode = true;
                }

                buttonCooldown = DateTime.Now.AddMilliseconds(250);
            }
        }

        private void CheckIfOpenMenu()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && menu.GetEscCooldown() <= DateTime.Now)
            {
                scene = GlobalConstants.InMenu;
                menu.AddEscCooldown();
            }
        }

        #endregion
    }
}