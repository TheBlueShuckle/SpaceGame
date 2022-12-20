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
        Texture2D myShipFrame1, myShipFrame2, bigPlanet, smallPlanet, bullet;
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

            playerSprites[0] = Content.Load<Texture2D>("Sprites/protagonistStandingLeft");
            playerSprites[1] = Content.Load<Texture2D>("Sprites/protagonistStandingRight");
            playerSprites[2] = Content.Load<Texture2D>("Sprites/protagonistBackStandingRight");
            playerSprites[3] = Content.Load<Texture2D>("Sprites/protagonistBackStandingLeft");

            enemySprites[0] = Content.Load<Texture2D>("Sprites/protagonistStandingLeft");
            enemySprites[1] = Content.Load<Texture2D>("Sprites/protagonistStandingRight");
            enemySprites[2] = Content.Load<Texture2D>("Sprites/protagonistBackStandingRight");
            enemySprites[3] = Content.Load<Texture2D>("Sprites/protagonistBackStandingLeft");

            bullet = Content.Load<Texture2D>("Sprites/bullet");

            Texture2D enemyMeleeRange;

            enemyMeleeRange = new Texture2D(GraphicsDevice, 1, 1);
            enemyMeleeRange.SetData(new Color[] { Color.DarkSlateGray });

            GlobalConstants.PlayerSprites = playerSprites;
            GlobalConstants.EnemySprites = enemySprites;
            GlobalConstants.Bullet = bullet;
            GlobalConstants.EnemyMeleeRange = enemyMeleeRange;

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

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

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