using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sudoku.Content;
using Sudoku.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    class ModeMenu : Menu
    {
        public ModeMenu(Texture2D _texture, SpriteFont font, Texture2D texture) : base(_texture)
        {
            Button sud_1 = new Button(texture, font)
            {
                Position = new Vector2(230, 150)
            };
            sud_1.Text = "Easy";
            sud_1.Click += PressedSud_1;
            Button sud_2 = new Button(texture, font)
            {
                Position = new Vector2(400, 150)
            };
            sud_2.Text = "Middle";
            sud_2.Click += PressedSud_2;
            Button sud_3 = new Button(texture, font)
            {
                Position = new Vector2(570, 150)
            };
            sud_3.Text = "Hard";
            sud_3.Click += PressedSud_3;
            Button kil_1 = new Button(texture, font)
            {
                Position = new Vector2(230, 350)
            };
            kil_1.Text = "Easy";
            kil_1.Click += PressedKil_1;
            Button kil_2 = new Button(texture, font)
            {
                Position = new Vector2(400, 350)
            };
            kil_2.Text = "Middle";
            kil_2.Click += PressedKil_2;
            Button kil_3 = new Button(texture, font)
            {
                Position = new Vector2(570, 350)
            };
            kil_3.Text = "Hard";
            kil_3.Click += PressedKil_3;

            buttons.Add(sud_1);
            buttons.Add(sud_2);
            buttons.Add(sud_3);
            buttons.Add(kil_1);
            buttons.Add(kil_2);
            buttons.Add(kil_3);
        }

        public void PressedSud_1(object sender, EventArgs e)
        {
            Game1.currentComplexity = 1;
            Game1.currentLevel = 1;
            Game1.isUnfinishedLevel  = 0;
            Game1.state = Game1.GameState.Reload;
        }

        public void PressedSud_2(object sender, EventArgs e)
        {
            Game1.currentComplexity = 2;
            Game1.currentLevel = 1;
            Game1.isUnfinishedLevel  = 0;
            Game1.state = Game1.GameState.Reload;
        }

        public void PressedSud_3(object sender, EventArgs e)
        {
            Game1.currentComplexity = 3;
            Game1.currentLevel = 1;
            Game1.isUnfinishedLevel  = 0;
            Game1.state = Game1.GameState.Reload;
        }

        public void PressedKil_1(object sender, EventArgs e)
        {
            Game1.currentComplexity = 4;
            Game1.currentLevel = 1;
            Game1.isUnfinishedLevel  = 0;
            Game1.state = Game1.GameState.Reload;
        }

        public void PressedKil_2(object sender, EventArgs e)
        {
            Game1.currentComplexity = 5;
            Game1.currentLevel = 1;
            Game1.isUnfinishedLevel  = 0;
            Game1.state = Game1.GameState.Reload;
        }

        public void PressedKil_3(object sender, EventArgs e)
        {
            Game1.currentComplexity = 6;
            Game1.currentLevel = 1;
            Game1.isUnfinishedLevel  = 0;
            Game1.state = Game1.GameState.Reload;
        }
    }

}
