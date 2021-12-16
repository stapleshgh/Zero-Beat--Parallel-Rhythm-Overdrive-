using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;
using ZBPro.Content;

namespace ZBPro.States
{
    public class GameState : State
    {
        #region Fields
        private Texture2D pausedTexture;
        private Texture2D field;
        private Texture2D bullet;
        private Texture2D playerTexture;
        private Texture2D bgTexture; 
        public Player _player;
        private KeyboardState state;
        private KeyboardState prevState;
        private bool isPaused;
        private Image _bg;
        public Image paused;
        #endregion

        private List<Component> _components;


        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            isPaused = false;
            playerTexture = content.Load<Texture2D>("Sprites/player");
            bgTexture = content.Load<Texture2D>("Sprites/sky");
            pausedTexture = content.Load<Texture2D>("Sprites/paused_text");
            Player _player = new Player(playerTexture, 500, new Vector2(0, 0));
            paused = new Image(pausedTexture, new Vector2(0, 0), 0.75f);
            

            _components = new List<Component>()
            {
                _player
                
            };
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            

            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
            

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            state = Keyboard.GetState();

            if (isPaused == false)
            {
                if (state.IsKeyDown(Keys.Escape))
                    _components.Add(paused);
                    isPaused = true;
            }

            if (isPaused == true)
                if (state.IsKeyDown(Keys.Escape))
                    _components.Remove(paused);
                    isPaused = false;
                    

            if (isPaused == true)
                MediaPlayer.Pause();

            foreach (var component in _components)
                component.Update(gameTime);

            prevState = state;
        }
    }
}
