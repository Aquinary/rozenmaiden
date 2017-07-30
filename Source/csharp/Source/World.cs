using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RozenMaiden
{
    public class World
    {
        #region Fields



        private int[,] _map;

        private int _mapHeight;
        private int _mapWidth;

        private Tile _tileMap;

        #endregion

        #region Properties

        /// <summary>
        /// Контейнер для карты
        /// </summary>
        public int[,] Map
        {
            get { return _map;  }
            set { _map = value; }
        }

        /// <summary>
        /// Количество тайлов по оси Y
        /// </summary>
        public int MapHeight
        {
            get { return _mapHeight; }
            set { _mapHeight = value; }
        }

        /// <summary>
        /// Контейнер для карты
        /// </summary>
        public int MapWidth
        {
            get { return _mapWidth; }
            set { _mapWidth = value; }
        }                 

        public Tile TileMap
        {
            get { return _tileMap; }
            set { _tileMap = value; }
        }   

        #endregion

        #region Construct

        public World(string filePath, int mapHeight, int mapWidth,Tile tileMap)
        {
            
            MapHeight = mapHeight;
            MapWidth = mapWidth;

            TileMap = tileMap;

            Map = new int[MapWidth,MapHeight];

            var text = File.ReadLines(filePath).Select(x => x.Split(',')).ToArray();
            string[][] temp = text.ToArray();     
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    Map[x, y] = Convert.ToInt32(temp[y][x]) == -1 ? 0 : Convert.ToInt32(temp[y][x]);
                }
            }
        }

        #endregion

        #region PublicMethods
              
        public void Draw()
        {
            int leftTile = -(int)Camera.Position.X / (int)TileMap.Size.X;
            int rightTile = -(int)Camera.Position.X / (int)TileMap.Size.X + Program.Game.Device.PreferredBackBufferWidth / (int)TileMap.Size.X / (int)Camera.Zoom + 1;          
            int topTile = -(int)Camera.Position.Y / (int)TileMap.Size.Y;
            int bottomTile = -(int)Camera.Position.Y / (int)TileMap.Size.Y + Program.Game.Device.PreferredBackBufferHeight / (int)TileMap.Size.Y / (int)Camera.Zoom + 1;

            Program.Game.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointWrap, null, null,null, Camera.GetTransform());           
            for (int x = leftTile; x < rightTile; x++)
            {
                for (int y = topTile; y < bottomTile; y++)
                {
                    if ((x >= 0 && y >= 0) && (x < MapWidth && y < MapHeight))
                    {
                        Program.Game.SpriteBatch.Draw
                        (
                            TileMap.Texture, 
                            new Vector2(x * TileMap.Size.X, y * TileMap.Size.Y),
                            TileMap.GetRectangle(Map[x, y]),
                            Color.White
                        );
                    }
                }
            }

            Program.Game.SpriteBatch.End();
        }

        public void Update()
        {

        }

        #endregion
    }
}

