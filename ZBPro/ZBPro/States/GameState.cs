using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using ZBPro.Content;

namespace ZBPro.States
{
    public class GameState : State
    {
        private Texture2D field;
        private Texture2D bullet;
        private Texture2D playerTexture;
        public Player _player;

        private List<Component> _components;

        #region Properties
        private Image _bg;
        #endregion


        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            playerTexture = content.Load<Texture2D>("Sprites/player");
            Player _player = new Player(playerTexture, 500, new Vector2(0, 0));

            

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
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
