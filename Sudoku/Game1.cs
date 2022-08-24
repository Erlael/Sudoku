using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.IO;

namespace Sudoku
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Field field;
        private MainMenu mainMenu;
        private ModeMenu modeMenu;
        private PauseMenu pauseMenu;
        private VictoryMenu victoryMenu;
        private EndGameMenu endGame;
        //private int error;
        public static int currentLevel;
        public static int currentComplexity;
        public static int isUnfinishedLevel  = 0;
        public static int isExit = 0;


        public enum GameState
        {
            MenuMain,
            MenuMode,
            MenuPause,
            Gameplay,
            EndOfGame,
            VictoryOfGame,
            NextLevel,
            Reload,
            Save
        }

        public static GameState state;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // инициализаця игры при запуске
            state = GameState.MenuMain;
           
            _graphics.PreferredBackBufferWidth = 960;
            _graphics.PreferredBackBufferHeight = 540;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            field = new Field("0_1_1.json", Content.Load<Texture2D>("Images/cell"), Content.Load<SpriteFont>("Fonts/font"), Content.Load<Texture2D>("Images/cell"), Content.Load<Texture2D>("Images/NumberCell"), Content.Load<Texture2D>("Images/Background"));
            mainMenu = new MainMenu(Content.Load<Texture2D>("Images/main"), Content.Load<SpriteFont>("Fonts/font"), Content.Load<Texture2D>("Images/Button"));
            modeMenu = new ModeMenu(Content.Load<Texture2D>("Images/Modemenu"), Content.Load<SpriteFont>("Fonts/font"), Content.Load<Texture2D>("Images/button2"));
            pauseMenu = new PauseMenu(Content.Load<Texture2D>("Images/PauseMenu"), Content.Load<SpriteFont>("Fonts/font"), Content.Load<Texture2D>("Images/Button"));
            victoryMenu= new VictoryMenu(Content.Load<Texture2D>("Images/VictoryMenu"), Content.Load<SpriteFont>("Fonts/font"), Content.Load<Texture2D>("Images/Button"));
            endGame=new EndGameMenu(Content.Load<Texture2D>("Images/EndGame"), Content.Load<SpriteFont>("Fonts/font"), Content.Load<Texture2D>("Images/Button"));
            // загрузка контента
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || (isExit == 1))
                Exit();

            // TODO: Add your update logic here
            switch (state)
            {
                case GameState.MenuMain:
                    mainMenu.CheckButton();
                    mainMenu.Update(gameTime);
                    break;
                case GameState.MenuMode:
                    modeMenu.Update(gameTime);
                    break;
                case GameState.Gameplay:
                    field.Update(gameTime);
                    break;
                case GameState.MenuPause:
                    pauseMenu.Update(gameTime);
                    break;
                case GameState.EndOfGame:
                    endGame.Update(gameTime);
                    break;
                case GameState.Reload:
                    if (!File.Exists("" + isUnfinishedLevel  + "_" + currentComplexity + "_" + currentLevel + ".json"))
                    {
                        Game1.state = Game1.GameState.MenuMain;
                        break;
                    }
                    if(currentComplexity<4)
                         field.LoadField("" + isUnfinishedLevel  + "_" + currentComplexity + "_" + currentLevel+".json", Content.Load<Texture2D>("Images/Frame"));
                    else
                         field.LoadField("" + isUnfinishedLevel  + "_" + currentComplexity + "_" + currentLevel + ".json", Content.Load<Texture2D>("Images/Frame"+ currentComplexity+"_"+ currentLevel));
                    field.ClearCells();
                    Game1.state = Game1.GameState.Gameplay;
                    break;
                case GameState.VictoryOfGame:

                    victoryMenu.Update(gameTime);
                    break;
                case GameState.NextLevel:
                    currentLevel++;
                    if (!File.Exists("" + isUnfinishedLevel  + "_" + currentComplexity + "_" + currentLevel + ".json"))
                    {
                        Game1.state = Game1.GameState.MenuMain;
                        break;
                    }
                    if (currentComplexity < 4)
                        field.LoadField("" + isUnfinishedLevel  + "_" + currentComplexity + "_" + currentLevel + ".json", Content.Load<Texture2D>("Images/Frame"));
                    else
                        field.LoadField("" + isUnfinishedLevel  + "_" + currentComplexity + "_" + currentLevel + ".json", Content.Load<Texture2D>("Images/Frame" + currentComplexity + "_" + currentLevel));
                    
                    field.ClearCells();
                    Game1.state = Game1.GameState.Gameplay;
                    break;
                case GameState.Save:
                    field.Save();
                    state = GameState.MenuPause;
                    break;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            switch (state)
            {
                case GameState.MenuMain:
                    mainMenu.Draw(gameTime, _spriteBatch, _spriteBatch.GraphicsDevice.Viewport.Bounds);
                    break;
                case GameState.MenuMode:
                    modeMenu.Draw(gameTime, _spriteBatch, _spriteBatch.GraphicsDevice.Viewport.Bounds);
                    break;
                case GameState.Gameplay:
                    field.Draw(gameTime, _spriteBatch, _graphics.GraphicsDevice.Viewport.Bounds);
                    break;
                case GameState.MenuPause:
                    pauseMenu.Draw(gameTime, _spriteBatch, _graphics.GraphicsDevice.Viewport.Bounds);
                    break;
                case GameState.EndOfGame:
                    endGame.Draw(gameTime, _spriteBatch, _graphics.GraphicsDevice.Viewport.Bounds);
                    break;
                case GameState.VictoryOfGame:
                    victoryMenu.Draw(gameTime, _spriteBatch, _graphics.GraphicsDevice.Viewport.Bounds);
                    break;
           
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
