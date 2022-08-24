using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sudoku.Content;
using Sudoku.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    class VictoryMenu : Menu
    {
        public VictoryMenu(Texture2D _texture, SpriteFont font, Texture2D texture) : base(_texture)
        {
            Button nextLevel = new Button(texture, font)
            {
                Position = new Vector2(365, 365)
            };
            nextLevel.Text = "Next level";
            nextLevel.Click += NextLevel;
            buttons.Add(nextLevel);
        }

        public void NextLevel(object sender, EventArgs e)
        {
            Game1.isUnfinishedLevel  = 0;
            Game1.state = Game1.GameState.NextLevel;
        }
    }
}
