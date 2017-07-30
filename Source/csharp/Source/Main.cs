using System;
using System.Windows.Forms.VisualStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RozenMaiden.AppScreen;

namespace RozenMaiden
{
    public class Main : Game
    {
        
        public GraphicsDeviceManager Device;
        public SpriteBatch SpriteBatch;
        public GameState gameState;

        private MainMenu mainMenu; 
        private GameScreen gameScreen; 

        /// <summary>
        /// Конструктор
        /// </summary>
        public Main()
        {
            Device = new GraphicsDeviceManager(this);
            Device.PreferredBackBufferWidth = 864;
            Device.PreferredBackBufferHeight = 486;
            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Инициализация приложения
        /// </summary>dddfdf
        protected override void Initialize()
        {

            gameState = new GameState();

            mainMenu = new MainMenu();
            gameScreen = new GameScreen();

            gameState.State = GameState.Current.GameScreen;
            base.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// Загрузка контента
        /// </summary>
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// Выгрузка контекста
        /// </summary>
        protected override void UnloadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// Апдейтинг приложения
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            // Получаем текущее состояние сцены
            switch (gameState.State)
            {
                // Главное меню
                case GameState.Current.MainMenu:
                {
                    mainMenu.Update(gameState);
                    break;
                }
                // Игровой процесс
                case GameState.Current.GameScreen:
                {
                    gameScreen.Update(gameState);
                    break;
                }
                case GameState.Current.SplashScreen:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Апдейтинг графического вывода
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);
            // Получаем текущее состояние сцены
            switch (gameState.State)
            {
                    // Главное меню
                case GameState.Current.MainMenu:
                {
                    mainMenu.Draw(SpriteBatch);
                    break;
                }
                    // Игровой процесс
                case GameState.Current.GameScreen:
                {
                    gameScreen.Draw();
                    break;
                }
                case GameState.Current.SplashScreen:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            base.Draw(gameTime);
        }
    }
}