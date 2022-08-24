﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sudoku.Controls
{
    public class Button 
    {
        #region Fields

        protected MouseState _currentMouse;

        protected SpriteFont _font;

        protected bool _isHovering;

        protected MouseState _previousMouse;

        protected Texture2D _texture;

        protected Color colour;
        protected Color _color;



        #endregion

        #region Properties

        public event EventHandler Click;

        public bool Enabled { get; set; }

        public bool Clicked { get; private set; }

        public Color PenColour { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 PosField { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public string Text { get; set; }

        #endregion

        #region Methods

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;

            _font = font;

            PenColour = Color.Black;
            colour = Color.White;
            Enabled = true;
        }



        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            _color = colour;

            if (_isHovering)
                _color = Color.Gray;

            spriteBatch.Draw(_texture, Rectangle, _color);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour);
            }
        }

        public void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        public void ToggleColor(bool isToggled)
        {
            if (isToggled)
            {
                colour = Color.Gray;
            }
            else
            {
                colour = Color.White;
            }


        }

        #endregion
    }
}
