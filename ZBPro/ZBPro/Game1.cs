using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ZBPro
{
    public class Game1 : Game
    {
        Texture2D texture;
        private GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 position;


        //Constructor, mostly used to make valid objects for use later in the program
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            position = new Vector2(0, 0);
        }


        //Long-running initializations that typically last longer
        protected override void Initialize()
        {
            texture = new Texture2D(this.GraphicsDevice, 100, 100);
            Color[] colorData = new Color[100 * 100];
            for (int i = 0; i < 10000; i ++)
            {
                colorData[i] = Color.White;
            }

            texture.SetData<Color>(colorData);


            base.Initialize();
        }


        //Defines what to do while the window is in focus
        protected override void OnActivated(object sender, EventArgs args)
        {
            this.Window.Title = "Active Application";
            base.OnActivated(sender, args);
        }


        //Defines what to do if the window is clicked off of
        protected override void OnDeactivated(object sender, EventArgs args)
        {
            this.Window.Title = "Inactive Application";
            base.OnDeactivated(sender, args);
        }


        //Loads all game content, like assets and music and such
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Content.Load<Texture2D>("background");

            
        }


        //Runs at a speed defined by gameTime, which is the time since the last frame call
        protected override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
            }
            position.X += 200.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (position.X > this.GraphicsDevice.Viewport.Width)
                position.X = 0;

            base.Update(gameTime);
            
        }


        //Updates the screen and the objects on the screen
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();

            

            base.Draw(gameTime);
        }
    }
}
