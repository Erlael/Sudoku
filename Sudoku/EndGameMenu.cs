using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sudoku.Content;
using Sudoku.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sudoku
{
    class EndGameMenu : Menu
    {
        public EndGameMenu(Texture2D _texture, SpriteFont font, Texture2D texture) : base(_texture)
        {
            Button _reload = new Button(texture, font)
            {
                Position = new Vector2(360, 350)
            };
            _reload.Text = "Reload";
            _reload.Click += PressedReload;
            Button _exit = new Button(texture, font)
            {
                Position = new Vector2(360, 410)
            };
            _exit.Text = "Exit";
            _exit.Click += PressedExit;
            buttons.Add(_reload);
            buttons.Add(_exit);
        }
        public void PressedReload(object sender, EventArgs e)
        {
            Game1.state = Game1.GameState.Reload;
        }
        public void PressedExit(object sender, EventArgs e)
        {
            ForSave s_ = new ForSave();
            s_.currentComplexity = Game1.currentComplexity;
            s_.currentLevel = Game1.currentLevel;
            s_.isUnfinishedLevel  = 0;
            FileStream fs = File.Create("load.json");
            fs.Close();
            Game1.state = Game1.GameState.MenuMain;
        }
    }
}
