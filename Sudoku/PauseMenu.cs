using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sudoku.Content;
using Sudoku.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    class PauseMenu : Menu
    {
        public PauseMenu(Texture2D _texture, SpriteFont font, Texture2D texture) : base(_texture)
        {
            Button _return = new Button(texture, font)
            {
                Position = new Vector2(348, 270)
            };
            _return.Text = "Return";
            _return.Click += PressedReturn;

            Button _save = new Button(texture, font)
            {
                Position = new Vector2(348, 330)
            };
            _save.Text = "Save";
            _save.Click += PressedSave;
            Button _exit = new Button(texture, font)
            {
                Position = new Vector2(348, 390)
            };
            _exit.Text = "Exit";
            _exit.Click += PressedExit;

            buttons.Add(_return);
            buttons.Add(_save);
            buttons.Add(_exit);

        }
        public void PressedReturn(object sender, EventArgs e)
        {
            Game1.state = Game1.GameState.Gameplay;
        }
        public void PressedSave(object sender, EventArgs e)
        {
            Game1.isUnfinishedLevel  = 1;
            Game1.state = Game1.GameState.Save;
        }
        public void PressedExit(object sender, EventArgs e)
        {
            
            Game1.state = Game1.GameState.MenuMain;
        }
    }
}
