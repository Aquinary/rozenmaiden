using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alice
{
    class Tile
    {
        // Массив спрайтов
        public static TileHandler[] ArrayTile = new TileHandler[1];

        public static void CreateTile(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            /* Сдесь должны быть прописаны все нужные спрайтовые сеты */
            // Основной спрайтовый сет
            ArrayTile[0] = new TileHandler(content, "tiles\\TileMap");
        }
    }
}
