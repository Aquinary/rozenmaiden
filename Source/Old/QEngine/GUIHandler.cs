using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alice
{
    public class GUIHandler
    {
        // Определяет позицию и размеры элемента
        public Vector2 position = Vector2.Zero;
        public Vector2 area = Vector2.Zero;
        // Определяем отображаемый фрейм
        public int frame = 0;
        // Наличие клика
        public bool click = false;
        // Текстура для элемента
        Texture2D gui;
        public GUIHandler(Microsoft.Xna.Framework.Content.ContentManager content, int x, int y, int w, int h, string path)
        {
            position = new Vector2(x, y);
            area = new Vector2(w, h);
            gui = content.Load<Texture2D>(path);
        }

        // Отрисовка GUI элементов
        public void Draw(GameWindow window, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(gui, position, new Rectangle(0,frame * (int)area.Y,(int)area.X,(int)area.Y), Color.White);
            spriteBatch.End();
        }
    }
}
