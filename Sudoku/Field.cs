using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Sudoku.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sudoku
{
    class Field
    {
        Texture2D texture;
        Texture2D textureField;
        public int[,] correct = new int[9, 9];
        public int[,] visible = new int[9, 9];
        public Button[,] cels = new Button[9, 9];
        public Vector2 PressedButton;
        public int attemps;
        private bool isVictory;
        FieldArrays fieldArrays;
        SpriteFont font;

        public Button[,] numbers = new Button[3, 3];
        public Field(string name, Texture2D buttonTexture, SpriteFont _font, Texture2D fieldTexture, Texture2D numberTexture, Texture2D backgroundTexture)
        {
            LoadField(name, fieldTexture);
            texture = backgroundTexture;
            font = _font;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cels[i, j] = new Button(buttonTexture, font)
                    {
                        Position = new Vector2(150 + 32 * j, 100+32 * i),
                        PosField = new Vector2(i, j)
                    };
                    cels[i, j].Click += PressButton;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    numbers[i, j] = new Button(numberTexture, font)
                    {
                        Position = new Vector2(40 * i + 600, 40 * j + 200)
                    };
                    numbers[i, j].Text = (j * 3 + i + 1).ToString();
                    numbers[i, j].Click += PressNumber;
                }
            }
        }
        public void LoadField(string name, Texture2D text)
        {
           
                
            textureField = text;
            attemps = 3;
            PressedButton = new Vector2(-1, -1);

            StreamReader jsonReader = new StreamReader(name);
            string json = jsonReader.ReadToEnd();
            jsonReader.Close();

            fieldArrays = JsonConvert.DeserializeObject<FieldArrays>(json);
            correct = FieldArrays.arrayToMatrix(fieldArrays.correct);
            visible = FieldArrays.arrayToMatrix(fieldArrays.visible);
            
        }

        public void ClearCells()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cels[i, j].ToggleColor(false);
                }
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch, Rectangle bounds)
        {
            _spriteBatch.Draw(texture, bounds, Color.White);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cels[i, j].Draw(gameTime, _spriteBatch);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    numbers[i, j].Draw(gameTime, _spriteBatch);
                }
            }
            _spriteBatch.Draw(textureField, new Vector2(150,100), Color.White);
            _spriteBatch.DrawString(font, "Mistakes: "+(-attemps+3)+"/3", new Vector2(600, 100), Color.Black);
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game1.state = Game1.GameState.MenuPause;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (visible[i, j] != 0)
                        cels[i, j].Text = visible[i, j].ToString();
                    else
                        cels[i, j].Text = " ";
                    cels[i, j].Update(gameTime);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    numbers[i, j].Update(gameTime);
                }
            }

        }

        private void PressButton(object sender, EventArgs e)
        {
            Button temp = (Button)sender;
            
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cels[i, j].ToggleColor(false);
                    cels[(int)temp.PosField.X, j].ToggleColor(true);
                   
                }
                cels[i, (int)temp.PosField.Y].ToggleColor(true);
            }
            PressedButton = temp.PosField;
            
        }

        private void PressNumber(object sender, EventArgs e)
        {
            if (PressedButton == new Vector2(-1,-1))
            return;
            Button temp = (Button)sender;
            if ((visible[(int)PressedButton.X, (int)PressedButton.Y] == 0) || (cels[(int)PressedButton.X, (int)PressedButton.Y].PenColour == Color.Red))
            {
                visible[(int)PressedButton.X, (int)PressedButton.Y] = Convert.ToInt32(temp.Text);
                if (temp.Text != correct[(int)PressedButton.X, (int)PressedButton.Y].ToString())
                {
                    cels[(int)PressedButton.X, (int)PressedButton.Y].PenColour = Color.Red;
                    attemps--;
                    if (attemps == 0)
                        Game1.state = Game1.GameState.EndOfGame;
                }
                else
                    cels[(int)PressedButton.X, (int)PressedButton.Y].PenColour = Color.Black;
            }

            isVictory = true;
            for (int i=0; i<9; i++)
            {
                for (int j=0; j < 9; j++)
                {
                    if (visible[i, j] !=correct[i,j] )
                        isVictory = false;
                }
            }
            if (isVictory == true)
                Game1.state = Game1.GameState.VictoryOfGame;

        }

        public void Save()
        {
            fieldArrays.correct = FieldArrays.matrixToArray(correct);
            fieldArrays.visible = FieldArrays.matrixToArray(visible);

            ForSave s_ = new ForSave();
            s_.currentComplexity = Game1.currentComplexity;
            s_.currentLevel = Game1.currentLevel;
            s_.isUnfinishedLevel  = Game1.isUnfinishedLevel ;

            FileStream fs = File.Create("1_" + Game1.currentComplexity + "_" + Game1.currentLevel + ".json");
            fs.Close();
            fs = File.Create("load.json");
            fs.Close();
            StreamWriter jsonWriter = new StreamWriter("1_" + Game1.currentComplexity + "_" + Game1.currentLevel + ".json");

            string json = JsonConvert.SerializeObject(fieldArrays, Formatting.Indented);
            jsonWriter.Write(json);
            jsonWriter.Close();

            jsonWriter = new StreamWriter("load.json");
            jsonWriter.Write(JsonConvert.SerializeObject(s_, Formatting.Indented));
           
            jsonWriter.Close();
        }


    }



    public class FieldArrays
    {
        public int[] correct { get; set; }
        public int[] visible { get; set; }

        public static int[,] arrayToMatrix(int[] temp)
        {
            int[,] array = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    array[i, j] = temp[i * 9 + j];
                }
            }
            return array;
        }
        public static int[] matrixToArray(int[,] temp)
        {
            int[] array = new int[81];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    array[i * 9 + j] = temp[i, j];
                }
            }
            return array;
        }
    }

    public class ForSave
    {
        public int currentLevel { get; set; }
        public int currentComplexity { get; set; }
        public int isUnfinishedLevel  { get; set; }
    }
}
