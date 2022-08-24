using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sudoku.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Content
{
    class Menu
    {
        protected List<Button> buttons = new List<Button>();
        private Texture2D texture;

        public Menu(Texture2D _texture)
        {
            texture = _texture;
        }

        public void Update(GameTime gameTime)
        {
            foreach (Button temp in buttons)
            {
                temp.Update(gameTime);
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch, Rectangle bounds)
        {
            _spriteBatch.Draw(texture, bounds, Color.White);
         
            foreach (Button temp in buttons)
            {
                if (temp.Enabled)
                    temp.Draw(gameTime, _spriteBatch);
            }
        }

    }
}
