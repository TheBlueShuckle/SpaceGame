using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D myShip, planet;
        Vector2 myShipPos, planetPos;
        Vector2 myShipSpeed;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            myShipPos.X = 100;
            myShipPos.Y = 100;
            myShipSpeed.X = 2.5f;
            myShipSpeed.Y = 2.5f;

            planetPos.X = 200;
            planetPos.Y = 200;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            myShip = Content.Load<Texture2D>("Sprites/myShip");
            planet = Content.Load<Texture2D>("Sprites/planet");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            CheckMove();
            CheckBoundsX();
            CheckBoundsY();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(myShip, myShipPos, Color.White);
            _spriteBatch.Draw(planet, planetPos, Color.Red);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void CheckMove()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                myShipPos.X = myShipPos.X - myShipSpeed.X;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                myShipPos.X = myShipPos.X + myShipSpeed.X;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                myShipPos.Y = myShipPos.Y - myShipSpeed.Y;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                myShipPos.Y = myShipPos.Y + myShipSpeed.Y;
            }
        }

        public void CheckBoundsX()
        {
            if ((myShipPos.X >= (Window.ClientBounds.Width - myShip.Width) && Keyboard.GetState().IsKeyDown(Keys.D)) || (myShipPos.X <= 0 && Keyboard.GetState().IsKeyDown(Keys.A)))
            {
                if (myShipPos.X < 0)
                {
                    myShipPos.X = 0;
                }

                if (myShipPos.X >= Window.ClientBounds.Width - myShip.Width)
                {
                    myShipPos.X = Window.ClientBounds.Width - myShip.Width;
                }

                myShipSpeed.X = 0;
            }

            else
            {
                myShipSpeed.X = 2.5f;
            }
        }

        public void CheckBoundsY()
        {
            if ((myShipPos.Y >= (Window.ClientBounds.Height - myShip.Height) && Keyboard.GetState().IsKeyDown(Keys.S)) || (myShipPos.Y <= 0 && Keyboard.GetState().IsKeyDown(Keys.W)))
            {
                if (myShipPos.Y < 0)
                {
                    myShipPos.Y = 0;
                }

                if (myShipPos.Y >= Window.ClientBounds.Height - myShip.Height)
                {
                    myShipPos.Y = Window.ClientBounds.Height - myShip.Height;
                }

                myShipSpeed.Y = 0;
            }
            else
            {
                myShipSpeed.Y = 2.5f;
            }
        }
    }
}