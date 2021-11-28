using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;
using System;
using static ZBPro.LevelManager;
using System.Linq;
using System.Collections.Generic;

namespace ZBPro
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 position;
        KeyboardState prevState;
        private Color _backgroundColour;
        private List<Component> _gameComponents;




        //Constructor, mostly used to make valid objects for use later in the program
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = false;
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            
        }


        //Long-running initializations that typically last longer
        protected override void Initialize()
        {
            IsMouseVisible = true;
            _gameComponents = new List<Component>();

            base.Initialize();

            position = new Vector2(15, 0);
            prevState = Keyboard.GetState();
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



            var startButton = new Button(Content.Load<Texture2D>("Sprites/button"), Content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(250, graphics.PreferredBackBufferHeight / 2),
                Text = "Start",
            };


            startButton.Click += startButton_Click;

            var quitButton = new Button(Content.Load<Texture2D>("Sprites/button"), Content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(500, (graphics.PreferredBackBufferHeight / 2)),
                Text = "Quit",
            };

            quitButton.Click += quitButton_Click;

            _gameComponents = new List<Component>()
            {
                startButton,
                quitButton,
            };


        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            if (IsActive)
                Exit();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
            


            if (state.IsKeyDown(Keys.D) & !prevState.IsKeyDown(Keys.D))
                position.X += 50;
            if (state.IsKeyDown(Keys.A) & !prevState.IsKeyDown(Keys.A))
                position.X -= 50;
            if (state.IsKeyDown(Keys.W) & !prevState.IsKeyDown(Keys.W))
                position.Y -= 50;
            if (state.IsKeyDown(Keys.S) & !prevState.IsKeyDown(Keys.S))
                position.Y += 50;

            foreach (var component in _gameComponents)
                component.Update(gameTime);
            

            base.Update(gameTime);

            prevState = state;
            
        }


        //Updates the screen and the objects on the screen
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin();
            foreach (var component in _gameComponents)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            

            base.Draw(gameTime);
        }
    }
}
