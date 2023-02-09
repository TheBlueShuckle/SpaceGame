using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace SpaceGame
{
    public class Main : Game
    {
        #region Variables

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D myShipFrame1, myShipFrame2, bigPlanet, smallPlanet;
        Texture2D[] playerSprites = new Texture2D[4], enemySprites = new Texture2D[4];
        int scene = GlobalConstants.InSpace;
        ScenePlanet scenePlanet;
        SceneSpace sceneSpace;

        #endregion

        #region MainMethods

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            GlobalConstants.ScreenWidth = Window.ClientBounds.Width;
            GlobalConstants.ScreenHeight = Window.ClientBounds.Height;

            sceneSpace = new SceneSpace();
            sceneSpace.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GlobalConstants.SpriteBatch = _spriteBatch;

            // TODO: use this.Content to load your game content here
            myShipFrame1 = Content.Load<Texture2D>("Sprites/myShipFrame1");
            myShipFrame2 = Content.Load<Texture2D>("Sprites/myShipFrame2");

            bigPlanet = Content.Load<Texture2D>("Sprites/bigPlanet");
            smallPlanet = Content.Load<Texture2D>("Sprites/smallPlanet");

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
            
            sceneSpace.SetTextures(myShipFrame1, myShipFrame2, bigPlanet, smallPlanet);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here

            switch (scene)
            {
                case GlobalConstants.InSpace:
                    scene = sceneSpace.Update();

                    if (sceneSpace.GetEnteringPlanet())
                    {
                        scenePlanet = new ScenePlanet();
                    }

                    break;

                case GlobalConstants.OnPlanet:
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
                case GlobalConstants.InSpace:
                    GraphicsDevice.Clear(Color.Black);
                    break;

                case GlobalConstants.OnPlanet:
                    GraphicsDevice.Clear(sceneSpace.GetCollidedPlanet().GetPlanetColor());
                    break;

                default:
                    break;
            }

            // TODO: Add your drawing code herex

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);


            switch (scene)
            {
                case GlobalConstants.InSpace:
                    sceneSpace.Draw();
                    break;

                case GlobalConstants.OnPlanet:
                    scenePlanet.Draw();
                    break;

                default:
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion
    }
}