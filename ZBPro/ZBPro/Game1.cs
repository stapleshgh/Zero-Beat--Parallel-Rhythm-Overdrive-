using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;
using System;
using System.Linq;
using System.Collections.Generic;
using ZBPro.States;
using Microsoft.Xna.Framework.Media;
using ZBPro.Content;


namespace ZBPro
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private List<Component> _gameComponents;

        private State _currentState;

        private State _nextState;

        public void ChangeState(State state)
        {
            _nextState = state;
        }




        //Constructor, mostly used to make valid objects for use later in the program
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = false;
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            Window.Title = "Zero Beat";
            
        }


        //Long-running initializations that typically last longer
        protected override void Initialize()
        {
            IsMouseVisible = true;
            _gameComponents = new List<Component>();

            base.Initialize();

            
        }


        //Loads all game content, like assets and music and such
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState = new MenuState(Content, this, graphics.GraphicsDevice);

        }


        protected override void UnloadContent()
        {
            Content.Unload();
            base.UnloadContent();

        }



        //Runs at a speed defined by gameTime, which is the time since the last frame call
        protected override void Update(GameTime gameTime)
        {
                    

            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);
        }


        //Updates the screen and the objects on the screen
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentState.Draw(gameTime, spriteBatch);


            base.Draw(gameTime);

        }
    }
}
