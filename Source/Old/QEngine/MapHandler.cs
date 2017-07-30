using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Alice
{
    public class MapHandler
    {
        // Ширина, высота, глубина мира
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public int MapLayer { get; set; }

        // Массив, отведенный под мир
        public int[,,] Map;

        public MapHandler(int mapWidth, int mapHeight, int mapLayer)
        {
            MapWidth = mapWidth;
            MapHeight = mapHeight;
            MapLayer = mapLayer;

            Map = new int[MapWidth,MapHeight, MapLayer];

            // Заполняем мир нулями (первичная инициализация)
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    for (int z = 0; z < MapLayer; z++)
                    {
                        Map[x, y, z] = 0;
                    }
                }
            }
        }

        /* Метод отрисовывки карты */

        public void Draw(GameWindow window, SpriteBatch spriteBatch, TileHandler tile, EntityHandler[] entity)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, CameraHandler.GetTransform());
            // Переменная не дает отрисовываться персонажу множество раз
            bool isPlayerDraw = false;
            // Определяем границы, подходящие по размерам экрана
            int map_left = (int)-CameraHandler.Position.X / TileHandler.TileWidth - 1;
            int map_right = map_left + (window.ClientBounds.Width / TileHandler.TileWidth) / CameraHandler.resize + 3;
            int map_top = (int)-CameraHandler.Position.Y / TileHandler.TileHeight;
            int map_bottom = map_top + (window.ClientBounds.Height / TileHandler.TileHeight + 1) / CameraHandler.resize + 3;
            for (int x = map_left; x < map_right; x++)
            {
                for (int y = map_top; y < map_bottom; y++)
                {
                    for (int z = 0; z < 6; z++)
                    {
                        // Не рисуем то, что находится за границой карты
                        if ((x >= 0 && y >= 0) && (x < this.MapWidth && y < this.MapHeight))
                        {
                            // Слои с 0 по 4 включительно
                            if (z >= 0 && z <= 4)
                            {
                                spriteBatch.Draw(tile.Texture, new Vector2(x * TileHandler.TileWidth, y * TileHandler.TileHeight), tile.GetRectangle(Map[x, y, z]), Color.White, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, z*0.10f);
                            }
                            // Слои с 5 по 6 включительно
                            if (z > 4 && z < 6)
                            {
                                // z = 5 соответствует слою "hight". Становится чуть прозрачным, если пероснаж за ними.
                                if (z == 5 && ((int)entity[0].Position.X / TileHandler.TileWidth == x - 1 || (int)entity[0].Position.X / TileHandler.TileWidth == x || (int)entity[0].Position.X / TileHandler.TileWidth == x - 2) && ((int)entity[0].Position.Y / TileHandler.TileHeight == y - 1 || (int)entity[0].Position.Y / TileHandler.TileHeight == y || (int)entity[0].Position.Y / TileHandler.TileHeight == y+1))
                                {
                                    spriteBatch.Draw(tile.Texture, new Vector2(x * TileHandler.TileWidth, y * TileHandler.TileHeight), tile.GetRectangle(Map[x, y, z]), new Color(0,0,0,0.5f), 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, (z + 1) * 0.10f);
                                } else
                                {
                                    spriteBatch.Draw(tile.Texture, new Vector2(x * TileHandler.TileWidth, y * TileHandler.TileHeight), tile.GetRectangle(Map[x, y, z]), Color.White, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, (z + 1) * 0.10f);
                                } 
                            }
                            // Условие не позволяет рисовать сущности более одного раза
                            if (!isPlayerDraw)
                            {
                                isPlayerDraw = true;
                                // Проходим по всем сущностям в массиве
                                foreach (EntityHandler e in entity )
                                {
                                    // отрисовываем их
                                    e.Draw(window, spriteBatch);
                                }
                            }
                        }
                    }
                }
            }
            spriteBatch.End();
        }

        /* Метод чтения карты из существующего файла */
        public void Read(string path_to_file, int layerMap)
        {
            var text = File.ReadLines(path_to_file).Select(x => x.Split(',')).ToArray();
            string[][] temp = text.ToArray();     
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    Map[x, y, layerMap] = Convert.ToInt32(temp[y][x]) == -1 ? 0 : Convert.ToInt32(temp[y][x]);
                }
            }
        }
    }
}
