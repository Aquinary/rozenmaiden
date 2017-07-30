using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Alice
{
    public class TileHandler
    {
        public Texture2D Texture;
        public static int TileWidth = 16;
        public static int TileHeight = 16;

        public TileHandler(Microsoft.Xna.Framework.Content.ContentManager content, string path_to_texture)
        {
            // Устанавливаем контекст текстуры
            this.Texture = content.Load<Texture2D>(path_to_texture);
        }

        public Rectangle GetRectangle(int tileIndex)
        {
            int nx = ~~(1024 / TileHandler.TileWidth);
            int ny = ~~(1024 / TileHandler.TileHeight);
            // Возвращаем координаты, определяющие нужный спрайт в тайлсете
            return new Rectangle((tileIndex % nx) * TileHandler.TileWidth, (~~(tileIndex/ny)) * TileHandler.TileHeight, TileHandler.TileWidth, TileHandler.TileHeight);
        }
    }
}
