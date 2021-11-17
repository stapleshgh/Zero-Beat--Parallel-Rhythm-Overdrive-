using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZBPro
{
    public class Button : GameComponent
    {
        private bool isHovering;
        private bool isClicked;
        private Texture2D texture;
        private MouseState mouse;

        //properties
        public event EventHandler Click;

        public bool Clicked { get; private set; }
        
        public Vector2 Position { get; set; }
        
        public Rectangle Rect { get
            {
                return new Rectangle();
            }
        }

    }
}
