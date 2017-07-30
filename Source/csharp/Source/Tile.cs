using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RozenMaiden
{
    public class Tile
    {
        #region Fields

        private Texture2D _texture;
        private Vector2 _size = new Vector2 (8,8);

        #endregion

        #region Properties


        /// <summary>
        /// Контейнер для текстуры
        /// </summary>
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        /// <summary>
        /// Размеры тайла 
        /// </summary>
        public Vector2 Size
        {
            get { return _size; }
            set { _size = value; }
        }

        #endregion

        #region Construct

        public Tile(string filePath)
        {
            try
            {
                Texture = Program.Game.Content.Load<Texture2D>(filePath);
            }
            catch (Exception ex)
            {
                string tempError = ex.Message + "\n" + filePath; // формирование строчки с текстом ошибки
                MessageBox.Show(tempError, "Error!");
            }
        }  

        #endregion
    
        #region PublicMethods

        public Rectangle GetRectangle(int tileIndex)
        {
            int nx = ~~(1024 / (int)Size.X);
            int ny = ~~(1024 / (int)Size.Y);
            // Возвращаем координаты, определяющие нужный спрайт в тайлсете
            return new Rectangle
                (
                    (tileIndex % nx) * (int)Size.X, 
                    (~~(tileIndex/ny)) * (int)Size.Y, (int)Size.X, (int)Size.Y
                );
        }

        #endregion
                  
    }
}

