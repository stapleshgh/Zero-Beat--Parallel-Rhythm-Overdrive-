using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZBPro.Content;

namespace ZBPro.States
{
    public class GameState : State
    {
        private List<Component> _components;

        #region Properties
        private Image _bg;
        #endregion


        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Image bg) : base(game, graphicsDevice, content)
        {
            _bg = bg;

            _components = new List<Component>()
            { _bg
            };
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
