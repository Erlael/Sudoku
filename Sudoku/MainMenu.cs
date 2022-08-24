using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sudoku.Content;
using Sudoku.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Sudoku
{
    class MainMenu : Menu
    {
        public MainMenu(Texture2D _texture, SpriteFont font, Texture2D texture) : base(_texture)
        {
            Button _continue = new Button(texture, font)
            {
                Position = new Vector2(117, 165)
            };
            _continue.Text = "Continue";
            _continue.Click += PressedContinue;
           
            Button newGame = new Button(texture, font)
            {
                Position = new Vector2(117, 265)
            };
            newGame.Text = "New game";
            newGame.Click += PressedNewGame;
            Button exit = new Button(texture, font)
            {
                Position = new Vector2(117, 365)
            };
            exit.Text = "Exit";
            exit.Click += PressedExit;

            buttons.Add(_continue);
            buttons.Add(newGame);
            buttons.Add(exit);
            CheckButton();

        }

        public void PressedContinue(object sender, EventArgs e)
        {
            StreamReader jsonReader = new StreamReader("load.json");
            string json = jsonReader.ReadToEnd();
            jsonReader.Close();
            ForSave s_ = new ForSave();
            s_ = JsonConvert.DeserializeObject<ForSave>(json);
            Game1.currentComplexity = s_.currentComplexity;
            Game1.currentLevel = s_.currentLevel;
            Game1.isUnfinishedLevel  = s_.isUnfinishedLevel ;
            Game1.state = Game1.GameState.Reload;
        }

        public void PressedNewGame(object sender, EventArgs e)
        {
            Game1.state = Game1.GameState.MenuMode;
        }

        public void PressedExit(object sender, EventArgs e)
        {
            Game1.isExit = 1;
        }
        public void CheckButton()
        {
            if (!File.Exists("load.json"))
            {
                buttons[0].Enabled = false;
            }
            else
                buttons[0].Enabled = true;
        }
    }
}
