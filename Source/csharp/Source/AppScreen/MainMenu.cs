using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RozenMaiden.AppScreen;
using RozenMaiden.GUI;

namespace RozenMaiden
{
    public class MainMenu
    {
        private GameState gameState;
        public Color Backgroung { get; } = Color.Transparent; // фон
        public Button[] MainButton = new Button[2]; // массив кнопок
        public string[] TextureNewGame = new string[3]; // массив параметров с путями к текстурам
        public Dialog[] MainDialog = new Dialog[2];
        public string[] TextureExit = new string[2];

        //public RenderTarget2D Render;
        public MainMenu()
        {
            MainButton[0] = new Button("Texture\\GUI\\ButtonNewGame");
            MainButton[0].Size = new Vector2(390, 48);
            MainButton[0].Position = new Vector2(10, 0);
            MainButton[0].MouseDownHandler += EnableDialog1;

            MainButton[1] = new Button("Texture\\GUI\\ButtonNewGame");
            MainButton[1].Size = new Vector2(195, 24);
            MainButton[1].Position = new Vector2(10, 30);
            MainButton[1].MouseDownHandler += EnableDialog2;

            MainDialog[0] = new Dialog("Texture\\GUI\\DialogBorder");
            MainDialog[0].Size = new Vector2(913, 188);
            MainDialog[0].Position = new Vector2(10,60);

            MainDialog[0].Text[0] = "Это обычный, ничем не примечательный диалог. ssssdasd asd asda sdas dsad asd asd sad sa fdsfs dfds";
            MainDialog[0].Text[1] = "Однако простота только кажется таковой.";
            MainDialog[0].Text[2] = "Его реализация в таком виде далась крайне сложно.";
            MainDialog[0].Text[3] = "Есть очень много вещей, которые нужно добавить";
            MainDialog[0].Text[4] = "А так же много проблем, которых необходимо решить.";
            MainDialog[0].Text[5] = "Но я думаю, что Вы отнесетесь к этому с пониманием.";
            MainDialog[0].Text[6] = "%%Как же он забавно печатается...%%";


            MainDialog[1] = new Dialog("Texture\\GUI\\DialogBorder");
            MainDialog[1].Size = new Vector2(913, 188);
            MainDialog[1].Position = new Vector2(100,300);
            MainDialog[1].Text[0] = "Это еще один обыкновенный диалог.";
            MainDialog[1].Text[1] = "И он наглядно показывает, как просто можно связать";
            MainDialog[1].Text[2] = "Между собой элемент \"Button\" и элемент \"Dialog\"";
            MainDialog[1].Text[3] = "Это кажется довольно простым, но на самом деле";
            MainDialog[1].Text[4] = "Пришлось прибегнуть к использованию потоков.";
            MainDialog[1].Text[5] = "Следующий шаг - добавление ответов в диалоги";
            MainDialog[1].Text[6] = "Возможность выбора даст большую интерактивность.";      
        }

        /// <summary>
        /// Метод отрисовывает кнопки
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            
            for (int i = 0; i < MainButton.Length; i++)
            {
                MainButton[0].Draw();
            }

            for (int i = 0; i < MainDialog.Length; i++)
            {
                MainDialog[i].Draw();
            }                
        }
        /// <summary>
        /// Метод апдейта логики
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameState gameState)
        {
            this.gameState = gameState; 
            
            for (int i = 0; i < MainButton.Length; i++)
            {
                MainButton[i].Update();
            }
            for (int i = 0; i < MainDialog.Length; i++)
            {
                MainDialog[i].Update();
            }
        }

        public void EnableDialog1(object obj, EventArgs e)
        {
            //MainDialog[0].IsVisible = !MainDialog[0].IsVisible;
            gameState.State = GameState.Current.GameScreen;

        }

        public void EnableDialog2(object obj, EventArgs e)
        {
            MainDialog[1].IsVisible = !MainDialog[1].IsVisible;
        }
    }
}