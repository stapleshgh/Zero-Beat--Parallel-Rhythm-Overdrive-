using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;
using System;

namespace ZBPro
{
    public class Game1 : Game
    {
        Texture2D texture;
        private GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 position;
        KeyboardState prevState;


        //Constructor, mostly used to make valid objects for use later in the program
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            
        }


        //Long-running initializations that typically last longer
        protected override void Initialize()
        {
            texture = new Texture2D(this.GraphicsDevice, 25, 25);

            base.Initialize();

            position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
            prevState = Keyboard.GetState();
        }


        //Defines what to do while the window is in focus
        protected override void OnActivated(object sender, EventArgs args)
        {
            this.Window.Title = "Active Application";
            base.OnActivated(sender, args);

            //just a goofy lil thing i made to resize the window when its in focus to see how it interacts
            //with the rock
            graphics.PreferredBackBufferWidth = 1900;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();
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

        protected override void UnloadContent()
        {
            Content.Unload();
            base.UnloadContent();

        }



        //Runs at a speed defined by gameTime, which is the time since the last frame call
        protected override void Update(GameTime gameTime)
        {
            //create new keyboardstate object that holds the state of the entire keyboard
            KeyboardState state = Keyboard.GetState();
            


            //if they hit esc, exit the program
            if (IsActive)
            {
                if (state.IsKeyDown(Keys.Escape))
                    Exit();
            }


            System.Text.StringBuilder sb = new StringBuilder();
            foreach (var key in state.GetPressedKeys())
                sb.Append(key);

            if (sb.Length > 0)
                System.Diagnostics.Debug.WriteLine(sb.ToString());


            if (state.IsKeyDown(Keys.D) & !prevState.IsKeyDown(Keys.D))
                position.X += 5;
            if (state.IsKeyDown(Keys.A) & !prevState.IsKeyDown(Keys.A))
                position.X -= 5;
            if (state.IsKeyDown(Keys.W) & !prevState.IsKeyDown(Keys.W))
                position.Y -= 5;
            if (state.IsKeyDown(Keys.S) & !prevState.IsKeyDown(Keys.S))
                position.Y += 5;
            

            base.Update(gameTime);

            prevState = state;
            
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
