using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alice
{
    public static class GUI
    {
        public static GUIHandler[] Button = new GUIHandler[2];

        public static void Create(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            Button[0] = new GUIHandler(content, 30, 200, 256, 30, "GUI\\button_newgame");
            Button[1] = new GUIHandler(content, 10, 10, 128, 32, "GUI\\button_menu");
        }

        public static void Update()
        {
            // Получаем инфо о мыше
            MouseState mouse = Mouse.GetState();
            foreach (GUIHandler gui in GUI.Button)
            {
                // Проверяем, находится ли курсор в диапазоне элемента
                if ((mouse.X  >= gui.position.X * CameraHandler.resize && mouse.X <= gui.position.X * CameraHandler.resize + gui.area.X * CameraHandler.resize) && (mouse.Y >= gui.position.Y * CameraHandler.resize && mouse.Y <= gui.position.Y * CameraHandler.resize + gui.area.Y * CameraHandler.resize))
                {
                    // Меняем фрейм
                    gui.frame = 1;
                    // Если был клик ЛКМ...
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        //... сообщаем об этом
                        gui.click = true;
                    }  
                } else
                {
                    // Обнуляем
                    gui.frame = 0;
                    gui.click = false;
                }
            }
        }           
    }
}

